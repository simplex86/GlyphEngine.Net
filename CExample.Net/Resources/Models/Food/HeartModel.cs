using SimpleX.CEngine;

namespace CExample
{
    internal class HeartModel : CGameObject
    {
        public HeartModel()
        {
            AddPixel(new CPixel());

            var skin = new CSkin("-");
            skin.Set(0, 0, "♥", ConsoleColor.Red);
            ApplySkin(skin);
        }
    }
}
