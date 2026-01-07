namespace GlyphEngine
{
    /// <summary>
    /// 
    /// </summary>
    public class CText : CWidget
    {
        /// <summary>
        /// 
        /// </summary>
        public string Text
        {
            set
            {
                if (text != value)
                {
                    text = value;
                    ResetPixels();

                    Width = text.Length;
                    Height = 1;
                }
            }
            get { return text; }
        }

        private string text = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        internal CText(string text, CVector2 localposition)
             : base()
        {
            Transform.LocalPosition = localposition;
            Text = text;
        }

        /// <summary>
        /// 
        /// </summary>
        private void ResetPixels()
        {
            // 重置像素列表
            GameObject.ClearPixels();
            // 
            for (int i = 0; i < text.Length; i++)
            {
                var pixel = CPixelPool.Instance.Alloc(i - text.Length / 2,
                                                      0,
                                                      text[i],
                                                      Color);
                GameObject.AddPixel(pixel);
            }
        }
    }
}
