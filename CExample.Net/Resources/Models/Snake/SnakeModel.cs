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

            //var dx = neck.X - head.X;
            //var dy = neck.Y - head.Y;

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
            for (int i = 1; i < Count - 1; i++)
            {
                var prev = GetChild(i - 1);
                var self = GetChild(i);
                var next = GetChild(i + 1);

                var px = self.X - prev.X;
                var py = self.Y - prev.Y;
                var nx = next.X - self.X;
                var ny = next.Y - self.Y;

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

            var dx = tail.X - arse.X;
            var dy = tail.Y - arse.Y;

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
