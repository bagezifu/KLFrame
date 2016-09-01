using UnityEngine;
using System.Collections;
namespace KLFrame
{
    public static class KLExtension
    {
        public static Vector3 GetPosition(this GameObject go) {
            return go.transform.position;
        }
        public static Vector3 SetPosition(this GameObject go,Vector3 pos) {
           return go.transform.position = pos;
        }
        public static void SetParent(this GameObject go,Transform parent) {
            go.transform.SetParent(parent);
        }
        public static void SetParent(this GameObject go, GameObject parent)
        {
            go.transform.SetParent(parent.transform);
        }
        public static int GetLayer(this Transform tran) {
            return tran.gameObject.layer;
        }
        
    }
}
