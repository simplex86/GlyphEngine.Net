namespace CEngine
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class CBehaviour
    {
        /// <summary>
        /// 
        /// </summary>
        public CGameObject gameobject { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameobject"></param>
        protected CBehaviour(CGameObject gameobject)
        {
            this.gameobject = gameobject;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CBehaviourAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public string tag { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag"></param>
        public CBehaviourAttribute(string tag)
        {
            this.tag = tag;
        }
    }
}
