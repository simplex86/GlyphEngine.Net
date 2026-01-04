namespace GlyphEngine
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPanel
    {
        /// <summary>
        /// 
        /// </summary>
        CGameObject GameObject { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        void Update(float dt);
    }
}
