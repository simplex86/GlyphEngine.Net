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
        public CTexture texture
        {
            set
            {
                if (_texture != value)
                {
                    _texture = value;
                    ResetPixels();
                }
            }
            get { return _texture; }
        }

        /// <summary>
        /// 
        /// </summary>
        private CTexture _texture;

        /// <summary>
        /// 
        /// </summary>
        public CImage(CTexture tex, Vector2 localposition, ConsoleColor color)
        {
            this.color = color;
            this.texture = tex;
            this.Transform.LocalPosition = localposition;
        }

        /// <summary>
        /// 
        /// </summary>
        protected internal override void OnDestroy()
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
            ClearPixels();

            if (_texture == null) return;
            //
            for (int h = 0; h < _texture.Height; h++)
            {
                for (int w = 0; w < _texture.Width; w++)
                {
                    var i = h * _texture.Width + w;
                    var pixel = CPixelPool.Instance.Alloc(w - _texture.Width / 2,
                                                          h - _texture.Height / 2,
                                                          _texture.Glyphs[i],
                                                          color);
                    AddPixel(pixel);
                }
            }
        }
    }
}
