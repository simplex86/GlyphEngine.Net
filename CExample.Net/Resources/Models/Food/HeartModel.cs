using SimpleX.CEngine;

namespace CExample
{
    /// <summary>
    /// 
    /// </summary>
    internal class HeartModel : CGameObject
    {
        public HeartModel()
        {
            AddPixel(new CPixel());
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
            Set(0, 0, "♥", ConsoleColor.Red);
        }
    }
}
