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
        public CButton startButton { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public LaunchUIView()
            : base()
        {
            startButton = new CButton("press space key to start",
                                      new Vector2(0, 12),
                                      ConsoleKey.Spacebar,
                                      ConsoleColor.White,
                                      ConsoleColor.White,
                                      CButton.Style.Borderless);
            AddFocusChild(startButton);
        }
    }
}
