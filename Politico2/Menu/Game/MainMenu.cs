using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content; 
using Microsoft.Xna.Framework.Graphics; 

namespace MenuSystem.Menu.Game
{
    public class MainMenu : MenuScreen
    {
        public MainMenu() : base("")
        {
            fadeOptions = true; 
        }

        public override void LoadContent()
        {
            ContentManager Content = ScreenManager.Game.Content;

            SpriteFont font = Content.Load<SpriteFont>("Fonts/menufont"); 
            Texture2D backGround = Content.Load<Texture2D>("Menu/Graphics/menu_button_background");

            ButtonEntry playButton = new ButtonEntry("PLAY", backGround, backGround, font);
            ButtonEntry creditsButton = new ButtonEntry("CREDITS", backGround, backGround, font); 
            ButtonEntry exitButton = new ButtonEntry("EXIT", backGround, backGround, font); 

            playButton.Selected += PlayButton_Selected;
            creditsButton.Selected += CreditsButton_Selected;
            exitButton.Selected += ExitButton_Selected;

            MenuEntries.Add(playButton);
            MenuEntries.Add(creditsButton); 
            MenuEntries.Add(exitButton);

            base.LoadContent();
        }

        private void CreditsButton_Selected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new CreditsScreen(this), PlayerIndex.One); 
        }

        private void ExitButton_Selected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.Game.Exit(); 
        }

        protected override void UpdateMenuEntryLocations()
        {

            // start at Y = 175; each X value is generated per entry
            Vector2 position = new Vector2(0f, 200f);

            // update each menu entry's location in turn

            for (int i = 0; i < MenuEntries.Count; i++)
            {
                MenuEntry menuEntry = MenuEntries[i];

                // each entry is to be centered horizontally
                position.X = ScreenManager.Viewport.Width / 2 - menuEntry.GetWidth(this) / 2; //override this to put buttons next to each 

                // set the entry's position
                menuEntry.Position = position;

                // move down for the next entry the size of this entry plus our padding
                position.Y += menuEntry.GetHeight(this) + (menuEntryPadding * 2);
            }
        }

        private void PlayButton_Selected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new MainGame(), PlayerIndex.One); 
        }
    }
}
