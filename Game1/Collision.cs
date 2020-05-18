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
        /// <summary>
        /// Kollar ifall något kolliderar
        /// </summary>>
        public void Check(Player p, ref Room room, List<Rectangle> rlg, ref List<Box> bl, ref List<Chest> cl, ref List<Bullet> pl, ref List<Enemy> el, ref List<Loot> ll, int g, KeyboardState kstate, KeyboardState oldstate, int ww, int wh)
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
                    p.RectangleStuff(p.P.X, r.Y - p.P.Height, p.P.Width, p.P.Height);
                    p.UpdatePosition(ww);
                    p.JS = 0;
                }
                if (p.pTop.Intersects(r))
                {
                    if (p.GB == false)
                    {
                        p.RectangleStuff(p.P.X, r.Y + r.Height, p.P.Width, p.P.Height);
                    }
                    else
                    {
                        p.RectangleStuff(p.P.X, r.Y + r.Height + ww / 26, p.P.Width, p.P.Height);
                    }
                    p.UpdatePosition(ww);
                    p.JS *= -1;
                }
                if (p.pRight.Intersects(r))
                {
                    p.RectangleStuff(r.X + r.Width, p.P.Y, p.P.Width, p.P.Height);
                    p.UpdatePosition(ww);
                }
                if (p.pLeft.Intersects(r))
                {
                    p.RectangleStuff(r.X - p.P.Width, p.P.Y, p.P.Width, p.P.Height);
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
                        bl[i].box.Y = r.Y - bl[i].box.Height;
                        bl[i].FS = 0;
                        bl[i].F = false;
                        bl[i].UpdateAura();
                    }
                    else
                    {
                        bl[i].F = true;
                    }
                }
                // loot
                foreach (var l in ll)
                {
                    if (l.drop.Intersects(r))
                    {
                        l.drop.Y = r.Y - l.drop.Height;
                        l.S = 0;
                        l.FS = 0;
                        l.F = false;
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
            if (p.P.X > ww - p.P.Width)
            {
                p.RectangleStuff(ww - p.P.Width, p.P.Y, p.P.Width, p.P.Height);
            }
            else if (p.P.X < 0)
            {
                p.RectangleStuff(0, p.P.Y, p.P.Width, p.P.Height);
            }
            else if (p.P.Y > wh)
            {
                p.HP -= 1;
                p.sw.Start();
                p.UpdateHealth(ww);
                p.RectangleStuff(0, p.P.Y, p.P.Width, p.P.Height);
                p.RectangleStuff(p.P.X, wh - wh / 24 - p.P.Height, p.P.Width, p.P.Height);
                p.JS = 0;
            }
            // kollar ifall något kolliderar med en låda
            foreach (var b in bl)
            {
                if (b.BP == true)
                {
                    b.box.X = p.P.X - (b.box.Width - p.P.Width) / 2;
                    b.box.Y = p.P.Y - b.box.Height;
                }
                //kollar ifall spelaern står nära lådan för att kunna plocka upp den
                if (p.P.Intersects(b.aura) && b.BP == false && b.BT == false)
                {
                    b.E = true;
                }
                else
                {
                    b.E = false;
                }
                if (p.P.Intersects(b.aura) && kstate.IsKeyDown(Keys.E) && oldstate.IsKeyUp(Keys.E) && b.BP == false && b.BT == false)
                {
                    b.BP = true;
                    p.GB = true;
                    b.BT = false;
                }
                else if (kstate.IsKeyDown(Keys.E) && oldstate.IsKeyUp(Keys.E) && b.BP == true)
                {
                    if (p.D == 1)
                    {
                        b.box.X = p.P.X + p.P.Width;
                    }
                    else
                    {
                        b.box.X = p.P.X - b.box.Width;
                    }
                    b.BP = false;
                    p.GB = false;
                    b.box.Y = p.P.Y + p.P.Height - b.box.Height;
                    b.UpdateAura();
                }
                // spelare
                if (b.BP == false)
                {
                    if (p.pBot.Intersects(b.box))
                    {
                        p.DJ = false;
                        p.J = false;
                        p.RectangleStuff(p.P.X, b.box.Y - p.P.Height, p.P.Width, p.P.Height);
                        p.UpdatePosition(ww);
                        p.JS = 0;
                    }
                    if (p.pRight.Intersects(b.box))
                    {
                        p.RectangleStuff(b.box.X + b.box.Width, p.P.Y, p.P.Width, p.P.Height);
                        p.UpdatePosition(ww);
                    }
                    if (p.pLeft.Intersects(b.box))
                    {
                        p.RectangleStuff(b.box.X - p.P.Width, p.P.Y, p.P.Width, p.P.Height);
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
                    if (e.eRight.Intersects(b.box))
                    {
                        if (b.BT == true)
                        {
                            e.Dead = true;
                        }
                        b.BT = false;
                        e.enemy.X = b.box.X + b.box.Width;
                        e.Speed *= -1;
                        e.UpdatePosition();
                    }
                    if (e.eLeft.Intersects(b.box))
                    {
                        if (b.BT == true)
                        {
                            e.Dead = true;
                        }
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
            // kistor
            foreach (var c in cl)
            {
                if (p.P.Intersects(c.chest) && el.Count == 0 && c.Open == false)
                {
                    c.E = true;
                }
                else
                {
                    c.E = false;
                }
                if (p.P.Intersects(c.chest) && kstate.IsKeyDown(Keys.E) && oldstate.IsKeyUp(Keys.E) && c.Open == false)
                {
                    c.Open = true;
                    ll.Add(new Loot(c.inside.X + c.inside.Width / 3, c.chest.Y, ww, c.Loot));
                }
            }
            // kollar ifall skått eller spelar kolliderar med en fiende
            foreach (var e in el)
            {
                // skått
                for (int i = 0; i < pl.Count; i++)
                {
                    if (pl[i].bullet.Intersects(e.enemy) && pl[i].F == true)
                    {
                        e.Dead = true;
                        pl.RemoveAt(i);
                    }
                    // kollar ifall skått kolliderar med spelaren
                    else if (pl[i].bullet.Intersects(p.P) && pl[i].F == false)
                    {
                        pl.RemoveAt(i);
                        p.HP--;
                        p.sw.Start();
                        p.UpdateHealth(ww);
                    }
                }
                // spelare
                if (e.eTop.Intersects(p.pBot))
                {
                    e.Dead = true;
                    p.JS = p.JP * 3 / 4;
                }
                else if (e.enemy.Intersects(p.P) && p.sw.ElapsedMilliseconds == 0)
                {
                    p.HP--;
                    p.sw.Start();
                    p.UpdateHealth(ww);
                }
                // kollar ifall spelaren blir sedd av en fiende
                if (e.sight.Intersects(p.P) && e.sw.ElapsedMilliseconds == 0)
                {
                    e.sw.Start();
                    pl.Add(new Bullet(e.gun.X, e.gun.Y + 1, e.Speed, ww, wh, "e"));
                }
                // kollar ifall fiender lämnar fönstret
                if (e.enemy.Y > wh)
                {
                    e.Dead = true;
                }
            }
            // kollar ifall spelaren kolliderar med loot
            for (int i = 0; i < ll.Count; i++)
            {
                if (p.P.Intersects(ll[i].drop) && p.HP < p.MHP && ll[i].T == "hp")
                {
                    p.HP++;
                    p.UpdateHealth(ww);
                    ll.RemoveAt(i);
                }
                else if (p.P.Intersects(ll[i].drop) && p.Ammo < p.MAmmo && ll[i].T == "ammo")
                {
                    p.Ammo++;
                    p.UpdateAmmo(ww);
                    ll.RemoveAt(i);
                }
                else if (p.P.Intersects(ll[i].drop) && ll[i].T != "ammo" && ll[i].T != "hp")
                {
                    ll.RemoveAt(i);
                    p.StatUpgrade(ww);
                }
            }
        }
    }
}
