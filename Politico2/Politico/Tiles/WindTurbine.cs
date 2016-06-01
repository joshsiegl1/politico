using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; 

namespace Politico2.Politico.Tiles
{
    public class WindTurbine : Tile
    {
        Grass grass;

        static Texture2D texture;
        public static Texture2D Texture { get { return texture; } set { texture = value; } }

        static Texture2D texture_top; 
        public static Texture2D Texture_Top { get { return texture_top; } set { texture_top = value; } }

        static Texture2D texture_blade; 
        public static Texture2D Texture_Blade { get { return texture_blade; } set { texture_blade = value; } }

        static Texture2D texture_blade_night;
        public static Texture2D Texture_Blade_Night { get { return texture_blade_night; } set { texture_blade_night = value; } }

        static Texture2D texture_night;
        public static Texture2D Texture_Night { get { return texture_night; } set { texture_night = value; } }

        static Texture2D texture_top_night;
        public static Texture2D Texture_Top_Night { get { return texture_top_night; } set { texture_top_night = value; } }

        public WindTurbine(Vector2 position) : base(texture, position, texture_night)
        {
            grass = new Grass(position);
            haspower = true;
            baseStation = true; 
        }

        float bladerotation = 0f; 
        public override void Update(GameTime gametime)
        {
            grass.Update(gametime);
            bladerotation += 0.01f; 

            base.Update(gametime);
        }

        public override void onPlace(Tile[,] Tiles)
        {
            for (int x = this.X - 5; x < this.X + 5; x++)
            {
                for (int y = this.Y - 5; y < this.Y + 5; y++)
                {
                    if (y >= 0 && y <= Grid.GridHeight - 1 && x >= 0 && x <= Grid.GridWidth - 1)
                        Tiles[x, y].ElectricZone = true;
                }

            }

            base.onPlace(Tiles);
        }

        public override void Draw(SpriteBatch sbatch, int offsetX, int offsetY)
        {
            grass.Draw(sbatch, offsetX, offsetY, (Y * 0.01f) - 0.001f);
            base.Draw(sbatch, offsetX, offsetY);
            float layerDepth = LayerDepth + 0.02f;
            sbatch.Draw(Texture_Top, new Rectangle(bounds.X, bounds.Y - 64, bounds.Width, bounds.Height), null, selectedTint * Night.DayColor, 0.0f, Vector2.Zero, SpriteEffects.None, layerDepth + Night.DayAdditive);
            sbatch.Draw(Texture_Top_Night, new Rectangle(bounds.X, bounds.Y - 64, bounds.Width, bounds.Height), null, selectedTint * Night.NightColor, 0.0f, Vector2.Zero, SpriteEffects.None, layerDepth + Night.NightAdditive);
            sbatch.Draw(texture_blade, new Vector2(position.X + 32 - offsetX, position.Y - 10 - offsetY), null, selectedTint * Night.DayColor, bladerotation, new Vector2(32, 40), 1f, SpriteEffects.None, layerDepth + 0.00001f + Night.DayAdditive);
            sbatch.Draw(texture_blade_night, new Vector2(position.X + 32 - offsetX, position.Y - 10 - offsetY), null, selectedTint * Night.NightColor, bladerotation, new Vector2(32, 40), 1f, SpriteEffects.None, layerDepth + 0.00001f + Night.NightAdditive);
        }

        public override void Draw(SpriteBatch sbatch, int offsetX, int offsetY, float LayerDepth)
        {
            grass.Draw(sbatch, offsetX, offsetY, (Y * 0.01f) - 0.001f);
            base.Draw(sbatch, offsetX, offsetY, LayerDepth);
            float layerDepth = LayerDepth + 0.02f;
            sbatch.Draw(Texture_Top, new Rectangle(bounds.X, bounds.Y - 64, bounds.Width, bounds.Height), null, selectedTint * Night.DayColor, 0.0f, Vector2.Zero, SpriteEffects.None, layerDepth + Night.DayAdditive);
            sbatch.Draw(Texture_Top_Night, new Rectangle(bounds.X, bounds.Y - 64, bounds.Width, bounds.Height), null, selectedTint * Night.NightColor, 0.0f, Vector2.Zero, SpriteEffects.None, layerDepth + Night.NightAdditive);
            sbatch.Draw(texture_blade, new Vector2(position.X + 32 - offsetX, position.Y - 10 - offsetY), null, selectedTint * Night.DayColor, bladerotation, new Vector2(32, 40), 1f, SpriteEffects.None, layerDepth + 0.00001f + Night.DayAdditive);
            sbatch.Draw(texture_blade_night, new Vector2(position.X + 32 - offsetX, position.Y - 10 - offsetY), null, selectedTint * Night.NightColor, bladerotation, new Vector2(32, 40), 1f, SpriteEffects.None, layerDepth + 0.00001f + Night.NightAdditive);
        }

        public override int TileNumber()
        {
            return 5; 
        }

        public override void DrawLights(SpriteBatch sbatch, int offsetx, int offsety)
        {
            return; 
        }
    }
}
