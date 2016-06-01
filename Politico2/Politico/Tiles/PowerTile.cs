//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics; 

//namespace Politico2.Politico.Tiles
//{
//    /// <summary>
//    /// This class exists so that we can implement the functionality of the IPowerable interface in one spot, 
//    /// rather than duplicate code across the platform. Any tile that can hold power (IE if it's a building with lights)
//    /// Will derive from this class rather than the standard Tile class
//    /// </summary>
//    public abstract class PowerTile : Tile, IPowerable
//    {
//        public PowerTile(Texture2D texture, Vector2 position) : base(texture, position) { }  

//        public bool haspower
//        {
//            get; set; 
//        }

//        public bool baseStation
//        {
//            get; set; 
//        }

//        public void TransmitPower(ref Tile[,] Tiles, ref List<Tile> CheckedTiles)
//        {
//            if (CheckedTiles.Count <= 0) CheckedTiles.Add(this); 

//            if (isOdd)
//            {
//                SetTilePower(ref Tiles, ref CheckedTiles, X + 1, Y - 1);
//                SetTilePower(ref Tiles, ref CheckedTiles, X, Y - 1);
//                SetTilePower(ref Tiles, ref CheckedTiles, X, Y + 1);
//                SetTilePower(ref Tiles, ref CheckedTiles, X + 1, Y + 1); 
//            }
//            else
//            {
//                SetTilePower(ref Tiles, ref CheckedTiles, X, Y - 1);
//                SetTilePower(ref Tiles, ref CheckedTiles, X - 1, Y - 1);
//                SetTilePower(ref Tiles, ref CheckedTiles, X, Y + 1);
//                SetTilePower(ref Tiles, ref CheckedTiles, X - 1, Y + 1); 
//            }
//        }

//        void SetTilePower(ref Tile[,] Tiles, ref List<Tile> CheckedTiles, int x, int y)
//        {
//            if (CheckedTiles.Contains(Tiles[x, y])) return; 

//            if (Tiles[x, y] is PowerTile)
//            {
//                PowerTile t = Tiles[x, y] as PowerTile;

//                if (t.haspower == haspower || t.baseStation)
//                {
//                    CheckedTiles.Add(t);
//                    return; 
//                }

//                t.haspower = haspower;
//                CheckedTiles.Add(t);
//                t.TransmitPower(ref Tiles, ref CheckedTiles);
//                Tiles[x, y] = t; 
//            }
//        }

//        public override void onPlace(Tile[,] Tiles)
//        {
//            base.onPlace(Tiles);
//            if (baseStation) haspower = true; 
//            else haspower = false; 
//        }

//        public abstract void DrawLights(SpriteBatch sbatch, int offsetx, int offsety);

//        public override void DrawDebug(SpriteBatch sbatch, int offsetX, int offsetY)
//        {
//            base.DrawDebug(sbatch, offsetX, offsetY);

//            if (drawDebug)
//                sbatch.DrawString(Assets.debugFont, "Has Power: " + haspower.ToString(), new Vector2(0, 550), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
//        }

//    }
//}
