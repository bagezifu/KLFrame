using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

namespace KLFrame
{
    public static class KLExtension
    {

        public static Vector3 GetPosition(this GameObject go)
        {
            return go.transform.position;
        }
        public static Vector3 SetPosition(this GameObject go, Vector3 pos)
        {
            return go.transform.position = pos;
        }
        public static void SetParent(this GameObject go, Transform parent)
        {
            go.transform.SetParent(parent);
        }
        public static void SetParent(this GameObject go, GameObject parent)
        {
            go.transform.SetParent(parent.transform);
        }
        public static int GetLayer(this Transform tran)
        {
            return tran.gameObject.layer;
        }
        public static void SetActive(this Transform tran, bool value)
        {
            tran.gameObject.SetActive(value);
        }
        public static bool AcitiveSelf(this Transform tran)
        {
            return tran.gameObject.activeSelf;
        }
        public static string GetCurrentTime(this string str)
        {
            return DateTime.Now.ToString();
        }
        public static string SecondToMinute(this int second)
        {
            TimeSpan ts = new TimeSpan(0, 0, second);
            string strseconds = ts.Seconds.ToString();
            if (ts.Seconds < 10)
            {
                strseconds = "0" + strseconds;
            }
            return ts.Minutes + ":" + strseconds;
        }
        public static IEnumerator TextInputMark(this Text text, string blinkMark, float time)
        {
            for (;;)
            {
                yield return new WaitForSeconds(time);
                text.text += blinkMark;
                yield return new WaitForSeconds(time);
                text.text = text.text.Substring(0, text.text.Length-blinkMark.Length);
            }
        }
        public static Vector3 GetPosition(this Component com) {
            return com.transform.position;
        }

    }
}
