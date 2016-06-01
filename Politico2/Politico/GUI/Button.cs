using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Politico2.Politico.Tiles;

namespace Politico2.Politico.GUI
{
    public class Button
    {
        //@Todo: Finsih adding button textures
        public struct Textures
        {
            public static Texture2D BackgroundButton; 
            public static Texture2D HammerButton; 
            public static Texture2D RoadButton;
            public static Texture2D HouseButton;
            public static Texture2D WindTurbineButton;
            public static Texture2D CoalButton;
            public static Texture2D BulldozerButton;
            public static Texture2D GridButton; 
            public static Texture2D CorporationButton;
            public static Texture2D FactoryButton;
            public static Texture2D ApartmentButton;
            public static Texture2D CondoButton;
            public static Texture2D PoliceStationButton;
            public static Texture2D FireStationButton;
            public static Texture2D PowerGridButton;
            public static Texture2D FireGridButton;
            public static Texture2D PoliceGridButton;
            public static Texture2D DisasterButton;
            public static Texture2D TreeButton;
            public static Texture2D WaterButton;
            public static Texture2D PoliceDisasterButton;
            public static Texture2D FireDisasterButton;
            public static Texture2D HelicopterDisasterButton;
            public static Texture2D EarthquakeDisasterButton;
            public static Texture2D AlienAttackDisasterButton;
            public static Texture2D NuclearAttackDisasterButton;
            public static Texture2D WarDisasterButton;
            public static Texture2D StormDisasterButton;
            public static Texture2D WindowButton;
            public static Texture2D OptionsButton; 
        }

        private Texture2D texture;
        private Vector2 position, offset;
        private Rectangle bounds;

        private Tile T; 
        public Tile buttonTile { get { return T; } }

        public const int Width = 125;
        public const int Height = 92;

        float delayTimer = 0f; 

        public Button(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;

            bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public bool isSelected = false;

        public Button(Texture2D texture, Vector2 position, Tile t)
        {
            this.texture = texture;
            this.position = position;
            this.T = t; 

            bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height); 
        }

        public void Update(GameTime gametime, Cursor cursor)
        {
            delayTimer += (float)gametime.ElapsedGameTime.TotalMilliseconds; 
            CheckforIntersection(cursor); 
        }

        public void Update(GameTime gametime, Cursor cursor, Vector2 offset)
        {
            this.offset = offset; 
            bounds = new Rectangle((int)position.X + (int)offset.X, (int)position.Y + (int)offset.Y, texture.Width, texture.Height);

            delayTimer += (float)gametime.ElapsedGameTime.TotalMilliseconds;
            CheckforIntersection(cursor); 
        }

        void CheckforIntersection(Cursor cursor)
        {
            if (delayTimer >= 250)
            {
                if (cursor.Bounds().Intersects(this.bounds))
                {
                    if (cursor.isMouseClicked())
                    {
                        Click();
                        delayTimer = 0f;
                    }
                }
            }
        }

        public delegate void ClickEventTile(Tile thistile, EventArgs e); 
        public event ClickEventTile onClickTile;

        public delegate void ClickEventGrid(string type, EventArgs e);

        private void Click()
        {
            isSelected = !isSelected; 

            if (onClick != null)
                onClick(this, EventArgs.Empty);

            if (onClickTile != null)
                onClickTile(T, EventArgs.Empty);
        }

        public event EventHandler onClick; 

        public void Draw(SpriteBatch sbatch)
        {
            Color c = Color.White;
            if (isSelected)
            {
                c = Color.Black;

                sbatch.Draw(Textures.BackgroundButton, new Rectangle((int)position.X + (int)offset.X,
                    (int)position.Y + (int)offset.Y, texture.Width, texture.Height), Color.White);
            }

            sbatch.Draw(texture, position + offset, c); 
        }
    }
}
