using SimpleX.CEngine;

namespace CExample
{
    [ApplicationEntry]
    internal class Application : IApplication
    {
        private Snake snake = null;
        private IFood food = null;
        private int dx = -1;
        private int dy = 0;
        private float timer = 0;

        private const float INTERVAL = 0.15f;

        /// <summary>
        /// 
        /// </summary>
        public void Start()
        {
            var scene = CSceneManager.Load<SnakeScene>();

            snake = new Snake(5, 5);
            food = new Star();
            RandomFoodXY();

            CInput.OnKeyDown += OnKeyDown;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        public void Update(float dt)
        {
            timer += dt;
            if (timer >= INTERVAL)
            {
                snake.Move(dx, dy);
                if (snake.Eat(food))
                {
                    RandomFoodXY();
                }
                timer -= INTERVAL;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Exit()
        {
            CInput.OnKeyDown -= OnKeyDown;
        }

        /// <summary>
        /// 随机设置food的坐标
        /// </summary>
        private void RandomFoodXY()
        {
            var w = CWorld.width / 4;
            var h = CWorld.height / 4;

            var x = Random.Shared.Next(-w, w);
            var y = Random.Shared.Next(-h, h);
            food.transform.SetXY(x, y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        private void OnKeyDown(int key)
        {
            switch (key)
            {
                case (int)ConsoleKey.W:
                    Up();
                    break;
                case (int)ConsoleKey.S:
                    Down();
                    break;
                case (int)ConsoleKey.A:
                    Left();
                    break;
                case (int)ConsoleKey.D:
                    Right();
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void Up()
        {
            if (dy == 0)
            {
                dx = 0;
                dy = -1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void Down()
        {
            if (dy == 0)
            {
                dx = 0;
                dy = 1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void Left()
        {
            if (dx == 0)
            {
                dx = -1;
                dy = 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void Right()
        {
            if (dx == 0)
            {
                dx = 1;
                dy = 0;
            }
        }
    }
}
