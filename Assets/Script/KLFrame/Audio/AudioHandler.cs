//author:kuribayashi     2016年8月31日05:25:58
using UnityEngine;
using System.Collections;
using System;


namespace KLFrame
{
    /// <summary>
    /// 声音处理类
    /// 继承自MonoBehaviour,存在于场景中
    /// </summary>
    public class AudioHandler : MonoBehaviour
    {

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
        private AudioSource source;
        private void Awake()
        {
            source = gameObject.AddComponent<AudioSource>();
        }
        /// <summary>
        /// 执行开始播放
        /// </summary>
        /// <param name="att">声音属性类</param>
        public void StartPlay(AudioAttribute att)
        {          
            if (!source.clip)
            {
                source.clip = att.clip;
                source.volume = att.volume;
                source.spatialBlend = att.spatialBlend;
                source.loop = att.loop;
                source.Play();
                if (OnStartPlay != null)
                    OnStartPlay(this, new AudioEventArgs(att.clip.name));
               if(!source.loop) StartCoroutine(StopEvent(source.clip.length));
            }
            else
            {
                source.Play();
                StartCoroutine(StopEvent(source.clip.length - source.time));
            }
            autoDestroy = att.autoDestroy;
        }
        /// <summary>
        /// 执行暂停播放
        /// </summary>
        public void PausePlay()
        {
            source.Pause();
            if (OnPausePlay != null)
                OnPausePlay(this, new AudioEventArgs(source.clip.name));
            StopCoroutine("StopEvent");
        }
        /// <summary>
        /// 执行停止播放
        /// </summary>
        public void StopPlay()
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
                OnStopPlay(this, new AudioEventArgs(source.clip.name));
            if (autoDestroy)
                Destroy(this.gameObject);
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