//author:kuribayashi   2016年8月7日23:13:00
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace KLFrame
{
    public class Observer
    {
        public static Dictionary<GameObject, List<UniEventType>> dic_listeners = new Dictionary<GameObject, List<UniEventType>>();
        /// <summary>
        /// 增加事件订阅者
        /// </summary>
        /// <param name="listener">订阅者</param>
        /// <param name="type">订阅者的类</param>
        /// <param name="eventTypes">事件类型</param>
        public static void AddListener<T>(T listener, params UniEventType[] eventTypes)
        {
            if (listener.GetType().GetInterface("IObserver") == null)
            {
                Debug.LogError(string.Format("请把{0}类继承IObserver接口!", listener.GetType().Name));
                return;
            }
            dic_listeners.Add((listener as MonoBehaviour).gameObject, eventTypes.ToList());
        }
        /// <summary>&
        /// 移除订阅
        /// </summary>
        /// <param name="listener">订阅者</param>
        public static void RemoveListener<T>(T listener)
        {
            dic_listeners.Remove((listener as MonoBehaviour).gameObject);
        }

        /// <summary>
        /// 调用事件
        /// </summary>
        /// <param name="arg">事件参数</param>
#pragma warning disable 0219
        public static void Invoke(UniEventArgs arg)
        {
            foreach (var item in dic_listeners)
            {
                foreach (UniEventType t in item.Value.Where(v => v.Equals(arg.eventType)))
                {
                    item.Key.SendMessage("UniEventHandler", arg);
                }
            }
        }

    }


    /// <summary>
    /// 事件类型枚举
    /// </summary> 
    public enum UniEventType
    {
        OnUnitDeath,
        OnUnitAttack,
        OnGameOver,
    }

    /// <summary>
    /// 事件参数类
    /// </summary>
    public class UniEventArgs
    {
        public readonly GameObject sender;
        public readonly UniEventType eventType;
        public readonly System.Object content;
        public UniEventArgs(GameObject SENDER, UniEventType EVENTTYPE)
        {
            sender = SENDER;
            eventType = EVENTTYPE;
        }
        public UniEventArgs(GameObject SENDER, UniEventType EVENTTYPE, System.Object CONTENT) : this(SENDER, EVENTTYPE)
        {
            content = CONTENT;
        }
    }
}