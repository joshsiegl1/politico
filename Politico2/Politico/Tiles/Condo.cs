using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Politico2.Politico.Tiles
{
    public class Condo : Tile
    {
        Grass grass;

        static Texture2D texture;
        public static Texture2D Texture { get { return texture; } set { texture = value; } }

        static Texture2D texture_top;
        public static Texture2D Texture_Top { get { return texture_top; } set { texture_top = value; } }

        static Texture2D texture_lights_top; 
        public static Texture2D Texture_Lights_Top { get { return texture_lights_top; } set { texture_lights_top = value; } }

        static Texture2D texture_lights_bottom; 
        public static Texture2D Texture_Lights_Bottom { get { return texture_lights_bottom; } set { texture_lights_bottom = value; } }

        static Texture2D texture_night;
        public static Texture2D Texture_Night { get { return texture_night; } set { texture_night = value; } }

        static Texture2D texture_top_night;
        public static Texture2D Texture_Top_Night { get { return texture_top_night; } set { texture_top_night = value; } }

        public Condo(Vector2 position) : base(texture, position, texture_night)
        {
            grass = new Grass(position);
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
            float layerDepth = (Y * 0.01f) + 0.02f;
            sbatch.Draw(Texture_Top, new Rectangle(bounds.X, bounds.Y - 64, bounds.Width, bounds.Height), null, selectedTint * Night.DayColor, 0.0f, Vector2.Zero, SpriteEffects.None, layerDepth + Night.DayAdditive);
            sbatch.Draw(Texture_Top_Night, new Rectangle(bounds.X, bounds.Y - 64, bounds.Width, bounds.Height), null, selectedTint * Night.NightColor, 0.0f, Vector2.Zero, SpriteEffects.None, layerDepth + Night.NightAdditive);
        }

        public override void Draw(SpriteBatch sbatch, int offsetX, int offsetY, float LayerDepth)
        {
            grass.Draw(sbatch, offsetX, offsetY, (Y * 0.01f) - 0.001f);
            base.Draw(sbatch, offsetX, offsetY, LayerDepth);
            float layerDepth = LayerDepth + 0.02f;
            sbatch.Draw(Texture_Top, new Rectangle(bounds.X, bounds.Y - 64, bounds.Width, bounds.Height), null, selectedTint * Night.DayColor, 0.0f, Vector2.Zero, SpriteEffects.None, layerDepth + Night.DayAdditive);
            sbatch.Draw(Texture_Top_Night, new Rectangle(bounds.X, bounds.Y - 64, bounds.Width, bounds.Height), null, selectedTint * Night.NightColor, 0.0f, Vector2.Zero, SpriteEffects.None, layerDepth + Night.NightAdditive);
        }

        public override int TileNumber()
        {
            return 10;
        }

        public override void DrawLights(SpriteBatch sbatch, int offsetx, int offsety)
        {
            if (haspower)
            {
                float layerDepth = Y * 0.01f;
                sbatch.Draw(texture_lights_bottom, new Vector2(position.X - offsetx, position.Y - offsety), null, selectedTint, 0f, Vector2.Zero, 1f, SpriteEffects.None, layerDepth + 0.00001f);
                layerDepth = (Y * 0.01f) + 0.02f;
                sbatch.Draw(texture_lights_top, new Vector2(position.X - offsetx, position.Y - offsety - 64), null, selectedTint, 0f, Vector2.Zero, 1f, SpriteEffects.None, layerDepth + 0.00001f);
            }
        }
    }
}
