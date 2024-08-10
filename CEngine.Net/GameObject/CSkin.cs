using System;
using System.Collections.Generic;

namespace SimpleX.CEngine
{
    /// <summary>
    /// 皮肤
    /// </summary>
    public class CSkin
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        internal List<CPixel> pixels = new List<CPixel>();

        public CSkin(string name)
        {
            Name = name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="symbol"></param>
        /// <param name="color"></param>
        public void Set(int x, int y, string symbol, ConsoleColor color)
        {
            if (Get(x, y, out var pixel))
            {
                pixel.Symbol = symbol;
                pixel.Color = color;
            }
            else
            {
                pixels.Add(new CPixel()
                {
                    X = x,
                    Y = y,
                    Symbol = symbol,
                    Color = color
                });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="pixel"></param>
        /// <returns></returns>
        public bool Get(int x, int y, out CPixel pixel)
        {
            pixel = null;

            foreach (CPixel p in pixels)
            {
                if (p.X == x && p.Y == y)
                {
                    pixel = new CPixel()
                    {
                        X = p.X,
                        Y = p.Y,
                        Symbol = p.Symbol,
                        Color = p.Color
                    };

                    return true;
                }
            }
            
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameObject"></param>
        public void Apply(CGameObject gameObject)
        {
            foreach (var pixel in gameObject.pixels)
            {
                if (Get(pixel.X, pixel.Y, out var p))
                {
                    pixel.Symbol = p.Symbol;
                    pixel.Color  = p.Color;
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CSkinOfAttribute : Attribute
    {
        private Type type = null;

        /// <summary>
        /// 加载后生效
        /// </summary>
        internal bool applied { get; } = false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="applied"></param>
        public CSkinOfAttribute(Type type, bool applied = false)
        {
            this.type = type;
            this.applied = applied;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool Is(Type type)
        {
            return this.type == type;
        }
    }
}
