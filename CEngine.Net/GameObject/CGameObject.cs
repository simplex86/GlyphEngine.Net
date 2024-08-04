using System;
using System.Collections.Generic;

namespace SimpleX.CEngine
{
    /// <summary>
    /// 
    /// </summary>
    public class CGameObject
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; } = "GameObject";
        /// <summary>
        /// 可见性
        /// </summary>
        public bool Enabled { get; set; } = true;
        public CTransform Transform { get; }

        /// <summary>
        /// 
        /// </summary>
        internal List<CPixel> pixels { get; } = new List<CPixel>();

        public CGameObject()
            : this(0, 0)
        {

        }

        public CGameObject(string name)
            : this(0, 0)
        {
            Name = name;
        }

        public CGameObject(int x, int y)
        {
            Transform = new CTransform()
            {
                X = x,
                Y = y,
            };
        }

        public CGameObject(string name, int x, int y)
            : this(x, y)
        {
            Name = name;
        }

        public static CGameObject CreatePrimitive()
        {
            // TODO
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pixel"></param>
        protected void AddPixel(CPixel pixel)
        {
            pixels.Add(pixel);
        }
    }
}
