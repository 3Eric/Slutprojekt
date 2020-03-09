using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Game1
{
    class Player
    {
        Rectangle player;
        Rectangle playerR;
        Rectangle playerL;
        Rectangle playerT;
        Rectangle playerB;
        public Player(int wh)
        {
            player = new Rectangle(0, wh - 50 - wh / 5, 20, 50);
            playerR = new Rectangle(player.X, player.Y, player.Width / 2, player.Height);
            playerL = new Rectangle(player.X + playerR.Width, player.Y, playerR.Width, player.Height);
            playerT = new Rectangle(player.X, player.Y - 1, player.Width, 1);
            playerB = new Rectangle(player.X, player.Y + player.Height, player.Width, 1);
        }

        public void UpdatePosition()
        {
                playerR.X = player.X;
                playerL.X = player.X + playerR.Width;
                playerT.X = player.X;
                playerB.X = player.X;
                playerR.Y = player.Y;
                playerL.Y = player.Y;
                playerT.Y = player.Y - 1;
                playerB.Y = player.Y + player.Height;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D p)
        {
            spriteBatch.Draw(p, player, Color.Purple);
        }
        public int Y
        {
            get { return player.Y; }
            set { player.Y = value; }
        }
        public int X
        {
            get { return player.X; }
            set { player.X = value; }
        }
        public int RY
        {
            get { return playerR.Y; }
        }
        public int RX
        {
            get { return playerR.X; }
        }
        public int LY
        {
            get { return playerL.Y; }
        }
        public int LX
        {
            get { return playerL.X; }
        }
        public int TY
        {
            get { return playerT.Y; }
        }
        public int TX
        {
            get { return playerT.X; }
        }
        public int BY
        {
            get { return playerB.Y; }
        }
        public int BX
        {
            get { return playerB.X; }
        }
        public int Height
        {
            get { return player.Height; }
        }
        public int Width
        {
            get { return player.Width; }
        }
        public Rectangle P
        {
            get { return player; }
        }
        public Rectangle R
        {
            get { return playerR; }
        }
        public Rectangle L
        {
            get { return playerL; }
        }
        public Rectangle T
        {
            get { return playerT; }
        }
        public Rectangle B
        {
            get { return playerB; }
        }
    }
}
