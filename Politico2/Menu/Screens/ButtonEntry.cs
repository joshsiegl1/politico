using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

using System.Threading;  

namespace MenuSystem
{
    public class ButtonEntry : MenuEntry
    {
        /// <summary>
        /// The texture being used to draw the button 
        /// </summary>
        Texture2D currentTexture;

        /// <summary>
        /// The main button graphic 
        /// </summary>
        Texture2D mainTexture;

        /// <summary>
        /// The pressed button graphic
        /// </summary>
        Texture2D pressedTexture;

        /// <summary>
        /// The spritefont used for the button's text
        /// </summary>
        SpriteFont textFont; 

        bool buttonPressed;

        /// <summary>
        /// Defualt contructor, uses default button graphic for drawing
        /// </summary>
        /// <param name="text">The button Text</param>
        public ButtonEntry(string text) :
            base(text)
        {

        }

        /// <summary>
        /// Constructor overload allows for a custom button texture
        /// </summary>
        /// <param name="text">The button Text</param>
        /// <param name="main">The main button texture</param>
        /// <param name="pressed">The pressed button texture</param>
        public ButtonEntry(string text, Texture2D main,
            Texture2D pressed, SpriteFont textFont) :
            base(text)
        {
            this.mainTexture = main;
            this.pressedTexture = pressed;
            this.textFont = textFont; 
        }

        public override void Reset()
        {
            buttonPressed = false; 
            base.Reset();
        }

        protected internal override void OnSelectEntry(PlayerIndex playerIndex)
        {
            buttonPressed = true;
            base.OnSelectEntry(playerIndex);
        }

        
        public override void Draw(MenuScreen screen, bool isSelected, GameTime gameTime, bool fade)
        {
            Texture2D bttnTexture;
            Texture2D bttnpressedTexture;

            //Check to make sure we haven't defined a texture in the constructor
            if (mainTexture == null)
            {
                bttnTexture = screen.ScreenManager.ButtonTexture;
                bttnpressedTexture = screen.ScreenManager.ButtonPressedTexture;
            }
            else
            {
                bttnTexture = this.mainTexture;
                bttnpressedTexture = this.pressedTexture; 
            }

            ScreenManager screenManager = screen.ScreenManager;
            SpriteBatch spriteBatch = screenManager.SpriteBatch;
            SpriteFont font = screenManager.Font;

#if WINDOWS_PHONE
            isSelected = false;
#endif

            // Draw the selected entry in yellow, otherwise check to see if the font color was set, if it wasn't use Black.
            Color color = Color.White; //isSelected ? Color.White : fontColor;

            // Pulsate the size of the selected menu entry.
            double time = gameTime.TotalGameTime.TotalSeconds;

            //float pulsate = (float)Math.Sin(time * 6) + 1;

            float scale = 1; // + pulsate * 0.05f * selectionFade;

            // Modify the alpha to fade text out during transitions.
            if (fade)
                color *= screen.TransitionAlpha;

            if (!buttonPressed)
                currentTexture = bttnTexture;
            else
                currentTexture = bttnpressedTexture;

            if (textFont != null)
                font = textFont; 

            spriteBatch.Draw(currentTexture, Position, (fade) ? Color.White * screen.TransitionAlpha : Color.White);

            Vector2 origin = new Vector2(0, font.LineSpacing / 2);

            Vector2 fontPosition = new Vector2(position.X + (GetWidth(screen) / 2) - (font.MeasureString(text).X / 2),
                position.Y + (GetHeight(screen) / 2)); 

            spriteBatch.DrawString((textFont == null) ? font : textFont, text, fontPosition, color, 0,
                                   origin, scale, SpriteEffects.None, 0);
        }

        public override int GetHeight(MenuScreen screen)
        {
            if (this.mainTexture == null)
                return screen.ScreenManager.ButtonTexture.Height;
            else return mainTexture.Height; 
        }

        public override int GetWidth(MenuScreen screen)
        {
            if (this.mainTexture == null)
                return screen.ScreenManager.ButtonTexture.Width;
            else return mainTexture.Width; 
        }
    }
}
