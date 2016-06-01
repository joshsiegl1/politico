using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Politico2.Politico.Tiles;

namespace Politico2.Politico.GUI
{
    public class GridBar
    {
        public struct Textures
        {
            public static Texture2D SideBarBackground;
        }

        private bool show, inTransition;

        public bool Show { get { return show; } }

        readonly Vector2 HidePosition = new Vector2(-250, 150);
        readonly Vector2 ShowPosition = new Vector2(0, 150);

        Vector2 position, offset;

        List<Button> Buttons;

        public GridBar()
        {
            position = HidePosition;
            Buttons = new List<Button>();

            Buttons.Add(new Button(Button.Textures.PowerGridButton, position));
            Buttons.Add(new Button(Button.Textures.FireGridButton, position + new Vector2(Button.Textures.FireGridButton.Width, 0)));
            Buttons.Add(new Button(Button.Textures.PoliceGridButton, position + new Vector2(0, Button.Textures.PoliceGridButton.Height))); 

            Buttons[0].onClick += PowerGridButton_onClick;
            Buttons[1].onClick += FireGridButton_onClicked;
            Buttons[2].onClick += PoliceGridButton_onClicked; 

        }

        public event EventHandler onPowerClicked;
        private void PowerGridButton_onClick(object sender, EventArgs e)
        {
            Deselect(sender); 
            if (onPowerClicked != null)
            {
                onPowerClicked(sender, e);
            }
        }

        public event EventHandler onFireClicked;
        private void FireGridButton_onClicked(object sender, EventArgs e)
        {
            Deselect(sender);
            if (onFireClicked != null)
            {
                onFireClicked(sender, e); 
            }
        }

        public event EventHandler onPoliceClicked;
        private void PoliceGridButton_onClicked(object sender, EventArgs e)
        {
            Deselect(sender);
            if (onPoliceClicked != null)
            {
                onPoliceClicked(sender, e);
            }
        }

        void Deselect(object sender)
        {
            for (int i = 0; i < Buttons.Count; i++)
            {
                if (Buttons[i] != sender)
                    Buttons[i].isSelected = false; 
            }
        }

        public void DeselectAll()
        {
            for (int i = 0; i < Buttons.Count; i++)
                Buttons[i].isSelected = false;
        }

        public void Toggle()
        {
            if (!inTransition)
                show = !show;
        }

        public void Update(GameTime gametime, Cursor cursor)
        {
            if (show)
            {
                if (position.X + offset.X < ShowPosition.X)
                {
                    offset.X += 10f;
                    inTransition = true;
                }
                else inTransition = false;
            }
            else
            {
                if (position.X + offset.X > HidePosition.X)
                {
                    offset.X -= 10f;
                    inTransition = true;
                }
                else inTransition = false;
            }

            foreach (Button b in Buttons)
                b.Update(gametime, cursor, offset);
        }

        public void Draw(SpriteBatch sbatch)
        {
            sbatch.Draw(Textures.SideBarBackground, new Rectangle((int)position.X + (int)offset.X, (int)position.Y + (int)offset.Y, Textures.SideBarBackground.Width, 1080), Color.White);
            foreach (Button b in Buttons)
                b.Draw(sbatch);
        }
    }
}
