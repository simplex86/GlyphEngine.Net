using SimpleX.CEngine;

namespace CExample
{
    internal class Heart : IFood
    {
        private CGameObject gameObject = null;

        public CTransform transform => gameObject.transform;

        public Heart() : this(0, 0)
        {
            
        }

        public Heart(int x, int y)
        {
            gameObject = CGameObject.Load<HeartModel>(x, y);
        }
    }
}
