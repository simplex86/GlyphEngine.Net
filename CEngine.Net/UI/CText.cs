using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleX.CEngine.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class CText : CUIElement
    {
        /// <summary>
        /// 
        /// </summary>
        public string Text { get; set; }

        public CText()
        {

        }

        public CText(string text)
        {
            Text = text;
        }
    }
}
