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
                p.player.X += p.Speed;
                p.UpdatePosition(ww);
                p.D = "R";
            }
            if (kstate.IsKeyDown(Keys.A))
            {
                p.player.X -= p.Speed;
                p.UpdatePosition(ww);
                p.D = "L";
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
                            if (p.D == "R")
                            {
                                b.BS = 10;
                                b.box.X = p.player.X + p.player.Width;
                            }
                            else
                            {
                                b.BS = -10;
                                b.box.X = p.player.X - b.box.Width;
                            }
                            b.BP = false;
                            b.BT = true;
                            b.box.Y = p.player.Y;
                        }
                    }
                }
                else
                {
                    pl.Add(new Bullet(p.gun.X, p.gun.Y + 1, p.D, ww, wh));
                }
            }
            if (kstate.IsKeyDown(Keys.N) && oldstate.IsKeyUp(Keys.N))
            {
                bl.Add(new Box(50, wh - wh / 5 - 30, ww));
            }
            else if (kstate.IsKeyDown(Keys.M) && oldstate.IsKeyUp(Keys.M))
            {
                el.Add(new Enemy(120, 0, ww, wh));
            }
            else if (kstate.IsKeyDown(Keys.C) && oldstate.IsKeyUp(Keys.C))
            {
                el.Clear();
                bl.Clear();
            }
            else if (kstate.IsKeyDown(Keys.R) && oldstate.IsKeyUp(Keys.R))
            {
                room.Generate(ref rlg, ref bl, ref el, ww, wh);
            }

            // spelaren faller
            if (p.J == true || p.F == true)
            {
                p.player.Y -= p.JS;
                p.UpdatePosition(ww);
                p.JS -= g;
            }
            // flyttar alla fiender
            foreach (var e in el)
            {
                e.Move(ww);
                if (e.F == true)
                {
                    e.enemy.Y -= e.FS;
                    e.FS -= g;
                }
                e.UpdatePosition();
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
            // kollar ifall något kollidrerar
            c.Check(p, rlg, ref bl, ref pl, ref el, g, kstate, oldstate, ww, wh);
            oldstate = Keyboard.GetState();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            // ritar spelaren
            spriteBatch.Draw(pixel, p.gun, Color.Black);
            spriteBatch.Draw(pixel, p.player, Color.Purple);
            // ritar fienden
            foreach (var e in el)
            {
                spriteBatch.Draw(pixel, e.enemy, Color.Red);
            }
            // ritar marken och plattformar
            foreach (var e in rlg)
            {
                spriteBatch.Draw(pixel, e, Color.Black);
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
                spriteBatch.Draw(pixel, p.bullet, Color.Purple);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
