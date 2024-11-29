namespace SimpleX.CEngine
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
                if (_localpos != value)
                {
                    _localpos = value;
                    Reposition();
                }
            }
            get { return _localpos; }
        }
        /// <summary>
        /// 世界坐标
        /// </summary>
        public Vector2 worldposition
        {
            set
            {
                if (_worldpos != value)
                {
                    _worldpos = value;
                    RepositionChildren();
                }
            }
            get { return _worldpos; }
        }

        /// <summary>
        /// 
        /// </summary>
        private CGameObject _gameobject = null;
        /// <summary>
        /// 
        /// </summary>
        private Vector2 _localpos = Vector2.zero;
        /// <summary>
        /// 
        /// </summary>
        private Vector2 _worldpos = Vector2.zero;

        /// <summary>
        /// 
        /// </summary>
        internal CTransform(CGameObject gameobject)
        {
            _gameobject = gameobject;
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
            var parent = _gameobject.parent;
            worldposition = (parent == null) ? _localpos 
                                             : parent.transform._worldpos + _localpos;
        }

        /// <summary>
        /// 
        /// </summary>
        private void RepositionChildren()
        {
            foreach (var child in _gameobject.children)
            {
                child.transform.worldposition = _worldpos + child.transform._localpos;
            }
        }
    }
}
