using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Loot
    {
        Random r = new Random();
        Color color;
        public Rectangle drop;
        string type;
        int speed;
        int fallSpeed;
        bool fall;
        public Loot(int X, int Y, int ww, string loot)
        {
            if (loot == "hp")
            {
                color = Color.Red;
                drop = new Rectangle(X, Y, ww / 30, ww / 30);
            }
            else if (loot == "ammo")
            {
                color = Color.Black;
                drop = new Rectangle(X, Y, ww / 40, ww / 40);
            }
            else
            {
                color = Color.Blue;
                drop = new Rectangle(X, Y, ww / 30, ww / 30);
            }
            speed = r.Next(-ww / 266, ww / 266 + 1);
            type = loot;
            fallSpeed = r.Next(ww / 100);
            fall = true;
        }
        /// <summary>
        /// Uppdaterar looten ifall den faller
        /// </summary>
        public void Update(int g)
        {
            if (fall == true)
            {
                drop.X += speed;
                drop.Y -= fallSpeed;
                fallSpeed -= g;
            }
        }
        public int S
        {
            get { return speed; }
            set { speed = value; }
        }
        public int FS
        {
            get { return fallSpeed; }
            set
            {
                if (value < -28)
                {
                    fallSpeed = -28;
                }
                else
                {
                    fallSpeed = value;
                }
            }
        }
        public bool F
        {
            get { return fall; }
            set { fall = value; }
        }
        public Color C
        {
            get { return color; }
            set { color = value; }
        }
        public string T
        {
            get { return type; }
            set { type = value; }
        }
    }
}
