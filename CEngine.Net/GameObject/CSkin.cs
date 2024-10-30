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
                pixel.symbol = symbol;
                pixel.color = color;
            }
            else
            {
                pixels.Add(new CPixel()
                {
                    x = x,
                    y = y,
                    symbol = symbol,
                    color = color
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
                if (p.x == x && p.y == y)
                {
                    pixel = new CPixel()
                    {
                        x = p.x,
                        y = p.y,
                        symbol = p.symbol,
                        color = p.color
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
        internal void Apply(IRenderable renderable)
        {
            renderable.Foreach(pixel =>
            {
                if (Get(pixel.x, pixel.y, out var p))
                {
                    pixel.symbol = p.symbol;
                    pixel.color = p.color;
                }
            });
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
