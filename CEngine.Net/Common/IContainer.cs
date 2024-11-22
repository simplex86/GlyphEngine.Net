namespace SimpleX.CEngine
{
    /// <summary>
    /// 
    /// </summary>
    public interface IContainer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameobject"></param>
        public void Add(CGameObject gameobject);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameobject"></param>
        public void Remove(CGameObject gameobject);
    }
}
