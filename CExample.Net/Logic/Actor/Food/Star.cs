using SimpleX.CEngine;

namespace CExample
{
    internal class Star : IFood
    {
        private CGameObject gameObject = null;

        public CTransform transform => gameObject.transform;

        public Star() : this(0, 0)
        {
            
        }

        public Star(int x, int y)
        {
            gameObject = CGameObject.Load<StarModel>(x, y);
        }
    }
}
