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
        Rectangle box;
        int boxSpeed;
        bool pickedUp;
        bool thrown;
        bool e;
        public Box(int X, int Y)
        {
            box = new Rectangle(X, Y, 30, 30);
            pickedUp = false;
            thrown = false;
            e = false;
        }
        //uppdaterar spelarens position
        public void Update(Player p)
        {
            //kollar ifall lådan är upplockad av spelaren
            if (pickedUp == true)
            {
                box.X = p.X - 5;
                box.Y = p.Y - box.Height;
            }
            else if (pickedUp == false && thrown == true)
            {
                box.X += boxSpeed;
            }
        }
        //ritar lådorna
        public void Draw(SpriteBatch spriteBatch, Texture2D p)
        {
            spriteBatch.Draw(p, box, Color.SaddleBrown);
        }
        // kunna hämta olika värden vrådn lådor
        public Rectangle B
        {
            get { return box; }
        }
        public int X
        {
            get { return box.X; }
            set { box.X = value; }
        }
        public int Y
        {
            get { return box.Y; }
            set { box.Y = value; }
        }
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
        public int Height
        {
            get { return box.Height; }
        }
        public int Width
        {
            get { return box.Width; }
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
    }
}
