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
        private CImage logoImage;

        /// <summary>
        /// 
        /// </summary>
        public LaunchUIView()
            : base()
        {
            var texture = new CTexture("Textures/snake_12x12.tex");

            //
            logoImage = new CImage(texture,
                                   new Vector2(0, -5));
            AddChild(logoImage);
            //
            startButton = new CButton("press space key to start",
                                      new Vector2(0, 10),
                                      ConsoleKey.Spacebar,
                                      ConsoleColor.White,
                                      ConsoleColor.White,
                                      CButton.Style.Borderless);
            AddFocusChild(startButton);
        }
    }
}
