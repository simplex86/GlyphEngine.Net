namespace CEngine
{
    /// <summary>
    /// 容器接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IContainable<T>
    {
        /// <summary>
        /// 子对象数量
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="item"></param>
        void Add(T item);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="item"></param>
        void Remove(T item);

        /// <summary>
        /// 获取指定索引的子对象
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        T GetChild(int index);
    }
}
