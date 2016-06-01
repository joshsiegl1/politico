using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Politico2.Politico.Tiles;

namespace Politico2.Politico.GUI
{
    public class DisasterBar
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

        public DisasterBar()
        {
            position = HidePosition;
            Buttons = new List<Button>();

            Buttons.Add(new Button(Button.Textures.FireDisasterButton, HidePosition));
            Buttons.Add(new Button(Button.Textures.PoliceDisasterButton, new Vector2(HidePosition.X + Button.Width, HidePosition.Y)));
            Buttons.Add(new Button(Button.Textures.HelicopterDisasterButton, new Vector2(HidePosition.X, HidePosition.Y + Button.Height + 139)));
            Buttons.Add(new Button(Button.Textures.EarthquakeDisasterButton, new Vector2(HidePosition.X + Button.Width, HidePosition.Y + Button.Height + 139)));
            Buttons.Add(new Button(Button.Textures.AlienAttackDisasterButton, new Vector2(HidePosition.X, HidePosition.Y + (Button.Height * 2) + 278)));
            Buttons.Add(new Button(Button.Textures.NuclearAttackDisasterButton, new Vector2(HidePosition.X + Button.Width, HidePosition.Y + (Button.Height * 2) + 278)));
            Buttons.Add(new Button(Button.Textures.WarDisasterButton, new Vector2(HidePosition.X, HidePosition.Y + (Button.Height * 3) + 417)));
            Buttons.Add(new Button(Button.Textures.StormDisasterButton, new Vector2(HidePosition.X + Button.Width, HidePosition.Y + (Button.Height * 3) + 417))); 


            Buttons[0].onClick += DisasterBarFire_onClick;
            Buttons[1].onClick += DisasterBarRiot_onClick;
            Buttons[2].onClick += DisasterBarHelicopter_onClick;
            Buttons[3].onClick += DisasterBarEarthQuake_onClick;
            Buttons[4].onClick += DisasterBarAlien_onClick;
            Buttons[5].onClick += DisasterNuclear_onClick;
            Buttons[6].onClick += DisasterWar_onClick;
            Buttons[7].onClick += DisasterStorm_onClick;

        }

        public event EventHandler onStormClicked; 
        private void DisasterStorm_onClick(object sender, EventArgs e)
        {
            Deselect(sender);
            if (onStormClicked != null)
                onStormClicked(sender, e); 
        }

        public event EventHandler onWarClicked; 
        private void DisasterWar_onClick(object sender, EventArgs e)
        {
            Deselect(sender);
            if (onWarClicked != null)
                onWarClicked(sender, e); 
        }

        public event EventHandler onNuclearClicked;
        private void DisasterNuclear_onClick(object sender, EventArgs e)
        {
            Deselect(sender);
            if (onNuclearClicked!= null)
                onNuclearClicked(sender, e);
        }

        public event EventHandler onAlienClicked;
        private void DisasterBarAlien_onClick(object sender, EventArgs e)
        {
            Deselect(sender);
            if (onAlienClicked != null)
                onAlienClicked(sender, e);
        }

        public event EventHandler onEarthQuakeClicked; 
        private void DisasterBarEarthQuake_onClick(object sender, EventArgs e)
        {
            Deselect(sender);
            if (onEarthQuakeClicked != null)
                onEarthQuakeClicked(sender, e); 
        }

        public event EventHandler onHelicopterClicked; 
        private void DisasterBarHelicopter_onClick(object sender, EventArgs e)
        {
            Deselect(sender);
            if (onHelicopterClicked != null)
                onHelicopterClicked(sender, e); 
        }

        public event EventHandler onRiotClicked; 
        private void DisasterBarRiot_onClick(object sender, EventArgs e)
        {
            Deselect(sender); 
            if (onRiotClicked != null)
                onRiotClicked(sender, e); 
        }

        public event EventHandler onFireClicked; 
        private void DisasterBarFire_onClick(object sender, EventArgs e)
        {
            Deselect(sender);
            if (onFireClicked != null)
                onFireClicked(sender, e); 
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
