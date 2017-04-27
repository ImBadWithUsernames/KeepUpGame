using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace KeepUp
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Sprite Ball;
        Texture2D B1;
        int score = 0;
        enum GameStates { TitleScreen, Playing, GameOver };
        GameStates gameState = GameStates.TitleScreen;

        public Game1()
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
            this.IsMouseVisible = true;
            // TODO: Add your initialization logic here

            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            B1 = Content.Load<Texture2D>("Ball");
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Ball = new Sprite(new Vector2(300, 20),
                            B1,
                            new Rectangle(350, 150, 140, 140),
                            new Vector2(0, 700));


            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            Window.Title = "Score : " + score;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            if (gameState == GameStates.TitleScreen)
            {
                KeyboardState kb = Keyboard.GetState();

                if (kb.IsKeyDown(Keys.Space))
                {
                    gameState = GameStates.Playing;
                }
            }
            else if (gameState == GameStates.Playing)
            {
                MouseState ms = Mouse.GetState();
                Vector2 clk = new Vector2(ms.X, ms.Y);

                if (ms.LeftButton == ButtonState.Pressed && Vector2.Distance(clk, Ball.Center) < Ball.BoundingBoxRect.Width/2)
                {
                    Ball.Velocity = new Vector2(0, -1000);
                    score += 1;
                }
                if (Ball.Location.Y > this.Window.ClientBounds.Height-Ball.BoundingBoxRect.Height)
                {
                    Ball.Velocity = new Vector2(0, -1600);
                    score = 0;
                    //gameState = GameStates.GameOver;
                }
                /*
                if (Ball.Location.Y > this.Window.ClientBounds.Height)
                {
                    Ball.Velocity = new Vector2(0, -600);
                    //gameState = GameStates.GameOver;
                }*/

                Ball.Velocity += new Vector2(0, 50);

                Ball.Update(gameTime);
            }
            else if (gameState == GameStates.GameOver)
            {

            }
            


            base.Update(gameTime);
        }
        
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            if (gameState == GameStates.TitleScreen)
            {
                GraphicsDevice.Clear(Color.SkyBlue);
                spriteBatch.Begin();

                //spriteBatch.Draw(background, this.Window.ClientBounds, Color.White);

                spriteBatch.End();
            }
            else if (gameState == GameStates.Playing)
            {
                GraphicsDevice.Clear(Color.Turquoise);
                spriteBatch.Begin();

                //spriteBatch.Draw(background, this.Window.ClientBounds, Color.White);
                Ball.Draw(spriteBatch);

                spriteBatch.End();
            }
            else if (gameState == GameStates.GameOver)
            {
                GraphicsDevice.Clear(Color.SeaGreen);
                spriteBatch.Begin();

                //spriteBatch.Draw(background, this.Window.ClientBounds, Color.White);

                spriteBatch.End();

            }


            base.Draw(gameTime);
        }
    }
}
