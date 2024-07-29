using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleX.CEngine;

namespace CExample
{
    internal class SnakeScene : CScene
    {
        public SnakeScene()
        {
            var snake = Create<Snake>();
            snake.X = 5;
            snake.Y = 5;
        }
    }
}
