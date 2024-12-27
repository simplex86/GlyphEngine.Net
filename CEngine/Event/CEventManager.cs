namespace CEngine
{
    /// <summary>
    /// 
    /// </summary>
    public static class CEventManager
    {
        /// <summary>
        /// 
        /// </summary>
        private static Dictionary<Type, List<IEventHandler>> dict = new Dictionary<Type, List<IEventHandler>>();

        /// <summary>
        /// 初始化
        /// </summary>
        /// <exception cref="Exception"></exception>
        public static void Init()
        {
            var types = CReflectionHelper.FindAll<IEventHandler, CEventHandlerAttribute>();
            foreach (var type in types)
            {
                var handler = Activator.CreateInstance(type) as IEventHandler;
                if (handler == null)
                {
                    throw new Exception($"type not is a event handler: {type.Name}");
                }

                if (!dict.TryGetValue(handler.EventType, out var handlers))
                {
                    handlers = new List<IEventHandler>();
                    dict.Add(handler.EventType, handlers);
                }
                handlers.Add(handler);
            }
        }

        /// <summary>
        /// （同步）发送事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="evt"></param>
        public static void Send<T>(T evt) where T : struct, IEvent
        {
            if (dict.TryGetValue(typeof(T), out var handlers))
            {
                foreach (var handler in handlers)
                {
                    var eventHandler = handler as CEventHandler<T>;
                    eventHandler?.Run(evt);
                }
            }
        }

        /// <summary>
        /// （异步）发送事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="evt"></param>
        /// <returns></returns>
        public static async Task SendAsync<T>(T evt) where T : struct, IEvent
        {
            if (dict.TryGetValue(typeof(T), out var handlers))
            {
                foreach (var handler in handlers)
                {
                    var eventHandler = handler as CEventHandler<T>;
                    await eventHandler?.Run(evt);
                }
            }
        }
    }
}
