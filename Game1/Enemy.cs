using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Enemy
    {
        Random r = new Random();
        public Stopwatch sw = new Stopwatch();
        public Rectangle enemy;
        public Rectangle eRight;
        public Rectangle eLeft;
        public Rectangle eTop;
        public Rectangle eBot;
        public Rectangle gun;
        public Rectangle sight;
        int eSpeed;
        int fallSpeed;
        bool fall;
        bool dead;
        bool gotLoot;
        string loot;
        int lnr;
        public Enemy(int X, int Y, int ww, int wh)
        {
            enemy = new Rectangle(X, Y, ww / 25, wh / 10);
            eRight = new Rectangle(enemy.X, enemy.Y, enemy.Width / 2, enemy.Height);
            eLeft = new Rectangle(enemy.X + eRight.Width, enemy.Y, eRight.Width, enemy.Height);
            eTop = new Rectangle(enemy.X, enemy.Y - 1, enemy.Width, enemy.Height / 3);
            eBot = new Rectangle(enemy.X, enemy.Y + enemy.Height + 1, enemy.Width, 1);
            gun = new Rectangle(enemy.X + enemy.Width, enemy.Y + enemy.Height / 3, ww / 80, wh / 60);
            sight = new Rectangle(enemy.X + enemy.Width, enemy.Y + enemy.Height / 3, ww / 3, wh / 120);
            eSpeed = - ww / 266;
            fall = true;
            dead = false;
            lnr = r.Next(10);
            if (lnr == 0)
            {
                gotLoot = true;
                lnr = r.Next(2);
                if (lnr == 0)
                {
                    loot = "ammo";
                }
                else
                {
                    loot = "hp";
                }
            }
            else
            {
                gotLoot = false;
            }
        }
        /// <summary>
        /// flyttar fienden
        /// </summary>
        public void Move(int ww)
        {
            enemy.X += eSpeed;
            if (enemy.X < 0)
            {
                eSpeed *= -1;
                enemy.X = 0;
            }
            else if (enemy.X > ww)
            {
                eSpeed *= -1;
                enemy.X = ww - enemy.Width;
            }
        }
        /// <summary>
        /// uppdaterar findens position
        /// </summary>
        public void UpdatePosition()
        {
            eTop.X = enemy.X;
            eTop.Y = enemy.Y - 1;
            eRight.X = enemy.X;
            eRight.Y = enemy.Y;
            eLeft.X = enemy.X + eRight.Width;
            eLeft.Y = enemy.Y;
            eBot.X = enemy.X;
            eBot.Y = enemy.Y + enemy.Height + 1;
            gun.Y = enemy.Y + enemy.Height / 3;
            sight.Y = gun.Y;
            if (eSpeed < 0)
            {
                gun.X = enemy.X - gun.Width;
                sight.X = enemy.X - sight.Width;
            }
            else
            {
                gun.X = enemy.X + enemy.Width;
                sight.X = enemy.X + enemy.Width;
            }
        }
        // kunna hämta olika färden från fienden
        public int Speed
        {
            get { return eSpeed; }
            set { eSpeed = value; }
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
        public bool Dead
        {
            get { return dead; }
            set { dead = value; }
        }
        public bool GL
        {
            get { return gotLoot; }
        }
        public string Loot
        {
            get { return loot; }
        }
    }
}
