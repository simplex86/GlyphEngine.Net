using System;

namespace CEngine
{
    /// <summary>
    /// 
    /// </summary>
    public class CImage : CWidget
    {
        /// <summary>
        /// 
        /// </summary>
        public CTexture Texture
        {
            set
            {
                if (texture != value)
                {
                    texture = value;
                    ResetPixels();
                }
            }
            get { return texture; }
        }

        /// <summary>
        /// 
        /// </summary>
        private CTexture texture;

        /// <summary>
        /// 
        /// </summary>
        public CImage(CTexture tex, Vector2 localposition, ConsoleColor color)
            : base()
        {
            this.Color = color;
            this.Texture = tex;
            this.Transform.LocalPosition = localposition;
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnDestroy()
        {
            if (texture != null)
            {
                CResources.UnloadTex(texture);
                texture = null;
            }
            base.OnDestroy();
        }

        /// <summary>
        /// 
        /// </summary>
        private void ResetPixels()
        {
            // 重置像素列表
            view.ClearPixels();

            if (texture == null) return;
            //
            for (int h = 0; h < texture.Height; h++)
            {
                for (int w = 0; w < texture.Width; w++)
                {
                    var i = h * texture.Width + w;
                    var pixel = CPixelPool.Instance.Alloc(w - texture.Width / 2,
                                                          h - texture.Height / 2,
                                                          texture.Glyphs[i],
                                                          Color);
                    view.AddPixel(pixel);
                }
            }
        }
    }
}
