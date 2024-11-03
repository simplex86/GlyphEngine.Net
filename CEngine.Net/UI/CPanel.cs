namespace SimpleX.CEngine.UI
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPanel
    {
        /// <summary>
        /// 
        /// </summary>
        CGameObject gameObject { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        void Update(float dt);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CPanel<TView> : IPanel where TView : CPanelView, new()
    {
        /// <summary>
        /// 
        /// </summary>
        public CGameObject gameObject => view;

        /// <summary>
        /// 
        /// </summary>
        internal protected TView view { get; }

        /// <summary>
        /// 
        /// </summary>
        protected CPanel()
        {
            view = new TView();
        }

        public virtual void Update(float dt)
        {

        }
    }
}
