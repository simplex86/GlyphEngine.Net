using SimpleX.CEngine;

namespace CExample
{
    /// <summary>
    /// 
    /// </summary>
    internal class SnakeHeadModel : CGameObject
    {
        public SnakeHeadModel(ConsoleColor color = ConsoleColor.Gray)
        {
            AddPixel(new CPixel());

            var x = new CSkin("X");
            x.Set(0, 0, "◈", color);
            AddSkin(x);

            var u = new CSkin("U");
            u.Set(0, 0, "▲", color);
            AddSkin(u);

            var d = new CSkin("D");
            d.Set(0, 0, "▼", color);
            AddSkin(d);

            var l = new CSkin("L");
            l.Set(0, 0, "◀", color);
            AddSkin(l);

            var r = new CSkin("R");
            r.Set(0, 0, "▶", color);
            AddSkin(r);
        }
    }
}
