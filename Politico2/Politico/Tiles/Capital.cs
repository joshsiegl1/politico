using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Politico2.Politico.Tiles
{
    public class Capital : Tile
    {
        static Texture2D texture;
        public static Texture2D Texture { get { return texture; } set { texture = value; } }

        static Texture2D texture_light; 
        public static Texture2D Texture_Light { get { return texture_light; } set { texture_light = value; } }

        static Texture2D flagTexture; 
        public static Texture2D FlagTexture { get { return flagTexture; } set { flagTexture = value; } }

        static Texture2D flagTextureNight;
        public static Texture2D FlagTextureNight { get { return flagTextureNight; } set { flagTextureNight = value; } }

        static Texture2D texture_night;
        public static Texture2D Texture_Night { get { return texture_night; } set { texture_night = value; } }

        private Animation flagAnimation;

        public Capital(Vector2 position) : base(texture, position, texture_night)
        {
            flagAnimation = new Animation(Vector2.Zero, 25, 40, 8, true, 16); 
        }

        public override void Update(GameTime gametime)
        {
            flagAnimation.UpdateSpriteSheet(gametime);
            base.Update(gametime);
        }

        public override bool CanBeDestroyed()
        {
            return false; 
        }

        public override bool CanBeShot()
        {
            return false; 
        }

        public override bool CanBeBurnt()
        {
            return false; 
        }

        public override bool CanBeBombed()
        {
            return false; 
        }

        public override bool EarthQuakeDestroy()
        {
            return false; 
        }

        public override void Draw(SpriteBatch sbatch, int offsetX, int offsetY)
        {
            base.Draw(sbatch, offsetX, offsetY);

            sbatch.Draw(flagTexture, new Vector2(Position.X + 30 - offsetX, Position.Y - 28 - offsetY), flagAnimation.SourceRect, selectedTint * Night.DayColor, 0f, Vector2.Zero, 1f, SpriteEffects.None, (Y * 0.01f) + 0.001f + Night.DayAdditive);
            sbatch.Draw(flagTextureNight, new Vector2(Position.X + 30 - offsetX, Position.Y - 28 - offsetY), flagAnimation.SourceRect, selectedTint * Night.NightColor, 0f, Vector2.Zero, 1f, SpriteEffects.None, (Y * 0.01f) + 0.001f + Night.NightAdditive);
        }

        public override int TileNumber()
        {
            return 2;
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
