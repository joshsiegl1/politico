using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content; 
using Microsoft.Xna.Framework.Graphics; 

using MenuSystem;

using MenuSystem.Menu.Game; 

namespace Politico2.Menu.Game
{
    public class OptionsScreen : MenuScreen
    {
        public OptionsScreen() : base("")
        {
            IsPopup = true;
            fadeOptions = true; 
        }

        public override void LoadContent()
        {
            ContentManager Content = ScreenManager.Game.Content;

            SpriteFont font = Content.Load<SpriteFont>("Fonts/menufont");
            Texture2D backGround = Content.Load<Texture2D>("Menu/Graphics/menu_button_background");

            ButtonEntry Quit = new ButtonEntry("QUIT", backGround, backGround, font); 
            ButtonEntry Back = new ButtonEntry("BACK", backGround, backGround, font);

            Quit.Selected += Quit_Selected;
            Back.Selected += Back_Selected;


            MenuEntries.Add(Quit); 
            MenuEntries.Add(Back); 

            base.LoadContent();
        }

        private void Quit_Selected(object sender, PlayerIndexEventArgs e)
        {
             
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

        private void Back_Selected(object sender, PlayerIndexEventArgs e)
        {
            ExitScreen();
            MainGame.Pause = false; 
        }
    }
}
