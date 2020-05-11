using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Bullet
    {
        public Rectangle bullet;
        int bulletSpeed;
        bool friendly;
        Color color;
        // skapar skåttet och anger des riktning
        public Bullet(int X, int Y, int d, int ww, int wh, string t)
        {
            bullet = new Rectangle(X, Y, ww / 80, wh / 96);
            if (d > 0)
            {
                bulletSpeed = ww / 80;
            }
            else
            {
                bulletSpeed = - ww / 80;
            }
            if (t == "p")
            {
                friendly = true;
                color = Color.Blue;
            }
            else
            {
                friendly = false;
                color = Color.Red;
            }
        }
        /// <summary>
        /// Flyttar skåtten
        /// </summary>
        public void Update()
        {
            bullet.X += bulletSpeed;
        }
        public bool F
        {
            get { return friendly; }
        }
        public Color C
        {
            get { return color; }
        }
    }
}
