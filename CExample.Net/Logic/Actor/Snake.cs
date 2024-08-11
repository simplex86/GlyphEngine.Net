using SimpleX.CEngine;

namespace CExample
{
    internal class Snake
    {
        private SnakeModel gameObject = null;
        
        public  int x
        {
            get { return gameObject.x; }
            set { gameObject.x = value; }
        }
        public int y
        {
            get { return gameObject.y; }
            set { gameObject.y = value; }
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
            x += dx;
            y += dy;

            for (int i = gameObject.count - 1; i > 0; i--)
            {
                gameObject[i].x = gameObject[i - 1].x - dx;
                gameObject[i].y = gameObject[i - 1].y - dy;
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
            if (x == food.x && y == food.y)
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

            var x = b.x + (b.x - a.x);
            var y = b.y + (b.y - a.y);

            gameObject.AddChild(new SnakeBodyModel(x, y));
            gameObject.UpdateSkin();
        }
    }
}
