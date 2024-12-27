namespace CEngine
{
    /// <summary>
    /// 事件处理器接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEventHandler
    {
        /// <summary>
        /// 获取消息类型
        /// </summary>
        Type EventType { get; }
    }

    /// <summary>
    /// 消息处理器
    /// </summary>
    /// <typeparam name="T">消息类型</typeparam>
    public abstract class CEventHandler<T> : IEventHandler where T : struct, IEvent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="evt"></param>
        /// <returns></returns>
        public async Task Run(T evt)
        {
            try
            {
                await OnRun(evt);
            }
            catch (Exception ex)
            {
                CDebug.Error($"EventHandler Exception: {ex.Message}");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Type EventType => typeof(T);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="evt"></param>
        /// <returns></returns>
        protected abstract Task OnRun(T evt);
    }

    /// <summary>
    /// 事件处理器特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class CEventHandlerAttribute : Attribute
    {

    }
}
