//author:kuribayashi    2016年8月31日05:24:15
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace KLFrame
{
    /// <summary>
    /// 音效实用工具类
    /// </summary>
    public static class AudioUtility
    {
        //tag字典,保存tag与相关的的音量
        public static Dictionary<string, float> dic_tags = new Dictionary<string, float>();
        /// <summary>
        /// 在指定位置生成音效物体并增加音效脚本
        /// </summary>
        /// <param name="pos">坐标</param>
        /// <returns></returns>
        private static AudioHandler InstantiateAudioObject(Vector3 pos)
        {
            GameObject temp_go = new GameObject("OneShot");
            temp_go.transform.position = pos;
            return temp_go.AddComponent<AudioHandler>();
        }
        /// <summary>
        /// Tag统一处理
        /// </summary>
        /// <param name="att"></param>
        /// <returns></returns>
        private static AudioAttribute TagHandle(AudioAttribute att)
        {
            if (att.tag != string.Empty)
            {
                if (dic_tags.ContainsKey(att.tag))
                {
                    att.volume = dic_tags[att.tag];
                }
                else
                {
                    dic_tags.Add(att.tag, att.volume);
                    Debug.LogWarningFormat("新注册Tag:{0},音量:{1}", att.tag, att.volume);
                }
            }
            return att;
        }
        /// <summary>
        /// Event统一处理
        /// </summary>
        /// <param name="att"></param>
        /// <param name="handler"></param>
        private static void AudioEventHandle(AudioAttribute att, AudioHandler handler)
        {
            if (att.OnStartPlay != null) handler.OnStartPlay += att.OnStartPlay;
            if (att.OnPausePlay != null) handler.OnPausePlay += att.OnPausePlay;
            if (att.OnStopPlay != null) handler.OnStopPlay += att.OnStopPlay;
        }

        public static AudioHandler PlaySoundAsQueue(AudioAttribute[] atts)
        {
          return PlaySoundAsQueueAtLocation(atts, Camera.main.transform.position);
        }

        public static AudioHandler PlaySoundAsQueueAtLocation(AudioAttribute[] atts,Vector2 pos) {  
            AudioHandler handler = InstantiateAudioObject(pos);
            foreach (AudioAttribute a in atts)
            {
                TagHandle(a);
                AudioEventHandle(a, handler);
            }
            handler.AddQueues(atts);
            handler.Play();
            return handler;
        }


        public static AudioHandler PlaySound(AudioAttribute att) {
           return PlaySoundAtLocation(att,Camera.main.transform.position);
        }

        /// <summary>
        /// 在指定位置播放音效
        /// </summary>
        /// <param name="att">声音属性设置</param>
        /// <param name="pos">坐标</param>
        public static AudioHandler PlaySoundAtLocation(AudioAttribute att, Vector3 pos)
        {
            TagHandle(att);
            AudioHandler handler = InstantiateAudioObject(pos);
            AudioEventHandle(att, handler);
            handler.AddQueue(att);
            handler.Play();
            return handler;
        }

        /// <summary>
        /// 在指定位置随机使用数组中的某一声音属性设置
        /// </summary>
        /// <param name="audios">声音属性设置</param>
        /// <param name="pos">坐标</param>
        public static AudioHandler PlayRandomSoundAtLocation(AudioAttribute[] audios, Vector3 pos)
        {
            if (audios.Length == 0) return null;
            int random = Random.Range(0, audios.Length);
            return PlaySoundAtLocation(audios[random], pos);
        }
        /// <summary>
        /// 通过tag来实现调整音量
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="volumeWeight"></param>
        public static void SetVolumeWithTag(string tag, float volumeWeight)
        {
            if (dic_tags.ContainsKey(tag))
            {
                dic_tags[tag] = volumeWeight;
            }
            else
            {
                dic_tags.Add(tag, volumeWeight);
                Debug.LogWarningFormat("新注册Tag:{0},音量:{1}", tag, volumeWeight);
            }
        }
        /// <summary>
        /// 附加BGM到摄像头
        /// </summary>
        /// <param name="att"></param>
        public static AudioHandler AttackBGMtoCamera(AudioAttribute att)
        {
            att = TagHandle(att);
            att.loop = true;
            AudioHandler handler = Camera.main.gameObject.AddComponent<AudioHandler>();
            AudioEventHandle(att,handler);
            handler.AddQueue(att);
            handler.Play();
            return handler;
        }

       

    }
    [System.Serializable]
    public class AudioAttribute
    {
        public AudioClip clip;
        public string tag;
        public float volume;
        public bool loop;
        public float spatialBlend;
        public bool autoDestroy;
        public AudioHandler.SoundDelegate OnStartPlay;
        public AudioHandler.SoundDelegate OnPausePlay;
        public AudioHandler.SoundDelegate OnStopPlay;

        public void Initialize() {
            volume = 1;
            loop = false;
            spatialBlend = 0;
        }
      
        public AudioAttribute(AudioClip CLIP)
        {
            clip = CLIP;
        }
        public AudioAttribute(AudioClip CLIP, float VOLUME, float SPATIALBLEND) : this(CLIP)
        {
            volume = VOLUME;
            spatialBlend = SPATIALBLEND;
        }
        public AudioAttribute(AudioClip CLIP, float VOLUME, float SPATIALBLEND, string TAG) : this(CLIP, VOLUME, SPATIALBLEND)
        {
            tag = TAG;
        }
        public AudioAttribute(AudioClip CLIP, float VOLUME, float SPATIALBLEND, string TAG, bool AUTODESTROY,bool LOOP) : this(CLIP, VOLUME, SPATIALBLEND, TAG)
        {
            loop = LOOP;
            autoDestroy = AUTODESTROY;
        }
      
    }

}