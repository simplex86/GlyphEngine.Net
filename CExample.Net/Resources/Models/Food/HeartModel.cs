using SimpleX.CEngine;

namespace CExample
{
    /// <summary>
    /// 
    /// </summary>
    internal class HeartModel : CRenderableObject
    {
        public HeartModel()
        {
            AddPixel(0, 0);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [CSkinOf(typeof(HeartModel), true)]
    internal class HeartSkin : CSkin
    {
        public HeartSkin()
            : base("HEART")
        {
            Set(0, 0, '♥', ConsoleColor.Red);
        }
    }
}
