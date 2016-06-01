using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Politico2.Politico.Tiles; 

namespace Politico2.Politico.GUI
{
    public class ConstructionBar
    {
        public struct Textures
        {
            public static Texture2D SideBarBackground; 
        }

        private bool show, inTransition;

        public bool Show { get { return show; } }

        readonly Vector2 HidePosition = new Vector2(2170, 150);
        readonly Vector2 ShowPosition = new Vector2(1670, 150); 

        Vector2 position, offset;

        List<Button> Buttons; 

        public ConstructionBar()
        {
            position = HidePosition; 
            Buttons = new List<Button>();

            Buttons.Add(new Button(Button.Textures.RoadButton, position , new Road(Vector2.Zero)));
            Buttons.Add(new Button(Button.Textures.HouseButton, position + new Vector2(Button.Width, 0), new House(Vector2.Zero)));
            Buttons.Add(new Button(Button.Textures.WindTurbineButton, position + new Vector2(0, Button.Height + 62), new WindTurbine(Vector2.Zero)));
            Buttons.Add(new Button(Button.Textures.CoalButton, position + new Vector2(Button.Width, Button.Height + 62), new Coal(Vector2.Zero)));
            Buttons.Add(new Button(Button.Textures.CorporationButton, position + new Vector2(0, (Button.Height * 2) + 124), new Corporation(Vector2.Zero)));
            Buttons.Add(new Button(Button.Textures.FactoryButton, position + new Vector2(Button.Width, (Button.Height * 2) + 124), new CorpFactory(Vector2.Zero)));
            Buttons.Add(new Button(Button.Textures.ApartmentButton, position + new Vector2(0, (Button.Height * 3) + 186), new Apartment(Vector2.Zero)));
            Buttons.Add(new Button(Button.Textures.CondoButton, position + new Vector2(Button.Width, (Button.Height * 3) + 186), new Condo(Vector2.Zero)));
            Buttons.Add(new Button(Button.Textures.PoliceStationButton, position + new Vector2(0, (Button.Height * 4) + 248), new PoliceStation(Vector2.Zero)));
            Buttons.Add(new Button(Button.Textures.FireStationButton, position + new Vector2(Button.Width, (Button.Height * 4) + 248), new FireStation(Vector2.Zero)));
            Buttons.Add(new Button(Button.Textures.TreeButton, position + new Vector2(0, (Button.Height * 5) + 310), new Tree(Vector2.Zero)));
            Buttons.Add(new Button(Button.Textures.WaterButton, position + new Vector2(Button.Width, (Button.Height * 5) + 310), new Water(Vector2.Zero)));

            foreach (Button b in Buttons)
            {
                b.onClickTile += ConstructionBar_onClick;
            }
        }

        public delegate void TileSelectEvent(Tile t, EventArgs e);
        public event TileSelectEvent onTileSelect;

        private void ConstructionBar_onClick(Tile thistile, EventArgs e)
        {
            for (int i = 0; i < Buttons.Count; i++)
            {
                if (Buttons[i].buttonTile != thistile)
                    Buttons[i].isSelected = false;
            }

            if (onTileSelect != null)
                onTileSelect(thistile, e);
        }

        public delegate Tile SelectionMadeEvent(); 

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
                if (position.X + offset.X > ShowPosition.X)
                {
                    offset.X -= 10f;
                    inTransition = true;
                }
                else inTransition = false; 
            }
            else
            {
                if (position.X + offset.X < HidePosition.X)
                {
                    offset.X += 10f;
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
