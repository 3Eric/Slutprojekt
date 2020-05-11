using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Box
    {
        public Rectangle box;
        public Rectangle aura;
        int boxSpeed;
        bool pickedUp;
        bool thrown;
        bool e;
        int fallSpeed;
        bool fall;
        public Box(int X, int Y, int ww)
        {
            box = new Rectangle(X, Y, ww / 26, ww / 26);
            aura = new Rectangle(box.X - (ww / 20 - ww / 26) / 2, box.Y - (ww / 20 - ww / 26) / 2, ww / 20, ww / 20);
            pickedUp = false;
            thrown = false;
            e = false;
            fallSpeed = 0;
            fall = false;
        }
        /// <summary>
        /// uppdaterar boxens position
        /// </summary>
        public void Update(Player p, int g)
        {
            //kollar ifall lådan är upplockad av spelaren
            if (pickedUp == true)
            {
                box.X = p.P.X - (box.Width - p.P.Width) / 2;
                box.Y = p.P.Y - box.Height;
            }
            else if (pickedUp == false && thrown == true)
            {
                box.X += boxSpeed;
            }
            else if (fall == true)
            {
                box.Y -= fallSpeed;
                fallSpeed -= g;
            }
        }
        // placerar "auran" vid lådans position
        public void UpdateAura()
        {
            aura.X = box.X - (aura.Width - box.Width) / 2;
            aura.Y = box.Y - (aura.Width - box.Width) / 2;
        }
        // kunna hämta olika värden vrådn lådor
        public bool BP
        {
            get { return pickedUp; }
            set { pickedUp = value; }
        }
        public bool BT
        {
            get { return thrown; }
            set { thrown = value; }
        }
        public bool E
        {
            get { return e; }
            set { e = value; }
        }
        public int BS
        {
            get { return boxSpeed; }
            set { boxSpeed = value; }
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
