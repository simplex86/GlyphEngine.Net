using System;
using System.Collections.Generic;

namespace GlyphEngine
{
    /// <summary>
    /// 皮肤
    /// </summary>
    public sealed partial class CSkin
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        private List<CPixel> list = new List<CPixel>();
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<ulong, int> grid = new Dictionary<ulong, int>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        internal CSkin(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="symbol"></param>
        /// <param name="color"></param>
        internal void Set(int x, int y, char glyph, ConsoleColor color)
        {
            if (!Get(x, y, out var key, out var pixel))
            {
                pixel = CPixelPool.Instance.Alloc(x, y);
                list.Add(pixel);
                grid.Add(key, list.Count - 1);
            }

            pixel.Glyph = glyph;
            pixel.Color = color;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="pixel"></param>
        /// <returns></returns>
        private bool Get(int x, int y, out ulong key, out CPixel pixel)
        {
            pixel = null;

            key = (ulong)x;
            key = (key << 32) | (ulong)y;

            if (grid.TryGetValue(key, out var index))
            {
                pixel = list[index];
                return true;
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
                if (Get(pixel.X, pixel.Y, out var _, out var p))
                {
                    pixel.Glyph = p.Glyph;
                    pixel.Color = p.Color;
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        internal void Destroy()
        {
            CPixelPool.Instance.Release(list);
            grid.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal CSkin Clone()
        {
            var clone = new CSkin(Name);

            foreach (var pixel in list)
            {
                var clonepixel = pixel.Clone();
                clone.list.Add(clonepixel);
            }
            foreach (var kv in grid)
            {
                clone.grid.Add(kv.Key, kv.Value);
            }

            return clone;
        }
    }
}
