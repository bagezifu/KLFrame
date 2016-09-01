using System;
using UnityEditor;
using UnityEditor.Callbacks;

/// <summary>
/// 引擎事件
/// </summary>
public static class EngineEvent
{

    public static Action OnReloadScripts;
    public static Func<int, int, bool> OnOpenAsset;
    public static Action<BuildTarget, string> OnBuilded;
    public static Action OnStartPlayMode;
    public static EditorApplication.CallbackFunction OnPlayModeChanage = EditorApplication.playmodeStateChanged;
    public static Action OnPlayModePaused;

    static void Main() {
        EditorApplication.playmodeStateChanged = OnPlayModeChanged;
    }
    /// <summary>
    /// 当脚本重新加载
    /// </summary>
    [DidReloadScripts]
    public static void OnScriptsReloaded()
    {
        if (OnReloadScripts != null)
            OnReloadScripts();
    }
    /// <summary>
    /// 当打开资源
    /// </summary>
    /// <param name="instanceID"></param>
    /// <param name="line"></param>
    /// <returns></returns>
    [OnOpenAssetAttribute(1)]
    public static bool OnOpenAssetEvent(int instanceID, int line)
    {
        if (OnOpenAsset != null)
            OnOpenAsset(instanceID, line);
        /* string name = EditorUtility.InstanceIDToObject(instanceID).name;
         Debug.Log("Open Asset step: 1 (" + name + ")");*/
        return false;
    }
    /// <summary>
    /// 当编译游戏完成
    /// </summary>
    /// <param name="target"></param>
    /// <param name="pathToBuiltProject"></param>
    [PostProcessBuildAttribute(1)]
    public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
    {
        if (OnBuilded != null) OnBuilded(target, pathToBuiltProject);
    }
    /// <summary>
    /// 当进入引擎PplaMode
    /// </summary>
    [PostProcessSceneAttribute(2)]
    public static void OnPostprocessScene()
    {
        if (OnStartPlayMode != null)
            OnStartPlayMode();
    }
    /// <summary>
    /// 当PlayMode状态改变
    /// </summary>
    public static void OnPlayModeChanged()
    {
        //当PlayMode暂停执行
        if (EditorApplication.isPaused) {
            if (OnPlayModePaused != null) OnPlayModePaused();
        }  
    }

    


}





