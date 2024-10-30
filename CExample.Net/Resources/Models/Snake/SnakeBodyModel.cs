using SimpleX.CEngine;

namespace CExample
{
    /// <summary>
    /// 
    /// </summary>
    internal class SnakeBodyModel : CRenderableObject
    {
        public SnakeBodyModel(int x, int y, ConsoleColor color = ConsoleColor.Gray)
        {
            AddPixel(new CPixel());
            LoadSkins();

            transform.SetXY(x, y);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [CSkinOf(typeof(SnakeBodyModel))]
    internal class SnakeBodyVSkin : CSkin
    {
        public SnakeBodyVSkin()
            : base("V")
        {
            Set(0, 0, "│", ConsoleColor.Gray);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [CSkinOf(typeof(SnakeBodyModel))]
    internal class SnakeBodyHSkin : CSkin
    {
        public SnakeBodyHSkin()
            : base("H")
        {
            Set(0, 0, "─", ConsoleColor.Gray);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [CSkinOf(typeof(SnakeBodyModel))]
    internal class SnakeBodyLTSkin : CSkin
    {
        public SnakeBodyLTSkin()
            : base("LT")
        {
            Set(0, 0, "┌", ConsoleColor.Gray);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [CSkinOf(typeof(SnakeBodyModel))]
    internal class SnakeBodyRTSkin : CSkin
    {
        public SnakeBodyRTSkin()
            : base("RT")
        {
            Set(0, 0, "┐", ConsoleColor.Gray);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [CSkinOf(typeof(SnakeBodyModel))]
    internal class SnakeBodyLBSkin : CSkin
    {
        public SnakeBodyLBSkin()
            : base("LB")
        {
            Set(0, 0, "└", ConsoleColor.Gray);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [CSkinOf(typeof(SnakeBodyModel))]
    internal class SnakeBodyRBSkin : CSkin
    {
        public SnakeBodyRBSkin()
            : base("RB")
        {
            Set(0, 0, "┘", ConsoleColor.Gray);
        }
    }
}
