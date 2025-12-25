namespace CEngine
{
    /// <summary>
    /// 
    /// </summary>
    public class CText : CWidget
    {
        /// <summary>
        /// 
        /// </summary>
        public string text
        {
            set
            {
                if (_text != value)
                {
                    _text = value;
                    ResetPixels();

                    Width = _text.Length;
                    Height = 1;
                }
            }
            get { return _text; }
        }

        private string _text = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public CText(Vector2 localposition) 
            : this("Text", localposition)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        public CText(string text, Vector2 localposition)
             : base()
        {
            Transform.LocalPosition = localposition;
            this.text = text;
        }

        /// <summary>
        /// 
        /// </summary>
        private void ResetPixels()
        {
            // 重置像素列表
            ClearPixels();
            // 
            for (int i = 0; i < _text.Length; i++)
            {
                var pixel = CPixelPool.Instance.Alloc(i - _text.Length / 2,
                                                      0,
                                                      _text[i],
                                                      color);
                AddPixel(pixel);
            }
        }
    }
}
