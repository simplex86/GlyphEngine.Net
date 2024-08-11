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

                var px = self.x - prev.x;
                var py = self.y - prev.y;
                var nx = next.x - self.x;
                var ny = next.y - self.y;

                if (px != 0 && nx != 0)
                {
                    self.ApplySkin("H");
                }
                else if (py != 0 && ny != 0)
                {
                    self.ApplySkin("V");
                }
                else if ((px ==  1 && ny ==  1) ||
                         (py == -1 && nx == -1))
                {
                    self.ApplySkin("RT");
                }
                else if ((px == 1 && ny == -1) ||
                         (py == 1 && nx == -1))
                {
                    self.ApplySkin("RB");
                }
                else if ((px == -1 && ny == 1) ||
                         (py == -1 && nx == 1))
                {
                    self.ApplySkin("LT");
                }
                else if ((px == -1 && ny == -1) ||
                         (py ==  1 && nx ==  1))
                {
                    self.ApplySkin("LB");
                }
            }
        }

        private void UpdateTailSkin()
        {
            var tail = GetChild(-1);
            var arse = GetChild(-2);

            var dx = tail.x - arse.x;
            var dy = tail.y - arse.y;

            if (dx != 0)
            {
                tail.ApplySkin("H");
            }
            else // if (dy != 0)
            {
                tail.ApplySkin("V");
            }
        }
    }
}
