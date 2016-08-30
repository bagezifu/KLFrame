//author:kuribayashi   2016年8月7日23:12:45
namespace KLFrame
{
    /// <summary>
    /// 观察者模式接口
    /// </summary>
    public interface IObserver
    {
        /// <summary>
        /// 销毁时移除订阅
        /// </summary>
        void OnDestroy();
        /// <summary>
        /// 处理订阅
        /// </summary>
        void UniEventHandler(UniEventArgs arg);
    }
}