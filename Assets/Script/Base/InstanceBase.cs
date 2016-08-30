//author:kuribayashi  2016年8月7日23:13:28
using UnityEngine;

public class InstanceBase<T> : MonoBehaviour where T:class
{
    private static T instance;
    public InstanceBase() {
        instance = this as T;     
    }

    public static T GetInstance() {
        return instance;
    }
}
