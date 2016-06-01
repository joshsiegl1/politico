using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Politico2.Politico.Effects;
using Politico2.Politico.TrafficSystem;

namespace Politico2.Politico.Tiles
{
    public class Road : Tile
    {
        static Texture2D texture;
        public static Texture2D Texture { get { return texture; } set { texture = value; } } //Road Up Right

        static Texture2D texture_night;
        public static Texture2D Texture_Night { get { return texture_night; } set { texture_night = value; } }

        public static Texture2D TrafficLight;

        public static Texture2D TrafficLight_Night; 

        public static Dictionary<string, Texture2D> Pieces = new Dictionary<string, Texture2D>();
        public static Dictionary<string, Texture2D> PiecesNight = new Dictionary<string, Texture2D>(); 

        private PieceType type = PieceType.straight; 

        public PieceType pieceType { get { return type; } }

        public Tile topright, topleft, bottomleft, bottomright;

        //only applicable to cross and tri sections
        public CanGo TrafficCanGo = CanGo.upright_downleft;
        private CanGo LastCanGo;

        public bool isRiot;

        public enum CanGo
        {
            upright_downleft, 
            downright_upleft
        }

        private float CanGoTimer = 0f; 

        public Road(Vector2 position) : base(texture, position, texture_night)
        {
 
        }

        public override void Update(GameTime gametime)
        {
            if (type == PieceType.cross || type == PieceType.tri)
            {
                CanGoTimer += (float)gametime.ElapsedGameTime.TotalMilliseconds;

                if (CanGoTimer >= 10000)
                {
                    LastCanGo = TrafficCanGo; 

                    if (LastCanGo == CanGo.upright_downleft)
                        TrafficCanGo = CanGo.downright_upleft;
                    else 
                        TrafficCanGo = CanGo.upright_downleft;

                    CanGoTimer = 0f; 
                }
            }

            base.Update(gametime);
        }

        private void SetTextureRoad(string piece)
        {
            SetTextureNight(PiecesNight[piece]);
            SetTexture(Pieces[piece]);
        }

        public override void onPlace(Tile[,] Tiles)
        {
            if (isOdd)
            {
                topright = Tiles[X + 1, Y - 1];
                topleft = Tiles[X, Y - 1];
                bottomleft = Tiles[X, Y + 1];
                bottomright = Tiles[X + 1, Y + 1];

                if (Tiles[X + 1, Y - 1] is Road && Tiles[X, Y + 1] is Road && Tiles[X + 1, Y + 1] is Road && Tiles[X, Y - 1] is Road)
                {
                    SetTextureRoad("cross");
                    type = PieceType.cross;
                }

                else if (Tiles[X + 1, Y - 1] is Road && Tiles[X, Y + 1] is Road && Tiles[X, Y - 1] is Road)
                {
                    SetTextureRoad("tri_bottomleft-topleft-topright");
                    type = PieceType.tri; 
                }

                else if (Tiles[X + 1, Y + 1] is Road && Tiles[X, Y + 1] is Road && Tiles[X, Y - 1] is Road)
                {
                    SetTextureRoad("tri_bottomright-bottomleft-topleft");
                    type = PieceType.tri;
                }

                else if (Tiles[X + 1, Y + 1] is Road && Tiles[X + 1, Y - 1] is Road && Tiles[X, Y - 1] is Road)
                {
                    SetTextureRoad("tri_topleft-topright-bottomright");
                    type = PieceType.tri;
                }

                else if (Tiles[X + 1, Y + 1] is Road && Tiles[X + 1, Y - 1] is Road && Tiles[X, Y + 1] is Road)
                {
                    SetTextureRoad("tri_topright-bottomright-bottomleft");
                    type = PieceType.tri;
                }

                else if (Tiles[X, Y + 1] is Road && Tiles[X, Y - 1] is Road)
                {
                    SetTextureRoad("angle_topleft-bottomleft");
                    type = PieceType.angle;
                }

                else if (Tiles[X, Y + 1] is Road && Tiles[X + 1, Y + 1] is Road)
                {
                    SetTextureRoad("angle_bottomright-bottomleft");
                    type = PieceType.angle;
                }

                else if (Tiles[X + 1, Y - 1] is Road && Tiles[X + 1, Y + 1] is Road)
                {
                    SetTextureRoad("angle_topright-bottomright");
                    type = PieceType.angle;
                }

                else if (Tiles[X + 1, Y - 1] is Road && Tiles[X, Y - 1] is Road)
                {
                    SetTextureRoad("angle_topright-topleft");
                    type = PieceType.angle;
                }

                else if (Tiles[X, Y - 1] is Road)
                {
                    SetTextureRoad("up_left");
                    type = PieceType.straight;
                }

                else if (Tiles[X + 1, Y - 1] is Road)
                {
                    SetTextureRoad("up_right");
                    type = PieceType.straight;
                } 
            }
            else
            {
                topright = Tiles[X, Y - 1];
                topleft = Tiles[X - 1, Y - 1];
                bottomright = Tiles[X, Y + 1];
                bottomleft = Tiles[X - 1, Y + 1];

                if (Tiles[X, Y - 1] is Road && Tiles[X - 1, Y + 1] is Road && Tiles[X - 1, Y - 1] is Road && Tiles[X, Y + 1] is Road)
                {
                    SetTextureRoad("cross");
                    type = PieceType.cross; 
                }

                else if (Tiles[X, Y - 1] is Road && Tiles[X - 1, Y + 1] is Road && Tiles[X - 1, Y - 1] is Road)
                {
                    SetTextureRoad("tri_bottomleft-topleft-topright");
                    type = PieceType.tri; 
                }

                else if (Tiles[X, Y + 1] is Road && Tiles[X - 1, Y + 1] is Road && Tiles[X - 1, Y - 1] is Road)
                {
                    SetTextureRoad("tri_bottomright-bottomleft-topleft");
                    type = PieceType.tri;
                }

                else if (Tiles[X, Y + 1] is Road && Tiles[X, Y - 1] is Road && Tiles[X - 1, Y - 1] is Road)
                {
                    SetTextureRoad("tri_topleft-topright-bottomright");
                    type = PieceType.tri;
                }

                else if (Tiles[X, Y + 1] is Road && Tiles[X, Y - 1] is Road && Tiles[X - 1, Y + 1] is Road)
                {
                    SetTextureRoad("tri_topright-bottomright-bottomleft");
                    type = PieceType.tri;
                }

                else if (Tiles[X - 1, Y + 1] is Road && Tiles[X - 1, Y - 1] is Road)
                {
                    SetTextureRoad("angle_topleft-bottomleft");
                    type = PieceType.angle;
                }

                else if (Tiles[X - 1, Y + 1] is Road && Tiles[X, Y + 1] is Road)
                {
                    SetTextureRoad("angle_bottomright-bottomleft");
                    type = PieceType.angle;
                }

                else if (Tiles[X, Y - 1] is Road && Tiles[X, Y + 1] is Road)
                {
                    SetTextureRoad("angle_topright-bottomright");
                    type = PieceType.angle;
                }

                else if (Tiles[X, Y - 1] is Road && Tiles[X - 1, Y - 1] is Road)
                {
                    SetTextureRoad("angle_topright-topleft");
                    type = PieceType.angle;
                }

                else if (Tiles[X - 1, Y - 1] is Road)
                {
                    SetTextureRoad("up_left");
                    type = PieceType.straight;
                }

                else if (Tiles[X, Y - 1] is Road)
                {
                    SetTextureRoad("up_right");
                    type = PieceType.straight;
                }
            }

            base.onPlace(Tiles);
        }

        public override void Draw(SpriteBatch sbatch, int offsetX, int offsetY)
        {
            float LayerDepth = 0.001f; 
            Rectangle imageRect = new Rectangle((int)position.X - offsetX, (int)position.Y - offsetY, TileWidth, TileHeight);
            bounds = imageRect;
            sbatch.Draw(ParentTexture, imageRect, null, selectedTint * Night.DayColor, 0.0f, Vector2.Zero, SpriteEffects.None, LayerDepth + Night.DayAdditive);
            sbatch.Draw(ParentTextureNight, imageRect, null, selectedTint * Night.NightColor, 0.0f, Vector2.Zero, SpriteEffects.None, LayerDepth + Night.NightAdditive);

            if (type == PieceType.cross || type == PieceType.tri)
            {
                sbatch.Draw(TrafficLight, new Vector2((int)position.X - offsetX, (int)position.Y - offsetY), null, selectedTint * Night.DayColor, 0f, Vector2.Zero, 1f, SpriteEffects.None, LayerDepth + 0.2f + Night.DayAdditive); //LayerDepth + 0.0011f);
                sbatch.Draw(TrafficLight_Night, new Vector2((int)position.X - offsetX, (int)position.Y - offsetY), null, selectedTint * Night.NightColor, 0f, Vector2.Zero, 1f, SpriteEffects.None, LayerDepth + 0.2f + Night.NightAdditive);
            }
        }

        public override void DrawDebug(SpriteBatch sbatch, int offsetX, int offsetY)
        {
            if (Global.DrawDebug)
            {
                sbatch.DrawString(Assets.debugFont, "Can Go: " + TrafficCanGo.ToString(), new Vector2(0, 550), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            }
            base.DrawDebug(sbatch, offsetX, offsetY);
        }

        public enum PieceType
        {
            cross, 
            tri, 
            angle, 
            straight
        }

        public override void TransmitPower(ref Tile[,] Tiles, ref List<Tile> CheckedTiles)
        {
            return; 
        }

        public override int TileNumber()
        {
            return 3;
        }

        public override void DrawLights(SpriteBatch sbatch, int offsetx, int offsety)
        {
            return; 
        }
    }
}
