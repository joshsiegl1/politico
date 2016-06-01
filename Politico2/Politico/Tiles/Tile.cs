using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Politico2.Politico.Tiles
{
    public interface IPowerable
    {
        bool haspower { get; set; }
        bool baseStation { get; set; }
        void TransmitPower(ref Tile[,] Tiles, ref List<Tile> CheckedTiles);
        void DrawLights(SpriteBatch sbatch, int offsetx, int offsety);
    }

    public abstract class Tile : IPowerable
    {
        static int KeyTracker = 0;
        public int UniqueKey; 

        public const int TileWidth = 64;
        public const int TileHeight = 64;

        public const int TileStepX = 64;
        public const int TileStepY = 16;

        public const int OddRowXOffset = 32;
        public const int HeightTileOffset = 32;

        public readonly int X, Y;

        protected readonly bool isOdd;

        private Texture2D texture;
        public Texture2D ParentTexture { get { return texture; } }

        private Texture2D texture_night; 
        public Texture2D ParentTextureNight { get { return texture_night; } }

        protected Vector2 position;
        public Vector2 Position { get { return position; } }

        protected Rectangle bounds;

        protected Color selectedTint = Color.White;

        //This is used for pixel perfect collision detection
        private Color[] tileTextureData;

        public bool FireZone, PoliceZone, ElectricZone;

        public bool onFire = false;

        private bool destroy = false; 
        public bool Destroy { get { return destroy; } }

        public Tile(Texture2D texture, Vector2 position)
        {
            KeyTracker++;
            this.UniqueKey = KeyTracker; 

            this.texture = texture;
            this.position = position;

            startingPosition = position; 

            this.bounds = Rectangle.Empty;

            //Create and set the color data of the tile used for collision detection
            tileTextureData = new Color[texture.Width * texture.Height];
            Grass.Texture.GetData(tileTextureData);

            Y = (int)position.Y / Tile.TileStepY;

            int rowOffset = 0;
            if (Y % 2 == 1)
            {
                rowOffset = Tile.OddRowXOffset;
                isOdd = true;
            }

            int xPos = (int)position.X - rowOffset;

            X = xPos / Tile.TileStepX;

            layerDepth = Y * 0.01f;
        }

        public Tile(Texture2D texture, Vector2 position, Texture2D texture_night)
        {
            KeyTracker++;
            this.UniqueKey = KeyTracker;

            this.texture = texture;
            this.position = position;
            this.texture_night = texture_night; 

            this.bounds = Rectangle.Empty;

            startingPosition = position;

            //Create and set the color data of the tile used for collision detection
            tileTextureData = new Color[texture.Width * texture.Height];
            Grass.Texture.GetData(tileTextureData);

            Y = (int)position.Y / Tile.TileStepY;

            int rowOffset = 0;
            if (Y % 2 == 1)
            {
                rowOffset = Tile.OddRowXOffset;
                isOdd = true;
            }

            int xPos = (int)position.X - rowOffset;

            X = xPos / Tile.TileStepX;

            layerDepth = Y * 0.01f;
        }

        protected void UpdateBaseTexture(Texture2D texture)
        {
            this.texture = texture; 
        }

        protected void UpdateBaseTextureNight(Texture2D texture_night)
        {
            this.texture_night = texture_night; 
        }

        public abstract int TileNumber();

        float burnTimer = 0f; 
        public virtual void Update(GameTime gametime)
        {
            if (earthQuake)
            {
                if (position.Y < startingPosition.Y + 50)
                    position.Y++;
                else destroy = true;            
            }

            if (onFire && CanBeBurnt() && !FireZone)
            {
                burnTimer += (float)gametime.ElapsedGameTime.TotalMilliseconds;
                if (burnTimer >= 5000f)
                    destroy = true; 
            }
        }

        public void Reset()
        {
            if (baseStation) haspower = true;
            else haspower = false;

            ElectricZone = FireZone = PoliceZone = false; 
        }

        public virtual void onPlace(Tile[,] Tiles)
        {
        }

        public virtual bool CanBeDestroyed()
        {
            return true; 
        }

        public virtual bool CanBeBurnt()
        {
            return true; 
        }

        public virtual bool CanBeShot()
        {
            return true; 
        }

        public virtual bool CanBeBombed()
        {
            return true; 
        }

        public virtual bool CanBeReplaced()
        {
            return false; 
        }

        protected void SetTexture(Texture2D texture)
        {
            this.texture = texture; 
        }

        bool earthQuake = false;
        Vector2 startingPosition; 
        public Vector2 StartPosition { get { return startingPosition; } }
        public virtual bool EarthQuakeDestroy()
        {
            earthQuake = true;
            return true; 
        }

        protected void SetTextureNight(Texture2D texture_night)
        {
            this.texture_night = texture_night; 
        }

        public void setSelectedTint(Color c)
        {
            selectedTint = c; 
        }

        float layerDepth;
        public float LayerDepth { get { return layerDepth; } }
        public virtual void Draw(SpriteBatch sbatch, int offsetX, int offsetY)
        {
            Rectangle imageRect = new Rectangle((int)position.X - offsetX, (int)position.Y - offsetY, TileWidth, TileHeight);
            bounds = imageRect;
            sbatch.Draw(texture, imageRect, null, selectedTint * Night.DayColor, 0.0f, Vector2.Zero, SpriteEffects.None, layerDepth + Night.DayAdditive);
            sbatch.Draw(texture_night, imageRect, null, selectedTint * Night.NightColor, 0.0f, Vector2.Zero, SpriteEffects.None, layerDepth + Night.NightAdditive);

        }

        public virtual void Draw(SpriteBatch sbatch, int offsetX, int offsetY, float LayerDepth)
        {
            Rectangle imageRect = new Rectangle((int)position.X - offsetX, (int)position.Y - offsetY, TileWidth, TileHeight);
            bounds = imageRect;
            sbatch.Draw(texture, imageRect, null, selectedTint * Night.DayColor, 0.0f, Vector2.Zero, SpriteEffects.None, LayerDepth + Night.DayAdditive);
            sbatch.Draw(texture_night, imageRect, null, selectedTint * Night.NightColor, 0.0f, Vector2.Zero, SpriteEffects.None, LayerDepth + Night.NightAdditive);
        }

        public virtual void DrawZone(MainUserInterface.GridSelection gridselection)
        {
            switch(gridselection)
            {
                case MainUserInterface.GridSelection.Power:
                    if (ElectricZone) setSelectedTint(Color.Yellow); 
                    break;
                case MainUserInterface.GridSelection.Police:
                    if (PoliceZone) setSelectedTint(Color.Blue);
                    break;
                case MainUserInterface.GridSelection.Fire:
                    if (FireZone) setSelectedTint(Color.Orange);
                    break; 
            }
        }

        public bool drawDebug; 
        public virtual void DrawDebug(SpriteBatch sbatch, int offsetX, int offsetY)
        {
            Vector2 position = new Vector2(this.position.X - offsetX, this.position.Y - offsetY);

            if (drawDebug)
            {
                sbatch.DrawString(Assets.debugFont, "Is odd: " + isOdd.ToString(), new Vector2(0, 400), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f); 
                sbatch.DrawString(Assets.debugFont, "X: " + X.ToString() + " Y: " + Y.ToString(), new Vector2(0, 450), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                sbatch.DrawString(Assets.debugFont, "Layer Depth: " + layerDepth.ToString(), new Vector2(0, 500), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                sbatch.DrawString(Assets.debugFont, "Has Power: " + haspower.ToString(), new Vector2(0, 550), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                sbatch.DrawString(Assets.debugFont, "On Fire: " + onFire.ToString(), new Vector2(0, 650), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            }

            
        }

        public bool haspower
        {
            get; set;
        }

        public bool baseStation
        {
            get; set;
        }

        public virtual void TransmitPower(ref Tile[,] Tiles, ref List<Tile> CheckedTiles)
        {
            if (CheckedTiles.Count <= 0) CheckedTiles.Add(this);

            if (isOdd)
            {
                SetTilePower(ref Tiles, ref CheckedTiles, X + 1, Y - 1);
                SetTilePower(ref Tiles, ref CheckedTiles, X, Y - 1);
                SetTilePower(ref Tiles, ref CheckedTiles, X, Y + 1);
                SetTilePower(ref Tiles, ref CheckedTiles, X + 1, Y + 1);
            }
            else
            {
                SetTilePower(ref Tiles, ref CheckedTiles, X, Y - 1);
                SetTilePower(ref Tiles, ref CheckedTiles, X - 1, Y - 1);
                SetTilePower(ref Tiles, ref CheckedTiles, X, Y + 1);
                SetTilePower(ref Tiles, ref CheckedTiles, X - 1, Y + 1);
            }
        }

        void SetTilePower(ref Tile[,] Tiles, ref List<Tile> CheckedTiles, int x, int y)
        {
            if (CheckedTiles.Contains(Tiles[x, y])) return;
            if (!Tiles[x, y].ElectricZone)
            {
                CheckedTiles.Add(Tiles[x, y]);
                return; 
            }

            //if (Tiles[x, y] is PowerTile)
            {
                //PowerTile t = Tiles[x, y] as PowerTile;

                if (Tiles[x,y].haspower == haspower || Tiles[x,y].baseStation)
                {
                    CheckedTiles.Add(Tiles[x,y]);
                    return;
                }

                Tiles[x,y].haspower = haspower;
                CheckedTiles.Add(Tiles[x,y]);
                Tiles[x,y].TransmitPower(ref Tiles, ref CheckedTiles);
                Tiles[x, y] = Tiles[x,y];
            }
        }

        public abstract void DrawLights(SpriteBatch sbatch, int offsetx, int offsety);

        public virtual bool IntersectPixels(Rectangle rectangleB, Color[] dataB)
        {
            // Find the bounds of the rectangle intersection
            int top = Math.Max(bounds.Top, rectangleB.Top);
            int bottom = Math.Min(bounds.Bottom, rectangleB.Bottom);
            int left = Math.Max(bounds.Left, rectangleB.Left);
            int right = Math.Min(bounds.Right, rectangleB.Right);

            // Check every point within the intersection bounds
            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    // Get the color of both pixels at this point
                    Color colorA = tileTextureData[(x - bounds.Left) +
                                         (y - bounds.Top) * bounds.Width];
                    Color colorB = dataB[(x - rectangleB.Left) +
                                         (y - rectangleB.Top) * rectangleB.Width];

                    // If both pixels are not completely transparent,
                    if (colorA.A != 0 && colorB.A != 0)
                    {

                        selectedTint = Color.Red;
                        // then an intersection has been found
                        return true;
                    }
                }
            }

            // No intersection found
            selectedTint = Color.White;
            return false;
        }
    }
}
