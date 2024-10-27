using SimpleX.CEngine;

namespace CExample
{
    internal class Snake
    {
        private SnakeModel gameObject = null;

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
            gameObject.transform.Move(dx, dy);

            for (int i = gameObject.count - 1; i > 0; i--)
            {
                var px = gameObject[i - 1].transform.position.x - dx;
                var py = gameObject[i - 1].transform.position.y - dy;
                gameObject[i].transform.SetXY(px, py);
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
            if (gameObject.transform.position == food.transform.position)
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
            var b = gameObject[-1].transform.position;
            var a = gameObject[-2].transform.position;
            var c = b + (b - a);

            gameObject.AddChild(new SnakeBodyModel(c.x, c.y));
            gameObject.UpdateSkin();
        }
    }
}
