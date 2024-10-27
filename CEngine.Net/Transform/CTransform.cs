namespace SimpleX.CEngine
{
    public class CTransform
    {
        /// <summary>
        /// 位置
        /// </summary>
        public Vector2 position { get; set; }

        /// <summary>
        /// 
        /// </summary>
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
            position = new Vector2(x, y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        public void Move(int dx, int dy)
        {
            position += new Vector2(dx, dy);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void MoveTo(int x, int y)
        {
            SetXY(x, y);
        }
    }
}
