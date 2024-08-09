using System;
using System.Collections.Generic;

namespace SimpleX.CEngine
{
    public class CTransform
    {
        /// <summary>
        /// X坐标
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Y坐标
        /// </summary>
        public int Y { get; set; }

        public CTransform()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetXY(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
