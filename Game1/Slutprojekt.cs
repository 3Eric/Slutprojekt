using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Slutprojekt : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D pixel;
        SpriteFont font;
        List<Rectangle> rlg = new List<Rectangle>();
        Player p;
        Rectangle ground;
        bool jump = false;
        bool doubleJump = false;
        int jumpS;
        int jumpP = 15;
        int g = 1;
        int ww;
        int wh;

        KeyboardState oldstate;

        public Slutprojekt()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            ww = Window.ClientBounds.Width;
            wh = Window.ClientBounds.Height;
            rlg.Add(ground = new Rectangle(0, wh - wh / 5, ww, wh));
            rlg.Add(new Rectangle(0, 200, 100, 20));
            rlg.Add(new Rectangle(400, 340, 100, 20));
            p = new Player(wh);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            pixel = Content.Load<Texture2D>("pixel");
            font = Content.Load<SpriteFont>("font");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            KeyboardState kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.D))
            {
                p.X += 5;
                p.UpdatePosition();
            }
            if (kstate.IsKeyDown(Keys.A))
            {
                p.X -= 5;
                p.UpdatePosition();
            }
            if (kstate.IsKeyDown(Keys.Space) && jump == false)
            {
                jump = true;
                jumpS = jumpP;
            }
            else if (kstate.IsKeyDown(Keys.Space) && jump == true && doubleJump == false && oldstate.IsKeyUp(Keys.Space))
            {
                doubleJump = true;
                jumpS = jumpP;
            }
            
            if (jump == true)
            {
                p.Y -= jumpS;
                p.UpdatePosition();
                jumpS -= g;
            }
            foreach (var e in rlg)
            {
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
                if (p.P.Intersects(e) == false && jump == false)
                {
                    p.Y -= jumpS;
                    p.UpdatePosition();
                    if (jumpS > -6)
                    {
                        jumpS -= g;
                    }
                }
            }
            oldstate = Keyboard.GetState();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            foreach(var e in rlg)
            {
                spriteBatch.Draw(pixel, e, Color.Black);
            }
            p.Draw(spriteBatch, pixel);



            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
