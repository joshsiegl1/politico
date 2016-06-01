#region File Description
//-----------------------------------------------------------------------------
// MessageBoxScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace MenuSystem
{
    /// <summary>
    /// A popup message box screen, used to display "are you sure?"
    /// confirmation messages.
    /// </summary>
    public class MessageBoxScreen : MenuScreen
    {
        #region Fields

        string message;
        Texture2D background;
        SpriteFont Font; 

        #endregion

        #region Events

        public event EventHandler<PlayerIndexEventArgs> Accepted;
        public event EventHandler<PlayerIndexEventArgs> Cancelled;

        #endregion

        #region Initialization

        public Color FontColor = Color.Black; 


        /// <summary>
        /// Constructor automatically includes the standard "A=ok, B=cancel"
        /// usage text prompt.
        /// </summary>
        public MessageBoxScreen(string message)
            : this(message, null, null, true)
        {
            EnabledGestures = Microsoft.Xna.Framework.Input.Touch.GestureType.Tap; 
        }


        /// <summary>
        /// Constructor lets the caller specify whether to include the standard
        /// "A=ok, B=cancel" usage text prompt.
        /// </summary>
        public MessageBoxScreen(string message, Texture2D background, SpriteFont font, bool includeUsageText)
            : base("")
        {
            const string usageText = "\nA button, Space, Enter = ok" +
                                     "\nB button, Esc = cancel";

            EnabledGestures = Microsoft.Xna.Framework.Input.Touch.GestureType.Tap; 

            if (includeUsageText)
                this.message = message + usageText;
            else
                this.message = message;

            IsPopup = true;

            this.background = background;
            this.Font = font; 

            TransitionOnTime = TimeSpan.FromSeconds(0.2);
            TransitionOffTime = TimeSpan.FromSeconds(0.2);

            MenuEntry yesButton = new MenuEntry("YES");
            yesButton.Font = font;
            yesButton.FontColor = Color.White;

            yesButton.Selected += yesButton_Selected;

            MenuEntry noButton = new MenuEntry("NO");
            noButton.Font = font;
            noButton.FontColor = Color.White;

            noButton.Selected += noButton_Selected;

            MenuEntries.Add(yesButton);
            MenuEntries.Add(noButton);

            fadeOptions = true; 
        }

        void noButton_Selected(object sender, PlayerIndexEventArgs e)
        {
            if (Cancelled != null)
                Cancelled(sender, e);

            ExitScreen(); 
        }

        void yesButton_Selected(object sender, PlayerIndexEventArgs e)
        {
            if (Accepted != null)
                Accepted(sender, e);

            ExitScreen(); 
        }

        /// <summary>
        /// Creates a new MessageBox 
        /// </summary>
        public MessageBoxScreen Instance
        {
            get 
            {
                MessageBoxScreen box = new MessageBoxScreen(this.message, this.background, this.Font, false);
                box.FontColor = this.FontColor;
                box.Cancelled = Cancelled;
                box.Accepted = Accepted; 
                return box; 
            }
        }

        #endregion

        #region Handle Input

        protected override void UpdateMenuEntryLocations()
        {
            Vector2 position = new Vector2(0f, 900f);

            for (int i = 0; i < MenuEntries.Count; i++)
            {
                MenuEntry menuEntry = MenuEntries[i];

                // each entry is to be centered horizontally
                position.X = 100 + (i * 700); //override this to put buttons next to each 

                // set the entry's position
                menuEntry.Position = position;
            }
        }


        #endregion

        #region Draw


        /// <summary>
        /// Draws the message box.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            SpriteFont font = ScreenManager.Font;

            if (Font != null)
                font = Font; 

            // Darken down any other screens that were drawn beneath the popup.
            ScreenManager.FadeBackBufferToBlack(TransitionAlpha * 2 / 3);

            // Center the message text in the viewport.
            Viewport viewport = ScreenManager.Viewport;
            Vector2 viewportSize = new Vector2(viewport.Width, viewport.Height);
            Vector2 textSize = new Vector2(font.MeasureString(message).X,  font.MeasureString(message).Y * 2);
            Vector2 textPosition = (viewportSize - textSize) / 2;

            // The background includes a border somewhat larger than the text itself.
            const int hPad = 32;
            const int vPad = 16;

            textPosition.X = hPad; 

            Rectangle backgroundRectangle = new Rectangle((int)0,
                                                          (int)textPosition.Y - vPad,
                                                          (int)viewportSize.X,
                                                          (int)textSize.Y + vPad * 2);


            if (background != null)
            {
                backgroundRectangle = new Rectangle((int)0,
                                                          (int)(viewportSize.Y / 2) - (background.Height / 2),
                                                          (int)background.Width,
                                                          (textSize.Y > background.Height) ? (int)textSize.Y + vPad * 2 : background.Height);

                textPosition.X = (background.Width / 2) - (textSize.X / 2); 
            }

            // Fade the popup alpha during transitions.
            Color fontcolor = FontColor * TransitionAlpha;
            Color bgcolor = Color.White * TransitionAlpha;

            Texture2D whitePixel = new Texture2D(ScreenManager.GraphicsDevice, 1, 1);
            Color[] data = new Color[1];
            data[0] = Color.White; 
            whitePixel.SetData(data);

            spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, ScreenManager.Scale);

             //Draw the background rectangle.
            spriteBatch.Draw((background == null) ? whitePixel : background, backgroundRectangle, bgcolor);

             //Draw the message box text.
            spriteBatch.DrawString(font, message, textPosition, fontcolor);

            spriteBatch.End();

            base.Draw(gameTime); 
        }


        #endregion
    }
}
