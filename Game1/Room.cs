using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Room
    {
        Random r = new Random();
        int rh;
        Rectangle ground;
        int gap;
        int t;
        int l;
        int m;
        int s;
        public Room(ref List<Rectangle> rlg, int ww, int wh)
        {
            t = wh / 24;
            l = ww / 2;
            m = ww / 4;
            s = ww / 8;
            gap = wh / 10 + ww / 26 + wh / 24;
            rlg.Add(ground = new Rectangle(0, wh - t, ww, wh));
        }
        public void Generate(ref List<Rectangle> rlg, ref List<Box> bl, ref List<Enemy> el, int ww, int wh)
        {
            rlg.Clear();
            rh = r.Next(2);
            if (rh == 0)
            {
                rlg.Add(ground);
                rlg.Add(new Rectangle(0, ground.Y - gap * 3, m, t));
                bl.Add(new Box(m - ww / 26, ground.Y - gap * 3 - ww / 26, ww));
                el.Add(new Enemy(0, ground.Y - gap * 3 - wh / 10, ww, wh));
                rlg.Add(new Rectangle(ww - m, ground.Y - gap * 3, m, t));
                bl.Add(new Box(ww - m, ground.Y - gap * 3 - ww / 26, ww));
                el.Add(new Enemy(ww - ww / 40, ground.Y - gap * 3 - wh / 10, ww, wh));
                rlg.Add(new Rectangle(ww / 2 - (l) / 2, ground.Y - gap, l, t));
                bl.Add(new Box(ww / 2 - (l) / 2, ground.Y - gap - ww / 26, ww));
                bl.Add(new Box(ww / 2 - (l) / 2 + l - ww / 26, ground.Y - gap - ww / 26, ww));
                rlg.Add(new Rectangle(0, ground.Y - gap, s, t));
                rlg.Add(new Rectangle(ww - s, ground.Y - gap, s, t));
                rlg.Add(new Rectangle(ww / 2 - (s) / 2, ground.Y - gap * 2, s, t));
                rlg.Add(new Rectangle(ww / 2 - (m) / 2, ground.Y - gap * 4, m, t));
                bl.Add(new Box(ww / 2 - (m) / 2, ground.Y - gap * 4 - ww / 26, ww));
                bl.Add(new Box(ww / 2 - (m) / 2 + m - ww / 26, ground.Y - gap * 4 - ww / 26, ww));
            }
            else
            {
                rlg.Add(new Rectangle(0, ground.Y, m, t));
                rlg.Add(new Rectangle(0 + m + s, ground.Y, m, t));
                rlg.Add(new Rectangle(0 + m * 2 + s * 2, ground.Y, m, t));
                rlg.Add(new Rectangle(ww / 2 - (s) / 2, ground.Y - gap, s, t));
                rlg.Add(new Rectangle(ww / 4 - (m) / 2, ground.Y - gap * 2, m, t));
                rlg.Add(new Rectangle(ww * 3 / 4 - (m) / 2, ground.Y - gap * 2, m, t));
                rlg.Add(new Rectangle(0, ground.Y - gap * 3, s, t));
                rlg.Add(new Rectangle(ww - s, ground.Y - gap * 3, m, t));
                rlg.Add(new Rectangle(ww / 2 - (l) / 2, ground.Y - gap * 4, l, t));
            }
        }
    }
}
