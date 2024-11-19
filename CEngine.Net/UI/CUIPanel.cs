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
    public class CUIPanel : IPanel
    {
        /// <summary>
        /// 
        /// </summary>
        public CGameObject gameObject => view;

        /// <summary>
        /// 
        /// </summary>
        private CUIPanelView view { get; }

        /// <summary>
        /// 
        /// </summary>
        protected CUIPanel()
        {
            var attrs = GetType().GetCustomAttributes(true);
            if (attrs.Length > 0)
            {
                var attr = attrs[0] as CUIPanelAttribute;
                var json = ResourceManager.LoadText(attr.design);
                view = CUIParser.Parse(json);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public T GetComponent<T>(string name) where T : CUIComponent
        {
            return view.GetComponent<T>(name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        public void Update(float dt)
        {
            view?.Update(dt);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CUIPanelAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        internal string design { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="design"></param>
        public CUIPanelAttribute(string design)
        {
            this.design = design;
        }
    }
}
