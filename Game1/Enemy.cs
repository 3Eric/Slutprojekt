using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Enemy
    {
        public Rectangle enemy;
        public Rectangle eRight;
        public Rectangle eLeft;
        public Rectangle eTop;
        public Rectangle eBot;
        int eSpeed;
        int fallSpeed;
        bool fall;
        public Enemy(int X, int Y, int ww, int wh)
        {
            enemy = new Rectangle(X, Y, ww / 40, wh / 10);
            eRight = new Rectangle(enemy.X, enemy.Y, enemy.Width / 2, enemy.Height);
            eLeft = new Rectangle(enemy.X + eRight.Width, enemy.Y, eRight.Width, enemy.Height);
            eTop = new Rectangle(enemy.X, enemy.Y - 1, enemy.Width, 1);
            eBot = new Rectangle(enemy.X, enemy.Y + enemy.Height + 1, enemy.Width, 1);
            eSpeed = - ww / 266;
            fall = true;
        }
        //flyttar fienden
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
        // uppdaterar findens position
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
            set { fallSpeed = value; }
        }
        public bool F
        {
            get { return fall; }
            set { fall = value; }
        }
    }
}
