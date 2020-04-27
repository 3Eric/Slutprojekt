using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Chest
    {
        Random r = new Random();
        public Rectangle chest;
        public Rectangle mark;
        public Rectangle lid;
        public Rectangle inside;
        bool e;
        bool open;
        string loot;
        Color color;
        int rh;
        public Chest(int X, int Y, int ww)
        {
            chest = new Rectangle(X, Y, ww / 20, ww / 26);
            mark = new Rectangle(chest.X + ww / 20 / 2 - ww / 60 / 2, chest.Y + ww / 26 / 2 - ww / 60 / 2, ww / 60, ww / 60);
            lid = new Rectangle(X, Y - ww / 26 / 2, ww / 20, ww / 26);
            inside = new Rectangle(X + ww / 80 / 2, lid.Y + ww / 80 / 2, lid.Width - ww / 80, lid.Height - ww / 80 / 2);
            open = false;
            rh = r.Next(3);
            if (rh == 0)
            {
                loot = "hp";
                color = Color.Red;
            }
            else if (rh == 1)
            {
                loot = "ammo";
                color = Color.Black;
            }
            else
            {
                loot = "";
                color = Color.Blue;
            }
        }
        public Color Color
        {
            get { return color; }
        }
        public bool E
        {
            get { return e; }
            set { e = value; }
        }
        public bool Open
        {
            get { return open; }
            set { open = value; }
        }
        public string Loot
        {
            get { return loot; }
            set { loot = value; }
        }
    }
}
