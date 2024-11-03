using SimpleX.CEngine;
using SimpleX.CEngine.UI;

namespace CExample
{
    /// <summary>
    /// 
    /// </summary>
    public class LaunchUIView : CPanelView
    {
        /// <summary>
        /// 
        /// </summary>
        public LaunchUIView()
            : base(60, 20)
        {
            var text = new CText("press space key to start");
            text.transform.position = new Vector2(0, 7);
            AddChild(text);
        }
    }
}
