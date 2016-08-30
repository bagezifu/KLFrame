//author:kuribayashi    2016年8月31日05:09:17
using UnityEngine;
using System.Reflection;
namespace KLFrame
{
    public class InstanceMono<T> : MonoBehaviour where T : class
    {

        private static T instance;
        /// <summary>
        /// 构造函数赋值单例
        /// </summary>
        public InstanceMono()
        {
            instance = this as T;
        }
        /// <summary>
        /// 获取单例
        /// </summary>
        /// <returns></returns>
        public static T GetInstance()
        {
            return instance;
        }
        /// <summary>
        /// Mono相关组件获取只能在主进程中进行,所以在Start中调用初始化并获取组件赋值给描述了AutoAssign的对象
        /// </summary>
        public virtual void Start()
        {
            Initialize();
        }

        /// <summary>
        /// 初始化通过反射寻找Attribute并且自动赋值
        /// </summary>
        void Initialize()
        {
            FieldInfo[] fields = instance.GetType().GetFields();
            foreach (FieldInfo f in fields)
            {
                object[] objAttrs = f.GetCustomAttributes(typeof(AutoAssignAttribute), true);
                if (objAttrs.Length > 0)
                {
                    AutoAssignAttribute attr = objAttrs[0] as AutoAssignAttribute;
                    if (attr != null)
                    {
                        Object obj = GetComponent(f.FieldType);
                        if (obj)
                            f.SetValue(instance, obj);
                        else Debug.LogWarningFormat("{0}类成员自动获取{1}组件失败,请确定当前Gameobject是否拥有此组件!", instance.GetType().Name, f.FieldType.Name);
                    }
                }
            }
        }
    }

}