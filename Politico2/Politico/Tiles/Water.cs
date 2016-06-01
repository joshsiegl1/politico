using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; 

namespace Politico2.Politico.Tiles
{
    public class Water : Tile
    {
        static Texture2D texture;
        public static Texture2D Texture { get { return texture; } set { texture = value; } }

        static Texture2D texture_night;
        public static Texture2D Texture_Night { get { return texture_night; } set { texture_night = value; } }

        public static Dictionary<string, Texture2D> Pieces = new Dictionary<string, Texture2D>();
        public static Dictionary<string, Texture2D> PiecesNight = new Dictionary<string, Texture2D>(); 

        public Water(Vector2 position) : base(texture, position, texture_night)
        {

        }

        private void SetTextureWater(string piece)
        {
            SetTextureNight(PiecesNight[piece]);
            SetTexture(Pieces[piece]);
        }


        public Tile topright, topleft, bottomleft, bottomright;
        public override void onPlace(Tile[,] Tiles)
        {
            if (isOdd)
            {
                topright = Tiles[X + 1, Y - 1];
                topleft = Tiles[X, Y - 1];
                bottomleft = Tiles[X, Y + 1];
                bottomright = Tiles[X + 1, Y + 1];

                if (Tiles[X + 1, Y - 1] is Water && Tiles[X, Y + 1] is Water && Tiles[X + 1, Y + 1] is Water && Tiles[X, Y - 1] is Water)
                {
                    SetTextureWater("cross");
                }

                else if (Tiles[X + 1, Y - 1] is Water && Tiles[X, Y + 1] is Water && Tiles[X, Y - 1] is Water)
                {
                    SetTextureWater("tri_bottomleft-topleft-topright");
                }

                else if (Tiles[X + 1, Y + 1] is Water && Tiles[X, Y + 1] is Water && Tiles[X, Y - 1] is Water)
                {
                    SetTextureWater("tri_bottomright-bottomleft-topleft");
                }

                else if (Tiles[X + 1, Y + 1] is Water && Tiles[X + 1, Y - 1] is Water && Tiles[X, Y - 1] is Water)
                {
                    SetTextureWater("tri_topleft-topright-bottomright");
                }

                else if (Tiles[X + 1, Y + 1] is Water && Tiles[X + 1, Y - 1] is Water && Tiles[X, Y + 1] is Water)
                {
                    SetTextureWater("tri_topright-bottomright-bottomleft");
                }

                else if (Tiles[X, Y + 1] is Water && Tiles[X, Y - 1] is Water)
                {
                    SetTextureWater("angle_topleft-bottomleft");
                }

                else if (Tiles[X, Y + 1] is Water && Tiles[X + 1, Y + 1] is Water)
                {
                    SetTextureWater("angle_bottomright-bottomleft");
                }

                else if (Tiles[X + 1, Y - 1] is Water && Tiles[X + 1, Y + 1] is Water)
                {
                    SetTextureWater("angle_topright-bottomright");
                }

                else if (Tiles[X + 1, Y - 1] is Water && Tiles[X, Y - 1] is Water)
                {
                    SetTextureWater("angle_topright-topleft");
                }

                else if (Tiles[X + 1, Y - 1] is Water && Tiles[X, Y + 1] is Water)
                {
                    SetTextureWater("straight_bottomleft-topright");
                }

                else if (Tiles[X, Y - 1] is Water && Tiles[X + 1, Y + 1] is Water)
                {
                    SetTextureWater("straight_topleft-bottomright");
                }

                else if (Tiles[X, Y - 1] is Water)
                {
                    SetTextureWater("single_topleft"); 
                }

                else if (Tiles[X + 1, Y - 1] is Water)
                {
                    SetTextureWater("single_topright");
                }

                else if (Tiles[X, Y + 1] is Water)
                {
                    SetTextureWater("single_bottomleft");
                }

                else if (Tiles[X + 1, Y + 1] is Water)
                {
                    SetTextureWater("single_bottomright");
                }
            }
            else
            {
                topright = Tiles[X, Y - 1];
                topleft = Tiles[X - 1, Y - 1];
                bottomright = Tiles[X, Y + 1];
                bottomleft = Tiles[X - 1, Y + 1];

                if (Tiles[X, Y - 1] is Water && Tiles[X - 1, Y + 1] is Water && Tiles[X - 1, Y - 1] is Water && Tiles[X, Y + 1] is Water)
                {
                    SetTextureWater("cross");
                }

                else if (Tiles[X, Y - 1] is Water && Tiles[X - 1, Y + 1] is Water && Tiles[X - 1, Y - 1] is Water)
                {
                    SetTextureWater("tri_bottomleft-topleft-topright");
                }

                else if (Tiles[X, Y + 1] is Water && Tiles[X - 1, Y + 1] is Water && Tiles[X - 1, Y - 1] is Water)
                {
                    SetTextureWater("tri_bottomright-bottomleft-topleft");
                }

                else if (Tiles[X, Y + 1] is Water && Tiles[X, Y - 1] is Water && Tiles[X - 1, Y - 1] is Water)
                {
                    SetTextureWater("tri_topleft-topright-bottomright");
                }

                else if (Tiles[X, Y + 1] is Water && Tiles[X, Y - 1] is Water && Tiles[X - 1, Y + 1] is Water)
                {
                    SetTextureWater("tri_topright-bottomright-bottomleft");
                }

                else if (Tiles[X - 1, Y + 1] is Water && Tiles[X - 1, Y - 1] is Water)
                {
                    SetTextureWater("angle_topleft-bottomleft");
                }

                else if (Tiles[X - 1, Y + 1] is Water && Tiles[X, Y + 1] is Water)
                {
                    SetTextureWater("angle_bottomright-bottomleft");
                }

                else if (Tiles[X, Y - 1] is Water && Tiles[X, Y + 1] is Water)
                {
                    SetTextureWater("angle_topright-bottomright");
                }

                else if (Tiles[X, Y - 1] is Water && Tiles[X - 1, Y - 1] is Water)
                {
                    SetTextureWater("angle_topright-topleft");
                }

                else if (Tiles[X, Y - 1] is Water && Tiles[X - 1, Y + 1] is Water)
                {
                    SetTextureWater("straight_bottomleft-topright");
                }

                else if (Tiles[X, Y + 1] is Water && Tiles[X - 1, Y - 1] is Water)
                {
                    SetTextureWater("straight_topleft-bottomright");
                }

                else if (Tiles[X - 1, Y - 1] is Water)
                {
                    SetTextureWater("single_topleft");
                }

                else if (Tiles[X, Y - 1] is Water)
                {
                    SetTextureWater("single_topright");
                }

                else if (Tiles[X - 1, Y + 1] is Water)
                {
                    SetTextureWater("single_bottomleft");
                }

                else if (Tiles[X, Y + 1] is Water)
                {
                    SetTextureWater("single_bottomright");
                }
            }

            base.onPlace(Tiles);
        }

        public override void Draw(SpriteBatch sbatch, int offsetX, int offsetY)
        {
            Rectangle imageRect = new Rectangle((int)position.X - offsetX, (int)position.Y - offsetY, TileWidth, TileHeight);
            bounds = imageRect;
            sbatch.Draw(ParentTexture, imageRect, null, selectedTint * Night.DayColor, 0.0f, Vector2.Zero, SpriteEffects.None, LayerDepth + Night.DayAdditive);
            sbatch.Draw(ParentTextureNight, imageRect, null, selectedTint * Night.NightColor, 0.0f, Vector2.Zero, SpriteEffects.None, LayerDepth + Night.NightAdditive);
        }

        public override void TransmitPower(ref Tile[,] Tiles, ref List<Tile> CheckedTiles)
        {
            return; 
        }

        public override void DrawLights(SpriteBatch sbatch, int offsetx, int offsety)
        {
            return;
        }

        public override int TileNumber()
        {
            return 13; 
        }
    }
}
