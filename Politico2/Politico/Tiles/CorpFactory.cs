using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; 

namespace Politico2.Politico.Tiles
{
    public class CorpFactory : Tile
    {
        public struct Buildings
        {
            static Texture2D corpfactory1; 
            public static Texture2D CorpFactory1 { get { return corpfactory1; } set { corpfactory1 = value; } }

            static Texture2D texture_night;
            public static Texture2D Texture_Night { get { return texture_night; } set { texture_night = value; } }
        }

        public struct Lights
        {
            static Texture2D corpfactory1lights; 
            public static Texture2D CorpFactory1Lights { get { return corpfactory1lights; }  set { corpfactory1lights = value; } }
        }

        public CorpFactory(Vector2 position) : base(Buildings.CorpFactory1, position, Buildings.Texture_Night)
        {

        }

        public override void DrawLights(SpriteBatch sbatch, int offsetx, int offsety)
        {
            if (haspower)
            {
                Rectangle imageRect = new Rectangle((int)position.X - offsetx, (int)position.Y - offsety, TileWidth, TileHeight);
                float layerDepth = Y * 0.01f;
                sbatch.Draw(Lights.CorpFactory1Lights, imageRect, null, selectedTint, 0.0f, Vector2.Zero, SpriteEffects.None, layerDepth + 0.00001f);
            }
        }

        public override int TileNumber()
        {
            return 8; 
        }
    }
}
