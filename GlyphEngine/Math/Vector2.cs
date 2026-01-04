using System;
using System.Runtime.CompilerServices;

namespace GlyphEngine
{
    /// <summary>
    /// 二维向量
    /// </summary>
    public struct Vector2
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
        public static Vector2 Zero { get; } = new Vector2(0, 0);
        /// <summary>
        /// 
        /// </summary>
        public static Vector2 One { get; } = new Vector2(1, 1);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Vector2(int x, int y)
        {
            this.X = x; 
            this.Y = y;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        public Vector2(Vector2 v)
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
        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X + b.X, a.Y + b.Y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector2 Add(Vector2 a, Vector2 b)
        {
            return a + b;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X - b.X, a.Y - b.Y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector2 Sub(Vector2 a, Vector2 b)
        {
            return a - b;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(Vector2 a, Vector2 b)
        {
            return a.X == b.X && a.Y == b.Y;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator != (Vector2 a, Vector2 b)
        {
            return a.X != b.X || a.Y != b.Y;
        }
    }
}
