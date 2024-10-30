using SimpleX.CEngine;

namespace CExample
{
    /// <summary>
    /// 
    /// </summary>
    internal class SnakeHeadModel : CRenderableObject
    {
        public SnakeHeadModel()
        {
            AddPixel(new CPixel());
            LoadSkins();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [CSkinOf(typeof(SnakeHeadModel), true)]
    internal class SnakeHeadXSkin : CSkin
    {
        public SnakeHeadXSkin()
            : base("X")
        {
            Set(0, 0, "◈", ConsoleColor.Gray);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [CSkinOf(typeof(SnakeHeadModel))]
    internal class SnakeHeadUSkin : CSkin
    {
        public SnakeHeadUSkin()
            : base("U")
        {
            Set(0, 0, "▲", ConsoleColor.Gray);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [CSkinOf(typeof(SnakeHeadModel))]
    internal class SnakeHeadDSkin : CSkin
    {
        public SnakeHeadDSkin()
            : base("D")
        {
            Set(0, 0, "▼", ConsoleColor.Gray);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [CSkinOf(typeof(SnakeHeadModel))]
    internal class SnakeHeadLSkin : CSkin
    {
        public SnakeHeadLSkin()
            : base("L")
        {
            Set(0, 0, "◀", ConsoleColor.Gray);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [CSkinOf(typeof(SnakeHeadModel))]
    internal class SnakeHeadRSkin : CSkin
    {
        public SnakeHeadRSkin()
            : base("R")
        {
            Set(0, 0, "▶", ConsoleColor.Gray);
        }
    }
}
