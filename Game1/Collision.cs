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
        public void Check(Player p, List<Rectangle> rlg, ref List<Box> bl, ref List<Bullet> pl, ref List<Enemy> el, int g, KeyboardState kstate, KeyboardState oldstate, int ww, int wh)
        {
            //kollar ifall något kollidrerar med marken eller en platform
            foreach (var r in rlg)
            {
                // spelare
                if (p.pBot.Intersects(r) || p.J == true)
                {
                    p.F = false;
                }
                else
                {
                    p.F = true;
                }
                if (p.pBot.Intersects(r))
                {
                    p.DJ = false;
                    p.J = false;
                    p.player.Y = r.Y - p.player.Height;
                    p.UpdatePosition(ww);
                    p.JS = 0;
                }
                if (p.pTop.Intersects(r))
                {
                    if (p.GB == false)
                    {
                        p.player.Y = r.Y + r.Height;
                    }
                    else
                    {
                        p.player.Y = r.Y + r.Height + ww / 26;
                    }
                    p.UpdatePosition(ww);
                    p.JS *= -1;
                }
                if (p.pRight.Intersects(r))
                {
                    p.player.X = r.X + r.Width;
                    p.UpdatePosition(ww);
                }
                if (p.pLeft.Intersects(r))
                {
                    p.player.X = r.X - p.player.Width;
                    p.UpdatePosition(ww);
                }
                // fiende
                foreach (var e in el)
                {
                    if (e.eBot.Intersects(r))
                    {
                        e.F = false;
                        e.FS = 0;
                        e.enemy.Y = r.Y - e.enemy.Height;
                        e.UpdatePosition();
                    }
                    else
                    {
                        e.F = true;
                    }
                    if (e.eTop.Intersects(r))
                    {
                        e.enemy.Y = r.Y + r.Height;
                        e.UpdatePosition();
                    }
                    if (e.eRight.Intersects(r))
                    {
                        e.enemy.X = r.X + r.Width;
                        e.Speed *= -1;
                        e.UpdatePosition();
                    }
                    if (e.eLeft.Intersects(r))
                    {
                        e.enemy.X = r.X - e.enemy.Width;
                        e.Speed *= -1;
                        e.UpdatePosition();
                    }
                }
                // låda
                for (int i = 0; i < bl.Count; i++)
                {
                    if ((bl[i].box.Intersects(r) || bl[i].box.X < 0 || bl[i].box.X > ww - bl[i].box.Width) && bl[i].BT == true)
                    {
                        bl.RemoveAt(i);
                    }
                    else if (bl[i].box.Intersects(r) && bl[i].BP == false)
                    {
                        bl[i].box.Y = r.Y - bl[i].box.Width;
                        bl[i].FS = 0;
                        bl[i].F = false;
                        bl[i].UpdateAura();
                    }
                    else
                    {
                        bl[i].F = true;
                    }
                }
                // skått
                for (int i = 0; i < pl.Count; i++)
                {
                    if (pl[i].bullet.Intersects(r) || pl[i].bullet.X < 0 || pl[i].bullet.X > ww - pl[i].bullet.Width)
                    {
                        pl.RemoveAt(i);
                    }
                }
            }
            //kollar ifall spelaren försöker lämna fönstret
            if (p.player.X > ww - p.player.Width)
            {
                p.player.X = ww - p.player.Width;
            }
            else if (p.player.X < 0)
            {
                p.player.X = 0;
            }
            else if (p.player.Y > wh)
            {
                p.HP -= 1;
                p.UpdateHealth(ww);
                p.player.X = 0;
                p.player.Y = wh - wh / 24 - p.player.Height;
                p.JS = 0;
            }
            // kollar ifall något kolliderar med en låda
            foreach (var b in bl)
            {
                if (b.BP == true)
                {
                    b.box.X = p.player.X - (b.box.Width - p.player.Width) / 2;
                    b.box.Y = p.player.Y - b.box.Height;
                }
                //kollar ifall spelaern står nära lådan för att kunna plocka upp den
                if (p.player.Intersects(b.aura) && b.BP == false && b.BT == false)
                {
                    b.E = true;
                }
                else
                {
                    b.E = false;
                }
                if (p.player.Intersects(b.aura) && kstate.IsKeyDown(Keys.E) && oldstate.IsKeyUp(Keys.E) && b.BP == false && b.BT == false)
                {
                    b.BP = true;
                    p.GB = true;
                    b.BT = false;
                }
                else if (kstate.IsKeyDown(Keys.E) && oldstate.IsKeyUp(Keys.E) && b.BP == true)
                {
                    if (p.D == "R")
                    {
                        b.box.X = p.player.X + p.player.Width;
                    }
                    else
                    {
                        b.box.X = p.player.X - b.box.Width;
                    }
                    b.BP = false;
                    p.GB = false;
                    b.box.Y = p.player.Y + p.player.Height - b.box.Height;
                    b.UpdateAura();
                }
                // spelare
                if (b.BP == false)
                {
                    if (p.pBot.Intersects(b.box))
                    {
                        p.DJ = false;
                        p.J = false;
                        p.player.Y = b.box.Y - p.player.Height;
                        p.UpdatePosition(ww);
                        p.JS = 0;
                    }
                    if (p.pTop.Intersects(b.box))
                    {
                        p.player.Y = b.box.Y + b.box.Height;
                        p.UpdatePosition(ww);
                        p.JS *= -1;
                    }
                    if (p.pRight.Intersects(b.box))
                    {
                        p.player.X = b.box.X + b.box.Width;
                        p.UpdatePosition(ww);
                    }
                    if (p.pLeft.Intersects(b.box))
                    {
                        p.player.X = b.box.X - p.player.Width;
                        p.UpdatePosition(ww);
                    }
                }
                // fiende
                foreach (var e in el)
                {
                    if (e.eBot.Intersects(b.box))
                    {
                        e.F = false;
                        e.FS = 0;
                        e.enemy.Y = b.box.Y - e.enemy.Height;
                        e.UpdatePosition();
                    }
                    else
                    {
                        e.F = true;
                    }
                    if (e.eTop.Intersects(b.box))
                    {
                        e.enemy.Y = b.box.Y + b.box.Height;
                        e.UpdatePosition();
                    }
                    if (e.eRight.Intersects(b.box))
                    {
                        b.BT = false;
                        e.enemy.X = b.box.X + b.box.Width;
                        e.Speed *= -1;
                        e.UpdatePosition();
                    }
                    if (e.eLeft.Intersects(b.box))
                    {
                        b.BT = false;
                        e.enemy.X = b.box.X - e.enemy.Width;
                        e.Speed *= -1;
                        e.UpdatePosition();
                    }
                }
                // skått
                for (int i = 0; i < pl.Count; i++)
                {
                    if (pl[i].bullet.Intersects(b.box))
                    {
                        pl.RemoveAt(i);
                    }
                }
            }
            // kollar ifall ett skått kolliderar med en fiende
            foreach (var e in el)
            {
                for (int i = 0; i < pl.Count; i++)
                {
                    if (pl[i].bullet.Intersects(e.enemy))
                    {
                        e.Dead = true;
                        pl.RemoveAt(i);
                    }
                }
            }
        }
    }
}
