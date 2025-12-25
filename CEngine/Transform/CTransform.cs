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
        public Vector2 LocalPosition
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
        public Vector2 WorldPosition
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
        private Vector2 localpos = Vector2.Zero;
        /// <summary>
        /// 
        /// </summary>
        private Vector2 worldpos = Vector2.Zero;

        /// <summary>
        /// 
        /// </summary>
        internal CTransform()
            : this(null)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameobject"></param>
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
            WorldPosition += dv;
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
            WorldPosition = wpos;
        }

        /// <summary>
        /// 
        /// </summary>
        internal void Reposition()
        {
            if (gameobject == null) return;

            var parent = gameobject.Parent;
            WorldPosition = (parent == null) ? LocalPosition 
                                             : parent.Transform.WorldPosition + LocalPosition;
        }

        /// <summary>
        /// 
        /// </summary>
        private void RepositionChildren()
        {
            if (gameobject == null) return;

            for (int i = 0; i<gameobject.Count; i++) 
            {
                var child = gameobject[i];
                child.Transform.WorldPosition = worldpos + child.Transform.localpos;
            }
        }
    }
}
