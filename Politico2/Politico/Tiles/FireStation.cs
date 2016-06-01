using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Politico2.Politico.Tiles
{
    public class FireStation : Tile
    {
        static Texture2D texture;
        public static Texture2D Texture { get { return texture; } set { texture = value; } }

        static Texture2D texture_light;
        public static Texture2D Texture_Light { get { return texture_light; } set { texture_light = value; } }

        static Texture2D texture_night;
        public static Texture2D Texture_Night { get { return texture_night; } set { texture_night = value; } }

        public FireStation(Vector2 position) : base(texture, position, texture_night) { }

        public override void onPlace(Tile[,] Tiles)
        {
            for (int x = this.X - 5; x < this.X + 5; x++)
            {
                for (int y = this.Y - 5; y < this.Y + 5; y++)
                {
                    if (y >= 0 && y <= Grid.GridHeight - 1 && x >= 0 && x <= Grid.GridWidth - 1)
                        Tiles[x, y].FireZone = true;
                }

            }

            base.onPlace(Tiles);
        }

        public override void DrawLights(SpriteBatch sbatch, int offsetx, int offsety)
        {
            if (haspower)
            {
                Rectangle imageRect = new Rectangle((int)position.X - offsetx, (int)position.Y - offsety, TileWidth, TileHeight);
                float layerDepth = Y * 0.01f;
                sbatch.Draw(texture_light, imageRect, null, selectedTint, 0.0f, Vector2.Zero, SpriteEffects.None, layerDepth + 0.00001f);
            }
        }

        public override int TileNumber()
        {
            return 12;
        }
    }
}
