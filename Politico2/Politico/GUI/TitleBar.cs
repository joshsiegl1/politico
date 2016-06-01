using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; 

namespace Politico2.Politico.GUI
{
    public class TitleBar
    {
        public struct Textures
        {
            public static Texture2D TitleBarBackground; 
        }

        private Dictionary<string, Button> Buttons;

        private bool show, inTransition;

        public bool Show { get { return show; } }

        Vector2 position, offset;

        readonly Vector2 HidePosition = new Vector2(0, -150);
        readonly Vector2 ShowPosition = new Vector2(0, 0);

        Button hiddenWindow; 

        public TitleBar()
        {
            position = Vector2.Zero; 

            Buttons = new Dictionary<string, Button>();
            Buttons.Add("Hammer", new Button(Button.Textures.HammerButton, Vector2.Zero));
            Buttons.Add("Bulldozer", new Button(Button.Textures.BulldozerButton, new Vector2(Button.Textures.HammerButton.Width, 0)));
            Buttons.Add("Grid", new Button(Button.Textures.GridButton, new Vector2(Button.Textures.GridButton.Width * 2, 0)));
            Buttons.Add("Disaster", new Button(Button.Textures.DisasterButton, new Vector2(Button.Textures.DisasterButton.Width * 3, 0)));
            Buttons.Add("Window", new Button(Button.Textures.WindowButton, new Vector2(1920 - (Button.Textures.WindowButton.Width * 2), 0)));
            Buttons.Add("Options", new Button(Button.Textures.OptionsButton, new Vector2(1920 - Button.Textures.OptionsButton.Width, 0))); 

            Buttons["Hammer"].onClick += Hammerbtn_onClick;
            Buttons["Bulldozer"].onClick += Bulldozerbtn_onClick;
            Buttons["Grid"].onClick += Gridbtn_onClick;
            Buttons["Disaster"].onClick += Disasterbtn_onClick;
            Buttons["Window"].onClick += Windowbtn_onClick;
            Buttons["Options"].onClick += Optionsbtn_onClick;

            hiddenWindow = new Button(Button.Textures.WindowButton, new Vector2(1920 - Button.Textures.WindowButton.Width, 0));
            hiddenWindow.onClick += HiddenWindowbtn_onClick; 

            show = true; 
        }

        public event EventHandler onOptionsButtonClicked; 
        private void Optionsbtn_onClick(object sender, EventArgs e)
        {
            if (onOptionsButtonClicked != null)
                onOptionsButtonClicked(sender, e); 
        }

        public event EventHandler onWindowButtonClicked; 
        private void Windowbtn_onClick(object sender, EventArgs e)
        {
            if (onWindowButtonClicked != null)
                onWindowButtonClicked(sender, e); 
        }

        public event EventHandler onHiddenWindowClicked; 
        private void HiddenWindowbtn_onClick(object sender, EventArgs e)
        {
            if (onHiddenWindowClicked != null)
                onHiddenWindowClicked(sender, e);

            hiddenWindow.isSelected = false; 
        }

        public EventHandler onDisasterClicked; 
        private void Disasterbtn_onClick(object sender, EventArgs e)
        {
            Deselect("Grid"); 
            if (onDisasterClicked != null)
                onDisasterClicked(sender, e); 
        }

        public EventHandler onGridClicked; 
        private void Gridbtn_onClick(object sender, EventArgs e)
        {
            Deselect("Disaster"); 
            if (onGridClicked != null)
                onGridClicked(this, e); 
        }

        public event EventHandler onBulldozerClicked; 
        private void Bulldozerbtn_onClick(object sender, EventArgs e)
        {
            if (onBulldozerClicked != null)
                onBulldozerClicked(sender, e); 
        }

        public event EventHandler onHammerClicked;
        private void Hammerbtn_onClick(object sender, EventArgs e)
        {
            if (onHammerClicked != null)
                onHammerClicked(sender, e);
        }

        public void Deselect(string buttonName)
        {
            Buttons[buttonName].isSelected = false; 
        }

        public void Toggle()
        {
            if (!inTransition)
                show = !show;
        }

        public void DeselectAll()
        {
            for (int i = 0; i < Buttons.Count; i++)
            {
                Buttons.ElementAt(i).Value.isSelected = false; 
            }
        }

        public void Update(GameTime gametime, Cursor cursor)
        {
            if (show)
            {
                if (position.Y + offset.Y < ShowPosition.Y)
                {
                    offset.Y += 10f;
                    inTransition = true;
                }
                else inTransition = false;
            }
            else
            {
                if (position.Y + offset.Y > HidePosition.Y)
                {
                    offset.Y -= 10f;
                    inTransition = true;
                }
                else inTransition = false;
            }

            foreach (Button b in Buttons.Values)
                b.Update(gametime, cursor, offset);

            if (!show) hiddenWindow.Update(gametime, cursor); 
        }

        //public void Update(GameTime gametime, Cursor cursor)
        //{
        //    foreach (Button b in Buttons.Values)
        //        b.Update(gametime, cursor); 
        //}

        public void Draw(SpriteBatch sbatch)
        {
            if (!show)
                hiddenWindow.Draw(sbatch); 

            sbatch.Draw(Textures.TitleBarBackground, position + offset, Color.White);

            foreach (Button b in Buttons.Values)
                b.Draw(sbatch); 
        }
    }
}
