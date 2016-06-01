using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MenuSystem;
using MenuSystem.Screens;
using MenuSystem.Menu.Game; 

namespace Politico2
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        ScreenManager manager;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width; //800;//1920;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;// 400; //1080;
            //graphics.IsFullScreen = true;
            this.IsMouseVisible = true;
            graphics.PreferMultiSampling = true; 
            graphics.ApplyChanges();

            manager = new ScreenManager(this, 1920, 1080);
            manager.AddScreen(new SplashScreen(new MainMenu()), PlayerIndex.One);
            Components.Add(manager);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit(); 

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(100, 149, 237));

            spriteBatch.Begin();

            spriteBatch.End(); 

            base.Draw(gameTime);
        }
    }
}
