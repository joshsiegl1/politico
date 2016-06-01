using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Politico2.Politico.Tiles
{
    public class Empty : Tile
    {
        static Texture2D texture;
        public static Texture2D Texture { get { return texture; } set { texture = value; } }


        public Empty(Vector2 position) : base(texture, position) { }

        public override bool IntersectPixels(Rectangle rectangleB, Color[] dataB)
        {
            return false;
        }

        public override void Draw(SpriteBatch sbatch, int offsetX, int offsetY)
        {
            return;
        }

        public override void DrawDebug(SpriteBatch sbatch, int offsetX, int offsetY)
        {
            return; 
        }

        public override bool CanBeDestroyed()
        {
            return false; 
        }

        public override bool EarthQuakeDestroy()
        {
            return false; 
        }

        public override bool CanBeBurnt()
        {
            return false; 
        }

        public override bool CanBeShot()
        {
            return false; 
        }

        public override bool CanBeBombed()
        {
            return false; 
        }

        public override int TileNumber()
        {
            return 0;
        }

        public override void TransmitPower(ref Tile[,] Tiles, ref List<Tile> CheckedTiles)
        {
            return; 
        }

        public override void DrawLights(SpriteBatch sbatch, int offsetx, int offsety)
        {
            return;
        }
    }
}
