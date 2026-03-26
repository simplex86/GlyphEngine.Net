namespace GlyphEngine
{
    /// <summary>
    /// 
    /// </summary>
    public class CTransform
    {
        /// <summary>
        /// 局部坐标
        /// </summary>
        public CVector2 LocalPosition
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
        public CVector2 WorldPosition
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
        private CVector2 localpos = CVector2.Zero;
        /// <summary>
        /// 
        /// </summary>
        private CVector2 worldpos = CVector2.Zero;

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
            Move(new CVector2(dx, dy));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dv"></param>
        public void Move(CVector2 dv)
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
            MoveTo(new CVector2(x, y));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wpos"></param>
        public void MoveTo(CVector2 wpos)
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
