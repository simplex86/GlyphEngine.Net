namespace CEngine
{
    /// <summary>
    /// 
    /// </summary>
    public class CPanel
    {
        /// <summary>
        /// 
        /// </summary>
        public CGameObject GameObject => View;

        /// <summary>
        /// 
        /// </summary>
        internal CPanelView View { get; set; } = null;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public T GetComponent<T>(string name) where T : CWidget
        {
            return View.GetComponent<T>(name);
        }
    }
}
