using System;
using System.Collections.Generic;

namespace SimpleX.CEngine
{
    /// <summary>
    /// 皮肤
    /// </summary>
    public sealed class CSkin
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
    }
}
