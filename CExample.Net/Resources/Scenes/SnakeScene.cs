using System;
using System.Collections.Generic;
using SimpleX.CEngine;

namespace CExample
{
    internal class SnakeScene : CScene
    {
        private Snake snake = null;

        public SnakeScene()
        {
            var camera = new CCamera("Snake Camera");
            Add(camera);

            snake = new Snake();
            Add(snake);

            snake.Transform.X = 5;
            snake.Transform.Y = 5;
        }

        protected override void OnEnter()
        {
            CInput.OnKeyDown += OnKeyDown;
        }

        protected override void OnExit()
        {
            CInput.OnKeyDown -= OnKeyDown;
        }

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

        private void Up()
        {
            snake.Transform.Y -= 1;
        }

        private void Down()
        {
            snake.Transform.Y += 1;
        }

        private void Left()
        {
            snake.Transform.X -= 1;
        }

        private void Right()
        {
            snake.Transform.X += 1;
        }
    }
}
