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
        // skapar skåttet och anger des riktning
        public Bullet(int X, int Y, string d, int ww, int wh)
        {
            bullet = new Rectangle(X, Y, ww / 80, wh / 96);
            if (d == "R")
            {
                bulletSpeed = ww / 80;
            }
            else
            {
                bulletSpeed = - ww / 80;
            }
        }
        // skåtter flyttas
        public void Update()
        {
            bullet.X += bulletSpeed;
        }
    }
}
