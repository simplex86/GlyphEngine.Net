using SimpleX.CEngine;

namespace CExample
{
    /// <summary>
    /// 
    /// </summary>
    internal class SnakeBodyModel : CGameObject
    {
        public SnakeBodyModel(int x, int y, ConsoleColor color = ConsoleColor.Gray)
        {
            AddPixel(new CPixel());
            Transform.SetXY(x, y);

            var v = new CSkin("V");
            v.Set(0, 0, "│", color);
            AddSkin(v);

            var h = new CSkin("H");
            h.Set(0, 0, "─", color);
            AddSkin(h);

            var lt = new CSkin("LT");
            lt.Set(0, 0, "┌", color);
            AddSkin(lt);

            var rt = new CSkin("RT");
            rt.Set(0, 0, "┐", color);
            AddSkin(rt);

            var lb = new CSkin("LB");
            lb.Set(0, 0, "└", color);
            AddSkin(lb);

            var rb = new CSkin("RB");
            rb.Set(0, 0, "┘", color);
            AddSkin(rb);
        }
    }
}
