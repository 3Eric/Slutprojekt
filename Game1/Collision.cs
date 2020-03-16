using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Collision
    {
        //Kollar ifall spelaren kolliderar med ett objekt
        public void Check(Player p, List<Rectangle> rlg, List<Box> bl, ref bool jump, ref bool doubleJump, ref int jumpS, int g, KeyboardState kstate, KeyboardState oldstate, int ww)
        {
            //kollar ifall spelaren kolliderar med marken eller en platform
            foreach (var e in rlg)
            {
                if (p.P.Intersects(e) == false && jump == false)
                {
                    p.Y -= jumpS;
                    p.UpdatePosition();
                    if (jumpS > -6)
                    {
                        jumpS -= g;
                    }
                }
                if (p.B.Intersects(e))
                {
                    doubleJump = false;
                    jump = false;
                    p.Y = e.Y - p.Height;
                    p.UpdatePosition();
                    jumpS = 0;
                }
                if (p.T.Intersects(e))
                {
                    p.Y = e.Y + e.Height;
                    p.UpdatePosition();
                    jumpS *= -1;
                }
                if (p.R.Intersects(e))
                {
                    p.X = e.X + e.Width;
                    p.UpdatePosition();
                }
                if (p.L.Intersects(e))
                {
                    p.X = e.X - p.Width;
                    p.UpdatePosition();
                }
            }
            if (p.X > ww - p.Width)
            {
                p.X = ww - p.Width;
            }
            if (p.X < 0)
            {
                p.X = 0;
            }
            //kollar ifall lådan kolliderar med något
            foreach (var b in bl)
            {
                if (p.P.Intersects(b.B) && b.BP == false)
                {
                    b.E = true;
                }
                else
                {
                    b.E = false;
                }
                if (p.P.Intersects(b.B) && kstate.IsKeyDown(Keys.E) && b.BP == false)
                {
                    b.BP = true;
                }
                if (b.B.X > ww - b.Width && b.BT == true)
                {
                    b.X = ww - b.Width;
                    b.BT = false;
                }
                else if (b.B.X < 0 && b.BT == true)
                {
                    b.X = 0;
                    b.BT = false;
                }
                foreach (var e in rlg)
                {
                    if (b.B.Intersects(e) && b.BT == true)
                    {
                        b.BT = false;
                    }
                }
            }

        }
    }
}
