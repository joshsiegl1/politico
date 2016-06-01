using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Politico2.Politico;
using Politico2.Menu.Game; 

namespace MenuSystem.Menu.Game
{
    public class MainGame : MenuScreen
    {
        public static bool Pause; 
        PoliticoGame game; 
        public MainGame() : base("")
        {

        }

        private void Game_onOptionsButtonClicked(object sender, EventArgs e)
        {
            ScreenManager.AddScreen(new OptionsScreen(), PlayerIndex.One);
            Pause = true; 
        }

        public override void LoadContent()
        {
            Assets.LoadContent(ScreenManager.Game.Content);

            game = new PoliticoGame(ScreenManager);
            game.onOptionsButtonClicked += Game_onOptionsButtonClicked;

            base.LoadContent();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            if (!Pause)
                game.Update(gameTime); 

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spritebatch = ScreenManager.SpriteBatch;

            //spritebatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, ScreenManager.Scale);

            game.Draw(spritebatch, ScreenManager.Scale, ScreenManager.GraphicsDevice, ScreenManager); 

            //spritebatch.End(); 

            base.Draw(gameTime);
        }
    }
}
