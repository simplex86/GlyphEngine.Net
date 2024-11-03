using SimpleX.CEngine;

namespace CExample
{
    internal class SnakeScene : CScene
    {
        public SnakeScene()
        {
            var camera = new CCamera("SnakeCamera");
            camera.mask = (ulong)ERenderMask.Default;
            Add(camera);
        }
    }
}
