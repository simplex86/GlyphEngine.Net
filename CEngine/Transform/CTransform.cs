namespace CEngine
{
    /// <summary>
    /// 
    /// </summary>
    public class CTransform
    {
        /// <summary>
        /// 局部坐标
        /// </summary>
        public Vector2 localposition
        {
            set
            {
                if (localpos != value)
                {
                    localpos = value;
                    Reposition();
                }
            }
            get { return localpos; }
        }
        /// <summary>
        /// 世界坐标
        /// </summary>
        public Vector2 worldposition
        {
            set
            {
                if (worldpos != value)
                {
                    worldpos = value;
                    RepositionChildren();
                }
            }
            get { return worldpos; }
        }

        /// <summary>
        /// 
        /// </summary>
        private CGameObject gameobject = null;
        /// <summary>
        /// 
        /// </summary>
        private Vector2 localpos = Vector2.zero;
        /// <summary>
        /// 
        /// </summary>
        private Vector2 worldpos = Vector2.zero;

        /// <summary>
        /// 
        /// </summary>
        internal CTransform(CGameObject gameobject)
        {
            this.gameobject = gameobject;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        public void Move(int dx, int dy)
        {
            Move(new Vector2(dx, dy));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dv"></param>
        public void Move(Vector2 dv)
        {
            worldposition += dv;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void MoveTo(int x, int y)
        {
            MoveTo(new Vector2(x, y));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wpos"></param>
        public void MoveTo(Vector2 wpos)
        {
            worldposition = wpos;
        }

        /// <summary>
        /// 
        /// </summary>
        internal void Reposition()
        {
            var parent = gameobject.parent;
            worldposition = (parent == null) ? localpos 
                                             : parent.transform.worldpos + localpos;
        }

        /// <summary>
        /// 
        /// </summary>
        private void RepositionChildren()
        {
            for (int i = 0; i<gameobject.count; i++) 
            {
                var child = gameobject[i];
                child.transform.worldposition = worldpos + child.transform.localpos;
            }
        }
    }
}
