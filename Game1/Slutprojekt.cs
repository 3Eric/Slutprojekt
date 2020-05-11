using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Game1
{
    public class Slutprojekt : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D pixel;
        SpriteFont font;
        List<Rectangle> rlg = new List<Rectangle>();
        List<Box> bl = new List<Box>();
        List<Bullet> pl = new List<Bullet>();
        List<Enemy> el = new List<Enemy>();
        List<Chest> cl = new List<Chest>();
        List<Loot> ll = new List<Loot>();
        Collision c = new Collision();
        Room room;
        Player p;
        int g = 1;
        int ww;
        int wh;

        KeyboardState oldstate;

        public Slutprojekt()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            ww = Window.ClientBounds.Width;
            wh = Window.ClientBounds.Height;
            room = new Room(ref rlg, ww, wh);
            p = new Player(ww, wh);
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            pixel = Content.Load<Texture2D>("pixel");
            font = Content.Load<SpriteFont>("font");
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            KeyboardState kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.D))
            {
                p.RectangleStuff(p.P.X + p.Speed, p.P.Y, p.P.Width, p.P.Height);
                p.UpdatePosition(ww);
                p.D = 1;
            }
            if (kstate.IsKeyDown(Keys.A))
            {
                p.RectangleStuff(p.P.X - p.Speed, p.P.Y, p.P.Width, p.P.Height);
                p.UpdatePosition(ww);
                p.D = -1;
            }
            if (kstate.IsKeyDown(Keys.S) && oldstate.IsKeyUp(Keys.S))
            {
                if (p.S == false)
                {
                    p.S = true;
                    p.Speed /= 2;
                }
                else if (p.S == true)
                {
                    p.RectangleStuff(p.P.X, p.P.Y - p.P.Height, p.P.Width, p.P.Height);
                    p.S = false;
                    p.Speed *= 2;
                }
                p.UpdatePosition(ww);
            }
            if (kstate.IsKeyDown(Keys.Space) && p.J == false)
            {
                p.J = true;
                p.JS = p.JP;
            }
            else if (kstate.IsKeyDown(Keys.Space) && p.J == true && p.DJ == false && oldstate.IsKeyUp(Keys.Space))
            {
                p.DJ = true;
                p.JS = p.JP;
            }
            if (kstate.IsKeyDown(Keys.Enter) && oldstate.IsKeyUp(Keys.Enter))
            {
                if (p.GB == true)
                {
                    p.GB = false;
                    foreach (var b in bl)
                    {
                        if (b.BP == true)
                        {
                            if (p.D > 0)
                            {
                                b.BS = 10;
                                b.box.X = p.P.X + p.P.Width;
                            }
                            else
                            {
                                b.BS = -10;
                                b.box.X = p.P.X - b.box.Width;
                            }
                            b.BP = false;
                            b.BT = true;
                            b.box.Y = p.P.Y;
                        }
                    }
                }
                else if (p.Ammo > 0)
                {
                    p.Ammo--;
                    p.UpdateAmmo(ww);
                    pl.Add(new Bullet(p.gun.X, p.gun.Y + 1, p.D, ww, wh, "p"));
                }
            }
            if (kstate.IsKeyDown(Keys.B) && oldstate.IsKeyUp(Keys.B))
            {
                bl.Add(new Box(50, wh - wh / 5 - 30, ww));
            }
            else if (kstate.IsKeyDown(Keys.M) && oldstate.IsKeyUp(Keys.M))
            {
                el.Add(new Enemy(120, 0, ww, wh));
            }
            else if (kstate.IsKeyDown(Keys.N) && oldstate.IsKeyUp(Keys.N))
            {
                cl.Add(new Chest(100, room.Ground.Y - ww / 26, ww));
            }
            else if (kstate.IsKeyDown(Keys.C) && oldstate.IsKeyUp(Keys.C))
            {
                p.HP = p.MHP;
                p.UpdateHealth(ww);
                p.Ammo = p.MAmmo;
                p.UpdateAmmo(ww);
                el.Clear();
                bl.Clear();
                cl.Clear();
                ll.Clear();
            }
            else if (kstate.IsKeyDown(Keys.R) && oldstate.IsKeyUp(Keys.R))
            {
                room.Generate(ref p, ref rlg, ref bl, ref cl, ref ll, ref el, ww, wh);
            }
            else if (kstate.IsKeyDown(Keys.L) && oldstate.IsKeyUp(Keys.L))
            {
                ll.Add(new Loot(0, 0, ww, "hp"));
            }


            // spelaren faller
            if (p.J == true || p.F == true)
            {
                p.RectangleStuff(p.P.X, p.P.Y - p.JS, p.P.Width, p.P.Height);
                p.UpdatePosition(ww);
                p.JS -= g;
            }
            // flyttar alla lådor ifall dom kastas eller släpps
            foreach (var b in bl)
            {
                b.Update(p, g);
            }
            // gör så att skåtten rör sig
            foreach (var p in pl)
            {
                p.Update();
            }
            if (room.Next(p, el, kstate) == true)
            {
                room.Generate(ref p, ref rlg, ref bl, ref cl, ref ll, ref el, ww, wh);
            }
            // kollar ifall något kollidrerar
            c.Check(p, ref room, rlg, ref bl, ref cl, ref pl, ref el, ref ll, g, kstate, oldstate, ww, wh);
            // Uppdaterar fienderna
            for (int i = 0; i < el.Count; i++)
            {
                // kollar ifall en fiende har dött
                if (el[i].Dead == true)
                {
                    if (el[i].GL == true)
                    {
                        ll.Add(new Loot(el[i].enemy.X, el[i].enemy.Y, ww, el[i].Loot));
                    }
                    el.RemoveAt(i);
                }
                else
                {
                    el[i].Move(ww);
                    if (el[i].F == true)
                    {
                        el[i].enemy.Y -= el[i].FS;
                        el[i].FS -= g;
                    }
                    el[i].UpdatePosition();
                    if (el[i].sw.ElapsedMilliseconds > 1000)
                    {
                        el[i].sw.Reset();
                    }
                }
            }
            foreach (var l in ll)
            {
                l.Update(g);
            }
            // Gör så att spelaren kan ta skada igen
            if (p.sw.ElapsedMilliseconds > 500)
            {
                p.sw.Reset();
            }
            oldstate = Keyboard.GetState();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            if (el.Count == 0)
            {
                spriteBatch.Draw(pixel, room.Door, Color.Yellow);
                if (room.E == true)
                {
                    spriteBatch.DrawString(font, "E", new Vector2(room.Door.X + room.Door.Width / 2 - font.LineSpacing / 2, room.Door.Y - font.LineSpacing), Color.White);
                }
            }
            // ritar alla kistor
            foreach (var c in cl)
            {
                spriteBatch.Draw(pixel, c.chest, Color.SaddleBrown);
                spriteBatch.Draw(pixel, c.mark, c.Color);
                if (c.E == true)
                {
                    spriteBatch.DrawString(font, "E", new Vector2(c.chest.X + c.chest.Width / 2 - font.LineSpacing / 2, c.chest.Y - font.LineSpacing), Color.White);
                }
                if (c.Open == true)
                {
                    spriteBatch.Draw(pixel, c.lid, Color.SaddleBrown);
                    spriteBatch.Draw(pixel, c.inside, Color.Black);
                }
            }
            // ritar spelaren
            spriteBatch.Draw(pixel, p.gun, Color.Black);
            spriteBatch.Draw(pixel, p.P, Color.Blue);
            // ritar fienden
            foreach (var e in el)
            {
                spriteBatch.Draw(pixel, e.gun, Color.Black);
                spriteBatch.Draw(pixel, e.enemy, Color.Red);
            }
            // ritar marken och plattformar
            foreach (var e in rlg)
            {
                spriteBatch.Draw(pixel, e, Color.DarkSlateGray);
            }
            // ritar all lådor
            foreach(var b in bl)
            {
                spriteBatch.Draw(pixel, b.box, Color.SaddleBrown);
                if (b.E == true)
                {
                    spriteBatch.DrawString(font, "E", new Vector2(b.box.X + b.box.Width / 2 - font.LineSpacing / 2, b.box.Y - font.LineSpacing), Color.White);
                }
            }
            // ritar alla skått
            foreach (var p in pl)
            {
                spriteBatch.Draw(pixel, p.bullet, p.C);
            }
            // ritar spelarens liv
            foreach (var hp in p.hpl)
            {
                spriteBatch.Draw(pixel, hp, Color.Red);
            }
            // ritar spelarens ammo
            foreach (var a in p.al)
            {
                spriteBatch.Draw(pixel, a, Color.Black);
            }
            // ritar loot
            foreach (var l in ll)
            {
                spriteBatch.Draw(pixel, l.drop, l.C);
            }
            spriteBatch.DrawString(font, "R:" + room.RC, new Vector2(ww - font.LineSpacing * room.A, 0), Color.White);
            //while (room.RC == 0)
            //{
                //spriteBatch.DrawString(font, "Move: A and D", new Vector2(300, 300), Color.White);
                //spriteBatch.DrawString(font, "Jump/Double jump: Spacebar", new Vector2(350, 300), Color.White);
                //spriteBatch.DrawString(font, "Sneak: S", new Vector2(400, 300), Color.White);
                //spriteBatch.DrawString(font, "Interact: E", new Vector2(450, 300), Color.White);
                //spriteBatch.DrawString(font, "Shoot/Throw: Enter", new Vector2(500, 300), Color.White);
            //}

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
