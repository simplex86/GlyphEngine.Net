using SimpleX.CEngine.UI;

namespace CExample
{
    /// <summary>
    /// 
    /// </summary>
    internal class LaunchProcedure : IProcedure
    {
        /// <summary>
        /// 
        /// </summary>
        public void Enter()
        {
            CUIManager.Open<LaunchUI>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        public void Update(float dt)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public void Exit()
        {
            CUIManager.Close<LaunchUI>();
        }
    }
}
