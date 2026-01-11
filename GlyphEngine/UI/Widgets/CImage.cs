using System;

namespace GlyphEngine
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
                    texture?.Destroy();
                    texture = value;
                    texture?.Refrence();

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
        public CImage(CTexture tex, CVector2 localposition, ConsoleColor color)
            : base(localposition)
        {
            this.Color = color;
            this.Texture = tex;
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnDestroy()
        {
            if (texture != null)
            {
                texture.Destroy();
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
            GameObject.ClearPixels();

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
                    GameObject.AddPixel(pixel);
                }
            }
        }
    }
}
