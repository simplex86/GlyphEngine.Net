using SimpleX.CEngine;

namespace CExample
{
    internal class Heart : IFood
    {
        private CGameObject gameObject = null;

        public int X => gameObject.X;
        public int Y => gameObject.Y;

        public Heart() : this(0, 0)
        {
            
        }

        public Heart(int x, int y)
        {
            gameObject = CGameObject.Load<HeartModel>(x, y);
        }

        public void SetXY(int x, int y)
        {
            gameObject.Transform.SetXY(x, y);
        }
    }
}
