using System;
using System.Runtime.CompilerServices;

namespace CEngine
{
    /// <summary>
    /// 二维向量
    /// </summary>
    public struct Vector2
    {
        /// <summary>
        /// 
        /// </summary>
        public int x;
        /// <summary>
        /// 
        /// </summary>
        public int y;

        /// <summary>
        /// 
        /// </summary>
        public static Vector2 zero { get; } = new Vector2(0, 0);
        /// <summary>
        /// 
        /// </summary>
        public static Vector2 one { get; } = new Vector2(1, 1);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Vector2(int x, int y)
        {
            this.x = x; 
            this.y = y;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        public Vector2(Vector2 v)
        {
            this.x = v.x;
            this.y = v.y;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x + b.x, a.y + b.y);
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
            return new Vector2(a.x - b.x, a.y - b.y);
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
            return a.x == b.x && a.y == b.y;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator != (Vector2 a, Vector2 b)
        {
            return a.x != b.x || a.y != b.y;
        }
    }
}
