namespace GlyphEngine
{
    /// <summary>
    /// 颜色
    /// </summary>
    public struct CColor
    {
        /// <summary>
        /// 红
        /// </summary>
        public byte R;
        /// <summary>
        /// 绿
        /// </summary>
        public byte G;
        /// <summary>
        /// 蓝
        /// </summary>
        public byte B;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        public CColor(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }

        /// <summary>
        /// 黑色
        /// </summary>
        public readonly static CColor Black = new CColor(12, 12, 12);
        /// <summary>
        /// 深蓝
        /// </summary>
        public readonly static CColor DarkBlue = new CColor(0, 55, 216);
        /// <summary>
        /// 深绿
        /// </summary>
        public readonly static CColor DarkGreen = new CColor(20, 165, 15);
        /// <summary>
        /// 
        /// </summary>
        public readonly static CColor DarkCyan = new CColor(58, 150, 220);
        /// <summary>
        /// 深红
        /// </summary>
        public readonly static CColor DarkRed = new CColor(207, 15, 32);
        /// <summary>
        /// 深紫
        /// </summary>
        public readonly static CColor DarkMagenta = new CColor(132, 23, 147);
        /// <summary>
        /// 深黄
        /// </summary>
        public readonly static CColor DarkYellow = new CColor(204, 164, 0);
        /// <summary>
        /// 深灰
        /// </summary>
        public readonly static CColor DarkGray = new CColor(118, 118, 118);
        /// <summary>
        /// 灰
        /// </summary>
        public readonly static CColor Gray = new CColor(208, 208, 208);
        /// <summary>
        /// 蓝
        /// </summary>
        public readonly static CColor Blue = new CColor(55, 110, 235);
        /// <summary>
        /// 绿
        /// </summary>
        public readonly static CColor Green = new CColor(23, 213, 12);
        /// <summary>
        /// 青/蓝绿
        /// </summary>
        public readonly static CColor Cyan = new CColor(87, 189, 189);
        /// <summary>
        /// 红
        /// </summary>
        public readonly static CColor Red = new CColor(228, 72, 85);
        /// <summary>
        /// 梅红
        /// </summary>
        public readonly static CColor Magenta = new CColor(108, 0, 158);
        /// <summary>
        /// 黄
        /// </summary>
        public readonly static CColor Yellow = new CColor(223, 216, 148);
        /// <summary>
        /// 白
        /// </summary>
        public readonly static CColor White = new CColor(247, 247, 247);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator==(CColor a, CColor b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(CColor a, CColor b)
        {
            return !a.Equals(b);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(CColor other)
        {
            return R == other.R &&
                   G == other.G && 
                   B == other.B;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            return Equals((CColor)other);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return (R << 16) | (G << 8) | B;
        }
    }
}
