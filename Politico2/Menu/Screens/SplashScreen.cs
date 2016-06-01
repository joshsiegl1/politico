using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content; 
using Microsoft.Xna.Framework.Graphics;

using Microsoft.Xna.Framework.Audio; 

namespace MenuSystem.Screens
{
    public class SplashScreen : MenuScreen
    {
        MenuScreen NextScreen; 

        ContentManager content; 
        Texture2D logo;
        

        float ActiveTimer;
        readonly float ActiveTime;

        Vector2 logoPosition;
        
        public SplashScreen(MenuScreen nextScreen)
            : base("")
        {
            ActiveTime = 1000f;
            this.NextScreen = nextScreen; 
        }

        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            logo = content.Load<Texture2D>("Menu/Graphics/Title");
        }

        protected override void OnCancel(PlayerIndex playerIndex)
        {
             
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            if (IsActive)
            {
                ActiveTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (ActiveTimer >= ActiveTime)
                {
                    ScreenManager.AddScreen(NextScreen, ControllingPlayer); 
                    ExitScreen();

                    //MonoGameSample.Monetization.MyInterstitial.ShowRandom(); 
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spritebatch = ScreenManager.SpriteBatch;

            spritebatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, ScreenManager.Scale);
 

            spritebatch.Draw(logo, new Rectangle((int)logoPosition.X, (int)logoPosition.Y,
                ScreenManager.Viewport.Width,
                ScreenManager.Viewport.Height), Color.White * TransitionAlpha);

            spritebatch.End();

            base.Draw(gameTime);
        }
    }
}
