using System;
using System.Collections.Generic;

namespace SimpleX.CEngine
{
    /// <summary>
    /// 相机
    /// </summary>
    public class CCamera
    {
        /// <summary>
        /// 渲染顺序，值越小越先被渲染
        /// </summary>
        public int Order { get; }
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
        public string Tag { get; }
        /// <summary>
        /// 宽度
        /// </summary>
        internal int width { get; } = Console.BufferWidth;
        /// <summary>
        /// 高度
        /// </summary>
        internal int height { get; } = Console.BufferHeight;
        /// <summary>
        /// 
        /// </summary>
        internal bool dirty { get; private set; } = true;

        private int x = 0;
        private int y = 0;
        /// <summary>
        /// 
        /// </summary>
        private CRenderBuffer buffer { get; } = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        internal CCamera(string tag, int order, CRenderBuffer buffer)
        {
            Tag = tag;
            Order = order;

            this.x = 0;
            this.y = 0;
            this.buffer = buffer;
        }

        /// <summary>
        /// 渲染
        /// </summary>
        /// <param name="objects"></param>
        internal void Render(List<CObject> objects)
        {
            foreach (CObject o in objects)
            {
                if (o.Enabled)
                {
                    RenderObject(o);
                }
            }

            dirty = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="cobject"></param>
        private void RenderObject(CObject cobject)
        {
            var ox = cobject.X;
            var oy = cobject.Y;

            foreach (var pixel in cobject.pixels)
            {
                if (Cull(pixel))
                {
                    var x = ox + pixel.X - X;
                    var y = oy + pixel.Y - Y;
                    buffer.SetPixel(x, y, pixel.Value, pixel.Color);
                }
            }

            cobject.dirty = false;
        }

        private bool Cull(CPixel pixel)
        {
            return true;
        }
    }
}
