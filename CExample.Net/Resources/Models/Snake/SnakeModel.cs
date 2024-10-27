using SimpleX.CEngine;

namespace CExample
{
    /// <summary>
    /// 
    /// </summary>
    internal class SnakeModel : CGameObject
    {
        public SnakeModel()
        {
            AddChild(new SnakeHeadModel());
            AddChild(new SnakeBodyModel(1, 0));
            AddChild(new SnakeBodyModel(2, 0));
            AddChild(new SnakeBodyModel(3, 0));
            AddChild(new SnakeBodyModel(4, 0));
            AddChild(new SnakeBodyModel(5, 0));

            UpdateSkin();
        }

        public void UpdateSkin()
        {
            UpdateHeadSkin();
            UpdateBodySkin();
            UpdateTailSkin();
        }

        private void UpdateHeadSkin()
        {
            //var head = GetChild(0);
            //var neck = GetChild(1);

            //var dx = neck.x - head.x;
            //var dy = neck.y - head.y;

            //if (dx == 1)
            //{
            //    head.ApplySkin("L");
            //}
            //else if (dx == -1)
            //{
            //    head.ApplySkin("R");
            //}
            //else if (dy == 1)
            //{
            //    head.ApplySkin("U");
            //}
            //else if (dy == -1)
            //{
            //    head.ApplySkin("D");
            //}

            var head = GetChild(0);
            head.ApplySkin("X");
        }

        private void UpdateBodySkin()
        {
            for (int i = 1; i < count - 1; i++)
            {
                var prev = GetChild(i - 1);
                var self = GetChild(i);
                var next = GetChild(i + 1);

                var p = self.transform.position - prev.transform.position;
                var n = next.transform.position - self.transform.position;

                if (p.x != 0 && n.x != 0)
                {
                    self.ApplySkin("H");
                }
                else if (p.y != 0 && n.y != 0)
                {
                    self.ApplySkin("V");
                }
                else if ((p.x ==  1 && n.y ==  1) ||
                         (p.y == -1 && n.x == -1))
                {
                    self.ApplySkin("RT");
                }
                else if ((p.x == 1 && n.y == -1) ||
                         (p.y == 1 && n.x == -1))
                {
                    self.ApplySkin("RB");
                }
                else if ((p.x == -1 && n.y == 1) ||
                         (p.y == -1 && n.x == 1))
                {
                    self.ApplySkin("LT");
                }
                else if ((p.x == -1 && n.y == -1) ||
                         (p.y ==  1 && n.x ==  1))
                {
                    self.ApplySkin("LB");
                }
            }
        }

        private void UpdateTailSkin()
        {
            var tail = GetChild(-1);
            var arse = GetChild(-2);

            var dv = tail.transform.position - arse.transform.position;

            if (dv.x != 0)
            {
                tail.ApplySkin("H");
            }
            else // if (dv.y != 0)
            {
                tail.ApplySkin("V");
            }
        }
    }
}
