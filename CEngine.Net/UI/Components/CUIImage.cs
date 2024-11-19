namespace SimpleX.CEngine.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class CUIImage : CUIComponent
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
                    // 重置像素列表
                    ClearPixels();
                    //
                    for (int h = 0; h < _texture.height; h++)
                    {
                        for (int w = 0; w < _texture.width; w++)
                        {
                            var i = h * _texture.width + w;
                            var pixel = CPixelPool.Instance.Alloc(w - _texture.width  / 2, 
                                                                  h - _texture.height / 2,
                                                                  _texture.chars[i]);
                            AddPixel(pixel);
                        }
                    }
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
        public CUIImage(CTexture tex, Vector2 position)
        {
            texture = tex;
            transform.position = position;
        }
    }
}
