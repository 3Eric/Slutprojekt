using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
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
        int roomCounter;
        int a;
        int b;
        int rh;
        Rectangle ground;
        Rectangle door;
        bool e;
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
            door = new Rectangle(ww - ww / 30, ground.Y - wh / 9, ww / 30, wh / 9);
            a = 3;
            b = 10;
        }
        public bool Next(Player p, List<Enemy> el, KeyboardState kstate)
        {
            if (p.P.Intersects(door) && el.Count == 0)
            {
                e = true;
                if (kstate.IsKeyDown(Keys.E))
                {
                    return true;
                }
            }
            else
            {
                e = false;
            }
            return false;
        }
        /// <summary>
        /// Skapar ett nytt rum
        /// </summary>
        public void Generate(ref Player p, ref List<Rectangle> rlg, ref List<Box> bl, ref List<Chest> cl, ref List<Loot> ll, ref List<Enemy> el, int ww, int wh)
        {
            roomCounter++;
            if (roomCounter == b)
            {
                b *= 10;
                a++;
            }
            p.RectangleStuff(0, p.P.Y, p.P.Width, p.P.Height);
            p.RectangleStuff(p.P.X, ground.Y - p.P.Height, p.P.Width, p.P.Height);
            rlg.Clear();
            el.Clear();
            bl.Clear();
            cl.Clear();
            ll.Clear();
            rh = r.Next(2);
            if (roomCounter % 5 == 0 || rh > 0)
            {
                cl.Add(new Chest(ww / 2 - ww / 20 / 2, Ground.Y - gap * 2 - ww / 26, ww));
            }
            if (roomCounter == 10)
            {
                rlg.Add(ground);
                rlg.Add(new Rectangle(s, ground.Y - gap, s, t));
                bl.Add(new Box(m - ww / 26, ground.Y - gap - ww / 26, ww));
                rlg.Add(new Rectangle(l - s, ground.Y - gap, s, t));
                bl.Add(new Box(l - ww / 26, ground.Y - gap - ww / 26, ww));
                rlg.Add(new Rectangle(ww - m - s, ground.Y - gap, s, t));
                bl.Add(new Box(l + m - ww / 26, ground.Y - gap - ww / 26, ww));
                rlg.Add(new Rectangle(ww - s, ground.Y - gap, s, t));
                rlg.Add(new Rectangle(0, ground.Y - gap * 2, l, t));
                rlg.Add(new Rectangle(l, ground.Y - gap * 2, m, t));
                rlg.Add(new Rectangle(0, ground.Y - gap * 3, s, t));
                rlg.Add(new Rectangle(ww - l, ground.Y - gap * 4, l, t));
                rlg.Add(new Rectangle(m, ground.Y - gap * 4, m, t));
                rlg.Add(new Rectangle(ww - s, ground.Y - gap * 5, s, t));
                cl.Add(new Chest(ww - ww / 20, Ground.Y - gap * 5 - ww / 26, ww));
                for (int i = 0; i < 11; i++)
                {
                    el.Add(new Enemy(ww - i * 2 * ww / 25, ground.Y - gap * 4 - wh / 10, ww, wh));
                }
            }
            else
            {
                rh = r.Next(3);
                // ground
                if (rh == 0)
                {
                    rlg.Add(ground);
                    rh = r.Next(2);
                    if (rh == 0)
                    {
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
                        rh = r.Next(2);
                        if (rh == 0)
                        {
                            rlg.Add(new Rectangle(0, ground.Y - gap, s, t));
                            rlg.Add(new Rectangle(ww - s, ground.Y - gap, s, t));
                            rh = r.Next(2);
                            if (rh == 0)
                            {
                                rlg.Add(new Rectangle(ww / 2 - (l) / 2, ground.Y - gap, l, t));
                            }
                            else
                            {
                                rlg.Add(new Rectangle(ww / 2 - (m) / 2, ground.Y - gap, m, t));
                            }
                            rh = r.Next(2);
                            if (rh == 0)
                            {
                                rlg.Add(new Rectangle(ww / 2 - (s) / 2, ground.Y - gap * 2, s, t));
                            }
                            else
                            {
                                rlg.Add(new Rectangle(ww / 2 - (m) / 2, ground.Y - gap * 2, m, t));
                            }
                        }
                        else
                        {
                            rlg.Add(new Rectangle(0, ground.Y - gap, m, t));
                            rlg.Add(new Rectangle(ww - m, ground.Y - gap, m, t));
                            rh = r.Next(2);
                            if (rh == 0)
                            {
                                rlg.Add(new Rectangle(ww / 2 - (s) / 2, ground.Y - gap, s, t));
                            }
                            rh = r.Next(2);
                            if (rh == 0)
                            {
                                rlg.Add(new Rectangle(ww / 2 - (m) / 2, ground.Y - gap * 2, m, t));
                            }
                            else
                            {
                                rlg.Add(new Rectangle(ww / 2 - (l) / 2, ground.Y - gap * 2, l, t));
                            }
                        }
                    }
                }
                // two holes
                else if (rh == 1)
                {
                    rlg.Add(new Rectangle(0, ground.Y, m, t));
                    rlg.Add(new Rectangle(0 + m + s, ground.Y, m, t));
                    rlg.Add(new Rectangle(0 + m * 2 + s * 2, ground.Y, m, t));
                    rlg.Add(new Rectangle(0, ground.Y - gap, s, t));
                    rlg.Add(new Rectangle(ww - s, ground.Y - gap, s, t));
                    rlg.Add(new Rectangle(0, ground.Y - gap * 3, m, t));
                    rlg.Add(new Rectangle(ww - m, ground.Y - gap * 3, m, t));
                    rh = r.Next(2);
                    if (rh == 0)
                    {
                        rlg.Add(new Rectangle(ww / 2 - (l) / 2, ground.Y - gap * 2, l, t));
                        rh = r.Next(2);
                        if (rh == 0)
                        {
                            rlg.Add(new Rectangle(ww / 2 - (m) / 2, ground.Y - gap * 4, m, t));
                        }
                        else
                        {
                            rlg.Add(new Rectangle(ww / 2 - (s) / 2, ground.Y - gap * 4, s, t));
                        }
                    }
                    else
                    {
                        rlg.Add(new Rectangle(m, ground.Y - gap * 2, s, t));
                        rlg.Add(new Rectangle(ww / 2 - (s) / 2, ground.Y - gap * 2, s, t));
                        rlg.Add(new Rectangle(ww - m - s, ground.Y - gap * 2, s, t));
                        rlg.Add(new Rectangle(ww / 2 - (m) / 2, ground.Y - gap * 4, m, t));
                    }

                }
                // one hole
                else
                {
                    rlg.Add(new Rectangle(0, ground.Y, m, t));
                    rlg.Add(new Rectangle(m, ground.Y, s, t));
                    rlg.Add(new Rectangle(ww - m, ground.Y, m, t));
                    rlg.Add(new Rectangle(ww - m - s, ground.Y, s, t));
                    rlg.Add(new Rectangle(s, ground.Y - gap * 4, m, t));
                    rlg.Add(new Rectangle(ww - m - s, ground.Y - gap * 4, m, t));
                    rh = r.Next(2);
                    if (rh == 0)
                    {
                        rlg.Add(new Rectangle(0, ground.Y - gap, s, t));
                        rlg.Add(new Rectangle(ww - s, ground.Y - gap, s, t));
                    }
                    else
                    {
                        rlg.Add(new Rectangle(0, ground.Y - gap, m, t));
                        rlg.Add(new Rectangle(ww - m, ground.Y - gap, m, t));
                        rh = r.Next(2);
                        if (rh == 0)
                        {
                            rlg.Add(new Rectangle(0, ground.Y - gap * 3, s, t));
                            rlg.Add(new Rectangle(ww - s, ground.Y - gap * 3, s, t));
                        }
                    }
                    rh = r.Next(2);
                    if (rh == 0)
                    {
                        rlg.Add(new Rectangle(ww / 2 - (s) / 2, ground.Y - gap, s, t));
                    }
                    rh = r.Next(2);
                    if (rh == 0)
                    {
                        rlg.Add(new Rectangle(ww / 2 - (l) / 2, ground.Y - gap * 2, l, t));
                    }
                    else
                    {
                        rlg.Add(new Rectangle(ww / 2 - (m) / 2, ground.Y - gap * 2, m, t));
                    }
                }
            }
        }
        public Rectangle Ground
        {
            get { return ground; }
        }
        public Rectangle Door
        {
            get { return door; }
            set { door = value; }
        }
        public bool E
        {
            get { return e; }
            set { e = value; }
        }
        public int RC
        {
            get { return roomCounter; }
            set { roomCounter = value; }
        }
        public int A
        {
            get { return a; }
        }
    }
}
