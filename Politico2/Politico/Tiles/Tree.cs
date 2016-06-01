using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Politico2.Politico.Tiles
{
    public class Tree : Tile
    {
        Grass grass;

        static Texture2D texture;
        public static Texture2D Texture { get { return texture; } set { texture = value; } }

        static Texture2D texture_night;
        public static Texture2D Texture_Night { get { return texture_night; } set { texture_night = value; } }

        public Tree(Vector2 position) : base(texture, position, texture_night)
        {
            grass = new Grass(position);
        }

        public override void onPlace(Tile[,] Tiles)
        {
            grass.onPlace(Tiles);
            base.onPlace(Tiles);
        }

        public override void Update(GameTime gametime)
        {
            grass.Update(gametime);
            base.Update(gametime);
        }

        public override void Draw(SpriteBatch sbatch, int offsetX, int offsetY)
        {
            grass.Draw(sbatch, offsetX, offsetY, (Y * 0.01f) - 0.001f);
            base.Draw(sbatch, offsetX, offsetY);
        }

        public override void TransmitPower(ref Tile[,] Tiles, ref List<Tile> CheckedTiles)
        {
            return;
        }

        public override int TileNumber()
        {
            return 14;
        }

        public override void DrawLights(SpriteBatch sbatch, int offsetx, int offsety)
        {
            return;
        }
    }
}
