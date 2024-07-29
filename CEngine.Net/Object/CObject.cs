using System;
using System.Collections.Generic;

namespace SimpleX.CEngine
{
    /// <summary>
    /// 
    /// </summary>
    public class CObject
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; } = "CObject";
        /// <summary>
        /// X坐标
        /// </summary>
        public int X
        {
            set
            {
                if (x != value)
                {
                    x = value;
                    dirty = true;
                }
            }
            get { return x; }
        }
        /// <summary>
        /// Y坐标
        /// </summary>
        public int Y
        {
            set
            {
                if (y != value)
                {
                    y = value;
                    dirty = true;
                }
            }
            get { return y; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// 
        /// </summary>
        internal List<CPixel> pixels = new List<CPixel>();
        /// <summary>
        /// 
        /// </summary>
        internal bool dirty { get; set; } = true;

        private int x = 0;
        private int y = 0;

        public CObject()
            : this(0, 0)
        {

        }

        public CObject(int x, int y)
        {
            X = x;
            Y = y;
        }

        public CObject(string name, int x, int y)
            : this(x, y)
        {
            Name = name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pixel"></param>
        public void AddPixel(CPixel pixel)
        {
            pixels.Add(pixel);
            dirty = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public void RemovePixel(int index)
        {
            pixels.RemoveAt(index);
            dirty = true;
        }
    }
}
