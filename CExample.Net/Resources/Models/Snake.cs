using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleX.CEngine;

namespace CExample
{
    internal class Snake : CGameObject
    {
        public Snake()
        {
            AddPixel(new CPixel()
            {
                X = 0,
                Y = 0,
                Symbol = "□",
            });
            AddPixel(new CPixel()
            {
                X = 1,
                Y = 0,
                Symbol = "▣",
            });
            AddPixel(new CPixel()
            {
                X = 2,
                Y = 0,
                Symbol = "▣",
            });
        }
    }
}
