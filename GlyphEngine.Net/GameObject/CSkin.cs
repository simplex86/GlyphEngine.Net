using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

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
        internal void Set(int x, int y, char glyph, in CColor color)
        {
            var key = Key(x, y);
            if (grid.TryGetValue(key, out var index))
            {
                var span = CollectionsMarshal.AsSpan(list);
                span[index].Set(glyph, color);
            }
            else
            {
                list.Add(new CPixel(x, y, glyph, color));
                grid.Add(key, list.Count - 1);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameObject"></param>
        internal void Apply(IRenderable renderable)
        {
            var pixels = renderable.Pixels;
            for (int i = 0; i < pixels.Length; i++)
            {
                var key = Key(pixels[i].X, pixels[i].Y);
                if (grid.TryGetValue(key, out var index))
                {
                    var p = list[index];
                    pixels[i].Set(p.Glyph, p.Color);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private ulong Key(int x, int y)
        {
            var key = (ulong)x;
            key = (key << 32) | (ulong)y;

            return key;
        }

        /// <summary>
        /// 
        /// </summary>
        internal void Destroy()
        {
            list.Clear();
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
                //var clonepixel = pixel.Clone();
                clone.list.Add(pixel);
            }
            foreach (var kv in grid)
            {
                clone.grid.Add(kv.Key, kv.Value);
            }

            return clone;
        }
    }
}
