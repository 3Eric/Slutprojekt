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
        public Rectangle player;
        public Rectangle pRight;
        public Rectangle pLeft;
        public Rectangle pTop;
        public Rectangle pBot;
        public Rectangle gun;
        string pDirection;
        int sp;
        int pSpeed;
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
            player = new Rectangle(0, wh - wh / 10 - wh / 5, ww / 40, wh / 10);
            pRight = new Rectangle(player.X, player.Y, player.Width / 2, player.Height);
            pLeft = new Rectangle(player.X + pRight.Width, player.Y, pRight.Width, player.Height);
            pTop = new Rectangle(player.X, player.Y - 1, player.Width, 1);
            pBot = new Rectangle(player.X, player.Y + player.Height - wh / 60 + 1, player.Width, wh / 60);
            gun = new Rectangle(player.X + player.Width, player.Y + player.Width, ww / 80, wh / 60);
            pDirection = "R";
            sp = 100;
            pSpeed = ww / 160;
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
            gun.Y = player.Y + player.Width;
            if (pDirection == "R")
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
            rh = r.Next(3);
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
                pSpeed = pSpeed * sp / 100;
            }
        }
        //kunna hämta olika värden från spelaren
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
        public string D
        {
            get { return pDirection; }
            set { pDirection = value; }
        }
        public bool GB
        {
            get { return gotBox; }
            set { gotBox = value; }
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
    }
}
