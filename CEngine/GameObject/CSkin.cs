using System;
using System.Collections.Generic;

namespace CEngine
{
    /// <summary>
    /// 皮肤
    /// </summary>
    internal class CSkin
    {
        /// <summary>
        /// 
        /// </summary>
        public string name { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        private List<CPixel> list = new List<CPixel>();
        private Dictionary<ulong, int> grid = new Dictionary<ulong, int>();

        public CSkin(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="symbol"></param>
        /// <param name="color"></param>
        internal void Set(int x, int y, char c, ConsoleColor color)
        {
            if (!Get(x, y, out var key, out var pixel))
            {
                pixel = CPixelPool.Instance.Alloc(x, y);
                list.Add(pixel);
                grid.Add(key, list.Count - 1);
            }

            pixel.c = c;
            pixel.color = color;
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
                if (Get(pixel.x, pixel.y, out var _, out var p))
                {
                    pixel.c = p.c;
                    pixel.color = p.color;
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
            var clone = new CSkin(name);

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
