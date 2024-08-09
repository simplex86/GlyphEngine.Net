using SimpleX.CEngine;

namespace CExample
{
    internal class StarModel : CGameObject
    {
        public StarModel()
        {
            AddPixel(new CPixel());

            var skin = new CSkin("-");
            skin.Set(0, 0, "✦", ConsoleColor.DarkYellow);
            ApplySkin(skin);
        }
    }
}
