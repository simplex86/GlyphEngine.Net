namespace SimpleX.CEngine
{
    public class CTransform
    {
        /// <summary>
        /// X坐标
        /// </summary>
        public int x { get; set; }

        /// <summary>
        /// Y坐标
        /// </summary>
        public int y { get; set; }

        public CTransform()
        {
            SetXY(0, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetXY(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
