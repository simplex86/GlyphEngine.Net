using System;
using System.Runtime.CompilerServices;

namespace GlyphEngine
{
    /// <summary>
    /// 二维向量
    /// </summary>
    public struct CVector2
    {
        /// <summary>
        /// 
        /// </summary>
        public int X;
        /// <summary>
        /// 
        /// </summary>
        public int Y;

        /// <summary>
        /// 
        /// </summary>
        public static CVector2 Zero { get; } = new CVector2(0, 0);
        /// <summary>
        /// 
        /// </summary>
        public static CVector2 One { get; } = new CVector2(1, 1);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public CVector2(int x, int y)
        {
            this.X = x; 
            this.Y = y;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        public CVector2(CVector2 v)
        {
            this.X = v.X;
            this.Y = v.Y;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static CVector2 operator +(CVector2 a, CVector2 b)
        {
            return new CVector2(a.X + b.X, a.Y + b.Y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static CVector2 Add(CVector2 a, CVector2 b)
        {
            return a + b;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static CVector2 operator -(CVector2 a, CVector2 b)
        {
            return new CVector2(a.X - b.X, a.Y - b.Y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static CVector2 Sub(CVector2 a, CVector2 b)
        {
            return a - b;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(CVector2 a, CVector2 b)
        {
            return a.X == b.X && a.Y == b.Y;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator != (CVector2 a, CVector2 b)
        {
            return a.X != b.X || a.Y != b.Y;
        }
    }
}
