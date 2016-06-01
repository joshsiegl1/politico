using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content; 

namespace MenuSystem 
{
    public class CreditsScreen : MenuScreen
    {
        const string firstline = "PROGRAMMING/DESIGN/ART";
        const string secondline = "JOSH SIEGL";

        const string thirdline = "SOUNDTRACK";
        const string fourthline = "NICHOLAS SINGER";

        const string fifthline = "ICONS BY";
        const string sixthline = "FREEPIC @ FLATICON.COM"; 

        MenuScreen backScreen;

        SpriteFont CreditsFont;

        public CreditsScreen(MenuScreen backScreen)
            : base("CREDITS")
        {
            

            this.menuTitleColor = Color.White;
            this.fadeOptions = true;

            this.TitleYPosition = 50; 

            this.backScreen = backScreen;
        }

        public override void LoadContent()
        {
            ContentManager Content = ScreenManager.Game.Content;

            CreditsFont = Content.Load<SpriteFont>("Fonts/creditsfont");

            MenuEntry firstLine = new MenuEntry(firstline);

            firstLine.FontColor = Color.White;
            firstLine.Font = CreditsFont; 
            MenuEntries.Add(firstLine);

            MenuEntry secondLine = new MenuEntry(secondline);
            secondLine.FontColor = Color.White;
            secondLine.Font = CreditsFont;
            MenuEntries.Add(secondLine);

            MenuEntry thirdLine = new MenuEntry(thirdline);
            thirdLine.FontColor = Color.White;
            thirdLine.Font = CreditsFont;
            MenuEntries.Add(thirdLine);

            MenuEntry fourthLine = new MenuEntry(fourthline);
            fourthLine.FontColor = Color.White;
            fourthLine.Font = CreditsFont;
            MenuEntries.Add(fourthLine);

            MenuEntry fifthLine = new MenuEntry(fifthline);
            fifthLine.FontColor = Color.White;
            fifthLine.Font = CreditsFont;
            MenuEntries.Add(fifthLine);

            MenuEntry sixthLine = new MenuEntry(sixthline);
            sixthLine.FontColor = Color.White;
            sixthLine.Font = CreditsFont;
            MenuEntries.Add(sixthLine); 

            this.menuTitleFont = CreditsFont;

            base.LoadContent();
        }

        protected override void OnCancel(PlayerIndex playerIndex)
        {
            ScreenManager.AddScreen(backScreen, PlayerIndex.One);
            ExitScreen(); 
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

                if (i == 1 || i == 3)
                    position.Y += 100; 
            }
        }
    }
}
