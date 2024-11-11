namespace SimpleX.CEngine.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class CText : CUIComponent
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
                    // 重置像素列表
                    ClearPixels();
                    // 
                    for (int i = 0; i < _text.Length; i++)
                    {
                        var pixel = CPixelPool.Instance.Alloc(i - _text.Length / 2, 
                                                              0,
                                                              _text[i]);
                        AddPixel(pixel);
                    }
                }
            }
            get { return _text; }
        }

        private string _text = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public CText() 
            : base()
        {
            this.text = "Text";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        public CText(string text)
             : base()
        {
            this.text = text;
        }
    }
}
