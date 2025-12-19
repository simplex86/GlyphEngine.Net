using System;

namespace CEngine.UI
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
            this.transform.localposition = localposition;
        }

        /// <summary>
        /// 
        /// </summary>
        protected internal override void OnDestroy()
        {
            if (texture != null)
            {
                CResourceManager.UnloadTex(texture);
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
            for (int h = 0; h < _texture.height; h++)
            {
                for (int w = 0; w < _texture.width; w++)
                {
                    var i = h * _texture.width + w;
                    var pixel = CPixelPool.Instance.Alloc(w - _texture.width / 2,
                                                          h - _texture.height / 2,
                                                          _texture.chars[i],
                                                          color);
                    AddPixel(pixel);
                }
            }
        }
    }
}
