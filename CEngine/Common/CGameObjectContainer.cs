namespace CEngine
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class CGameObjectContainer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameobject"></param>
        internal abstract void Add(CGameObject gameobject);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameobject"></param>
        internal abstract void Remove(CGameObject gameobject);
    }
}
