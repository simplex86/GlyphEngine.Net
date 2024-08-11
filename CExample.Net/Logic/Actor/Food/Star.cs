using SimpleX.CEngine;

namespace CExample
{
    internal class Star : IFood
    {
        private CGameObject gameObject = null;

        public int x => gameObject.x;
        public int y => gameObject.y;

        public Star() : this(0, 0)
        {
            
        }

        public Star(int x, int y)
        {
            gameObject = CGameObject.Load<StarModel>(x, y);
        }

        public void SetXY(int x, int y)
        {
            gameObject.transform.SetXY(x, y);
        }
    }
}
