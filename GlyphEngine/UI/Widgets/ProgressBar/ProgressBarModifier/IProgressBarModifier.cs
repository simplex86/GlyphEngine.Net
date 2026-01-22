using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlyphEngine
{
    /// <summary>
    /// 
    /// </summary>
    internal interface IProgressBarModifier
    {
        void Fill(IProgressBar progressbar);
        void Modify(IProgressBar progressbar);
    }
}
