using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Game1
{
    class Player
    {
        Random r = new Random();
        int rh;
        public Stopwatch sw = new Stopwatch();
        Rectangle player;
        public Rectangle pRight;
        public Rectangle pLeft;
        public Rectangle pTop;
        public Rectangle pBot;
        public Rectangle gun;
        int height;
        int direction;
        int sp;
        int baseSpeed;
        int pSpeed;
        bool sneak;
        bool gotBox;
        bool fall;
        bool jump;
        bool doubleJump;
        int jumpS;
        int jumpP;
        int mhp;
        int hp;
        int mammo;
        int ammo;
        int WW;
        public List<Rectangle> hpl = new List<Rectangle>();
        public List<Rectangle> al = new List<Rectangle>();
        //skapar spelaren
        public Player(int ww, int wh)
        {
            WW = ww;
            height = wh / 10;
            player = new Rectangle(0, wh - wh / 10 - wh / 5, ww / 40, height);
            pRight = new Rectangle(player.X, player.Y, player.Width / 2, height);
            pLeft = new Rectangle(player.X + pRight.Width, player.Y, pRight.Width, height);
            pTop = new Rectangle(player.X, player.Y - 1, player.Width, 1);
            pBot = new Rectangle(player.X, player.Y + height - wh / 60 + 1, player.Width, wh / 60);
            gun = new Rectangle(player.X + player.Width, player.Y + player.Width, ww / 80, wh / 60);
            direction = 1;
            sp = 100;
            baseSpeed = ww / 160;
            pSpeed = baseSpeed;
            jumpS = 0;
            jumpP = ww / 53;
            jump = false;
            doubleJump = false;
            gotBox = false;
            mhp = 3;
            hp = mhp;
            mammo = 5;
            ammo = mammo;
            UpdateHealth(ww);
            UpdateAmmo(ww);
        }
        /// <summary>
        /// Uppdaterar spelarens position
        /// </summary>
        public void UpdatePosition(int ww)
        {
            if (sneak == true)
            {
                player.Height = height / 2;
                pRight.Height = player.Height;
                pLeft.Height = player.Height;
            }
            else
            {
                player.Height = height;
                pRight.Height = player.Height;
                pLeft.Height = player.Height;
            }
            pRight.X = player.X;
            pLeft.X = player.X + pRight.Width;
            pTop.X = player.X;
            pBot.X = player.X;
            pRight.Y = player.Y;
            pLeft.Y = player.Y;
            if (gotBox == true)
            {
                pTop.Y = player.Y - ww / 26;
            }
            else
            {
                pTop.Y = player.Y - 1;
            }
            pBot.Y = player.Y + player.Height;
            gun.Y = player.Y + player.Height / 3;
            if (direction > 0)
            {
                gun.X = player.X + player.Width;
            }
            else
            {
                gun.X = player.X - gun.Width;
            }
        }
        /// <summary>
        /// uppdaterar spelarens liv
        /// </summary>
        public void UpdateHealth(int ww)
        {
            hpl.Clear();
            for (int i = 0; i < hp; i++)
            {
                hpl.Add(new Rectangle(ww / 80 * i + ww / 30 * i, 0, ww / 30, ww / 30));
            }
        }
        /// <summary>
        /// uppdaterar så att man ser hur många skått spelarn har
        /// </summary>
        public void UpdateAmmo(int ww)
        {
            al.Clear();
            for (int i = 0; i < ammo; i++)
            {
                al.Add(new Rectangle(ww / 80 * i + ww / 40 * i, ww / 30 + ww / 80, ww / 40, ww / 40));
            }
        }
        public void StatUpgrade(int ww)
        {
            if (pSpeed >= ww / 42)
            {
                rh = r.Next(2);
            }
            else
            {
                rh = r.Next(3);
            }
            if (rh == 0)
            {
                mhp++;
                hp++;
                UpdateHealth(ww);
            }
            else if (rh == 1)
            {
                mammo++;
                ammo++;
                UpdateAmmo(ww);
            }
            else if (rh == 2)
            {
                sp += 10;
                pSpeed = baseSpeed * sp / 100;
                if (pSpeed >= ww / 42)
                {
                    pSpeed = ww / 42;
                }
            }
        }
        /// <summary>
        /// Ändrar x, y, bredd och höjd värden på rektangeln player
        /// </summary>
        public void RectangleStuff (int x, int y, int w, int h)
        {
            player.X = x;
            player.Y = y;
            player.Width = w;
            player.Height = h;
            if (player.Height > height)
            {
                player.Height = height;
            }
            else if (player.Height < height / 2)
            {
                player.Height = height / 2;
            }
        }
        //kunna hämta olika värden från spelaren
        public Rectangle P
        {
            get { return player; }
            set { player = value; }
        }
        public int Speed
        {
            get { return pSpeed; }
            set 
            {
                if (value > WW / 42)
                {
                    pSpeed = WW / 42;
                }
                else
                {
                    pSpeed = value;
                }
            }
        }
        public int D
        {
            get { return direction; }
            set { direction = value; }
        }
        public bool GB
        {
            get { return gotBox; }
            set { gotBox = value; }
        }
        public bool S
        {
            get { return sneak; }
            set { sneak = value; }
        }
        public int JS
        {
            get { return jumpS; }
            set
            {
                if (value < - WW / 28)
                {
                    jumpS = -WW / 28;
                }
                else
                {
                    jumpS = value;
                }
            }
        }
        public int JP
        {
            get { return jumpP; }
            set { jumpP = value; }
        }
        public bool J
        {
            get { return jump; }
            set { jump = value; }
        }
        public bool DJ
        {
            get { return doubleJump; }
            set { doubleJump = value; }
        }
        public bool F
        {
            get { return fall; }
            set { fall = value; }
        }
        public int MHP
        {
            get { return mhp; }
            set { mhp = value; }
        }
        public int HP
        {
            get { return hp; }
            set
            {
                hp = value;
                if (hp < 0)
                {
                    hp = 0;
                }
            }
        }
        public int MAmmo
        {
            get { return mammo; }
            set { mammo = value; }
        }
        public int Ammo
        {
            get { return ammo; }
            set { ammo = value; }
        }
        public int BS
        {
            get { return baseSpeed; }
        }
        public int SP
        {
            get { return sp; }
            set { sp = value; }
        }
    }
}
