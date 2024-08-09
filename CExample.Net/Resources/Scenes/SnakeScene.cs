using System;
using System.Collections.Generic;
using SimpleX.CEngine;

namespace CExample
{
    internal class SnakeScene : CScene
    {
        public SnakeScene()
        {
            var camera = new CCamera("Snake Camera");
            Add(camera);
        }
    }
}
