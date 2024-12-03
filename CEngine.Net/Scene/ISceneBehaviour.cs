namespace SimpleX.CEngine
{
    /// <summary>
    /// 场景
    /// </summary>
    public interface ISceneBehaviour
    {
        /// <summary>
        /// 进入场景
        /// </summary>
        void Enter();

        /// <summary>
        /// 刷新场景
        /// </summary>
        /// <param name="dt"></param>
        void Update(float dt);

        /// <summary>
        /// 离开场景
        /// </summary>
        void Exit();
    }

    /// <summary>
    /// 场景
    /// </summary>
    public class CSceneBehaviour : ISceneBehaviour
    {
        /// <summary>
        /// 进入场景
        /// </summary>
        public virtual void Enter()
        {

        }

        /// <summary>
        /// 刷新场景
        /// </summary>
        /// <param name="dt"></param>
        public virtual void Update(float dt)
        {

        }

        /// <summary>
        /// 离开场景
        /// </summary>
        public virtual void Exit()
        {

        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CSceneBehaviourAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public string tag { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag"></param>
        public CSceneBehaviourAttribute(string tag)
        {
            this.tag = tag;
        }
    }
}
