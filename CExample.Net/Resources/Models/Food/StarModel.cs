using SimpleX.CEngine;

namespace CExample
{
    /// <summary>
    /// 
    /// </summary>
    internal class StarModel : CRenderableObject
    {
        public StarModel()
        {
            AddPixel(new CPixel());
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [CSkinOf(typeof(StarModel), true)]
    internal class StarSkin : CSkin
    {
        public StarSkin()
            : base("STAR")
        {
            Set(0, 0, "✦", ConsoleColor.DarkYellow);
        }
    }
}
