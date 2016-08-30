using UnityEngine;
using System.Collections;
using System.Reflection;

public class InstanceMono<T> : MonoBehaviour where T : class
{
    private static T instance;
    public InstanceMono()
    {
        instance = this as T;
    }

    public static T GetInstance()
    {
        return instance;
    }

    public virtual void Start() {
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

