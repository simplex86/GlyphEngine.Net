using SimpleX.CEngine;

namespace CExample
{
    internal class Snake
    {
        private SnakeModel gameObject = null;
        
        public  int X
        {
            get { return gameObject.X; }
            set { gameObject.X = value; }
        }
        public int Y
        {
            get { return gameObject.Y; }
            set { gameObject.Y = value; }
        }

        public Snake() : this(0, 0)
        {

        }

        public Snake(int x, int y)
        {
            gameObject = CGameObject.Load<SnakeModel>(x, y);
        }

        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        public void Move(int dx, int dy)
        {
            X += dx;
            Y += dy;

            for (int i = gameObject.Count - 1; i > 0; i--)
            {
                gameObject[i].X = gameObject[i - 1].X - dx;
                gameObject[i].Y = gameObject[i - 1].Y - dy;
            }

            gameObject.UpdateSkin();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="food"></param>
        /// <returns></returns>
        public bool Eat(IFood food)
        {
            if (X == food.X && Y == food.Y)
            {
                AddBody();
                return true;
            }

            return false;
        }

        /// <summary>
        /// 增加身体节点
        /// </summary>
        private void AddBody()
        {
            var b = gameObject[-1];
            var a = gameObject[-2];

            var x = b.X + (b.X - a.X);
            var y = b.Y + (b.Y - a.Y);

            gameObject.AddChild(new SnakeBodyModel(x, y));
            gameObject.UpdateSkin();
        }
    }
}
