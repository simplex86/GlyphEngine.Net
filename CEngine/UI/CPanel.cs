namespace CEngine.UI
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
    public class CPanel : IPanel
    {
        /// <summary>
        /// 
        /// </summary>
        public CGameObject gameObject => view;

        /// <summary>
        /// 
        /// </summary>
        private CPanelView view { get; }

        /// <summary>
        /// 
        /// </summary>
        protected CPanel()
        {
            var attrs = GetType().GetCustomAttributes(true);
            foreach (var v in attrs) 
            {
                if (v is CPanelAttribute attr)
                {
                    view = CPanelDeserializer.Deserialize(attr.design);
                    break;
                }
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
    public class CPanelAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        internal string design { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="design"></param>
        public CPanelAttribute(string design)
        {
            this.design = design;
        }
    }
}
