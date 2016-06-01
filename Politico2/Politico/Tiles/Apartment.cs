using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Politico2.Politico.Tiles
{
    public class Apartment : Tile
    {
        Grass grass;

        static Texture2D texture;
        public static Texture2D Texture { get { return texture; } set { texture = value; } }

        static Texture2D texture_light;
        public static Texture2D Texture_Light { get { return texture_light; } set { texture_light = value; } }

        static Texture2D texture_night;
        public static Texture2D Texture_Night { get { return texture_night; } set { texture_night = value; } }

        public Apartment(Vector2 position) : base(texture, position, texture_night)
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

        public override int TileNumber()
        {
            return 9;
        }

        public override void DrawLights(SpriteBatch sbatch, int offsetX, int offsetY)
        {
            if (haspower)
            {
                Rectangle imageRect = new Rectangle((int)position.X - offsetX, (int)position.Y - offsetY, TileWidth, TileHeight);
                float layerDepth = Y * 0.01f;
                sbatch.Draw(texture_light, imageRect, null, selectedTint, 0.0f, Vector2.Zero, SpriteEffects.None, layerDepth + 0.00001f);
            }
        }
    }
}
