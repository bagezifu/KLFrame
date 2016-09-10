//author:kuribayashi     2016年8月31日05:25:58
using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace KLFrame
{
    /// <summary>
    /// 声音处理类
    /// 继承自MonoBehaviour,存在于场景中
    /// </summary>
    public class AudioHandler : KLFrameBase<AudioHandler>, IEnumerable
    {

        public AudioSource source;
        public delegate void SoundDelegate(UnityEngine.Object sender, AudioEventArgs arg);
        //开始播放
        public SoundDelegate OnStartPlay;
        //暂停播放
        public SoundDelegate OnPausePlay;
        //停止播放|播放结束
        public SoundDelegate OnStopPlay;
        //是否自动销毁
        public bool autoDestroy;
        public float intervalTime;
        //系统音源组件

        private List<AudioAttribute> AudioQueues = new List<AudioAttribute>();
        private int queuesIndex = -1;
        private bool isPause = false;

        void Awake()
        {
            source = gameObject.AddComponent<AudioSource>();

        }

        public void AddQueue(AudioAttribute att)
        {
            AudioQueues.Add(att);
        }

        public void AddQueues(AudioAttribute[] atts)
        {
            AudioQueues.AddRange(atts);
        }

        /// <summary>
        /// 执行开始播放
        /// </summary>
        /// <param name="att">声音属性类</param>
        public void Play()
        {
            if (isPause)
            {
                source.Play();
                StartCoroutine(StopEvent(source.clip.length - source.time));
                isPause = false;
            }
            else
            {
                queuesIndex++;
                SetSourceValueAndPlay();
                if (OnStartPlay != null)
                    OnStartPlay(this, new AudioEventArgs(AudioQueues[queuesIndex].clip.name));
                if (!source.loop) StartCoroutine(StopEvent(source.clip.length));
            }
            autoDestroy = AudioQueues[queuesIndex].autoDestroy;
            Debug.Log(autoDestroy);
        }



        private void SetSourceValueAndPlay()
        {
            source.clip = AudioQueues[queuesIndex].clip;
            source.volume = AudioQueues[queuesIndex].volume;
            source.spatialBlend = AudioQueues[queuesIndex].spatialBlend;
            source.loop = AudioQueues[queuesIndex].loop;
            source.Play();
        }



        /// <summary>
        /// 执行暂停播放
        /// </summary>
        public void Pause()
        {
            isPause = true;
            source.Pause();
            if (OnPausePlay != null)
                OnPausePlay(this, new AudioEventArgs(source.clip.name));
            StopCoroutine("StopEvent");
        }
        /// <summary>
        /// 执行停止播放
        /// </summary>
        public void Stop()
        {
            source.Stop();
            if (OnStopPlay != null)
                OnStopPlay(this, new AudioEventArgs(source.clip.name));
        }
        /// <summary>
        /// 协程延时执行声音播放完成事件
        /// </summary>
        /// <param name="time">延迟时间</param>
        /// <returns></returns>
        IEnumerator StopEvent(float time)
        {
            yield return new WaitForSeconds(time);
            if (OnStopPlay != null)
            {
                OnStopPlay(this, new AudioEventArgs(source.clip.name));
            }
            if (queuesIndex + 1 == AudioQueues.Count)
            {
                if (autoDestroy)
                {
                    Destroy(this.gameObject);
                }
            }
            else
            {
                Play();
            }
        }

        public void FadeOut(AudioSource source, float duration)
        {
            StartCoroutine(Cor_Fade(FadeType.Out, source, duration));
        }

        public void FadeIn(AudioSource source, float duration)
        {
            source.volume = 0;
            StartCoroutine(Cor_Fade(FadeType.In, source, duration));
        }

        IEnumerator Cor_Fade(FadeType ft, AudioSource source, float duration)
        {
            float intervalCount = duration / intervalTime;
            float tween = 1.0f / intervalCount;
            for (;;)
            {
                switch (ft)
                {
                    case FadeType.In:
                        source.volume += tween;
                        break;
                    case FadeType.Out:
                        source.volume -= tween;
                        break;
                }
                yield return new WaitForSecondsRealtime(intervalTime);
                if (source.volume == 1 || source.volume == 0) StopCoroutine("Cor_Fade");
            }
        }

        public IEnumerator GetEnumerator()
        {
            return new AudioHandlerEnumerator(AudioQueues.ToArray());
        }





    }
    /// <summary>
    /// 声音事件参数
    /// </summary>
    public class AudioEventArgs : EventArgs
    {
        //声音片段名称
        public string ClipName { get; set; }
        public AudioEventArgs(string name)
        {
            ClipName = name;
        }
    }
    /// <summary>
    /// 淡入淡出枚举
    /// </summary>
    public enum FadeType
    {
        In,
        Out
    }
}