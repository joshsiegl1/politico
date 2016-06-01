using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Politico2.Politico.Tiles
{
    public class Grass : Tile
    {
        static Texture2D texture;
        public static Texture2D Texture { get { return texture; } set { texture = value; } }

        static Texture2D texture_night;
        public static Texture2D Texture_Night { get { return texture_night; } set { texture_night = value; } }

        public Grass(Vector2 position) : base(texture, position, texture_night)
        {
            
        }

        public override bool CanBeDestroyed()
        {
            return false; 
        }

        public override bool CanBeReplaced()
        {
            return true; 
        }

        public override bool EarthQuakeDestroy()
        {
            return false; 
        }

        public override bool CanBeBurnt()
        {
            return false;
        }

        public override void TransmitPower(ref Tile[,] Tiles, ref List<Tile> CheckedTiles)
        {
            return; 
        }

        public override int TileNumber()
        {
            return 1;
        }

        public override void DrawLights(SpriteBatch sbatch, int offsetx, int offsety)
        {
            return; 
        }
    }
}
