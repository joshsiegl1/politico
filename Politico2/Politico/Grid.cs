using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input; 

using Politico2.Politico.Tiles; 

namespace Politico2.Politico
{
    public class Grid
    {
        public const int GridWidth = 32;
        public const int GridHeight = 70;

        Tile[,] Tiles = new Tile[GridWidth, GridHeight];

        public Tile[,] getTiles() { return Tiles; }

        public readonly Tile[,] OriginalTiles = new Tile[GridWidth, GridHeight]; 

        public Grid()
        {
            string mapdata = "00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001110000000001110000000000000000000000000000000000000000000000000000011111110000011111110000000000000000000000000000000000000000000000000111111111110111111111110000000000000000000000000000000000000000000001111111111111111111111111110000000000000000000000000000000000000000011111111111111111111111111111110000000000000000000000000000000000000111111111111111111111111111111111110000000000000000000000000000000001111111111111111111111111111111111111110000000000000000000000000000011111111111111111111111111111111111111111110000000000000000000000000000111111111111111111111111111111111111111110000000000000000000000000000000111111111111111111111111111111111111100000000000000000000000000000000000111111111111111211111111111111111000000000000000000000000000000000000111111111111111111111111111111111110000000000000000000000000000000001111111111111111111111111111111111111110000000000000000000000000000011111111111111111111111111111111111111111110000000000000000000000000000111111111111111111111111111111111111111110000000000000000000000000000000111111111111111111111111111111111111100000000000000000000000000000000000111111111111111111111111111111111000000000000000000000000000000000000000111111111111111111111111111110000000000000000000000000000000000000000000111111111111111111111111100000000000000000000000000000000000000000000000111111111000111111111000000000000000000000000000000000000000000000000000111110000000111110000000000000000000000000000000000000000000000000000000100000000000100000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000"; 
            int charPosition = 0; 

            for (int x = 0; x < GridWidth; x++)
            {
                for (int y = 0; y < GridHeight; y++)
                {
                    int rowOffset = 0;
                    if (y % 2 == 1)
                        rowOffset = Tile.OddRowXOffset;

                    charPosition++; 

                    Tiles[x, y] = TileFactory.Get(int.Parse(mapdata[charPosition - 1].ToString()), new Vector2((x * Tile.TileStepX) + rowOffset, y * Tile.TileStepY));
                    OriginalTiles[x, y] = TileFactory.Get(int.Parse(mapdata[charPosition - 1].ToString()), new Vector2((x * Tile.TileStepX) + rowOffset, y * Tile.TileStepY));
                }
            }
        }

        public void KillTile(int x, int y)
        {
            Tiles[x, y] = TileFactory.Get(1, Tiles[x, y].Position); 
        }

        public void SetTiles(Tile[,] tiles)
        {
            Array.Copy(tiles, Tiles, GridWidth * GridHeight); 
        }

        MouseState MS, PMS; 
        float clickTimer = 0f;

        List<Vector2> DragTilePositions = new List<Vector2>();
        List<Tile> DragTiles = new List<Tile>();
        public void Update(GameTime gametime, Cursor cursor)
        {

            MS = Mouse.GetState(); 
            clickTimer += (float)gametime.ElapsedGameTime.TotalMilliseconds; 

            for (int x = 0; x < Grid.GridWidth; x++)
            {
                for (int y = 0; y < Grid.GridHeight; y++)
                {
                    Tiles[x, y].Update(gametime);

                    if (Tiles[x, y].Destroy)
                    {
                        onTileDestroyed(this, EventArgs.Empty, Tiles[x, y], Tiles);
                        Tiles[x, y] = new Grass(Tiles[x, y].StartPosition); 
                    }

                    if (Tiles[x, y].IntersectPixels(cursor.ZoomTranslationBounds(), cursor.CursorTextureData))
                    {
                        Tiles[x, y].drawDebug = false;

                        if (MainUserInterface._DisasterSelection == MainUserInterface.DisasterSelection.None)
                        {
                            cursor.SetSelectedTile(TileFactory.Get(cursor.SelectedTile.TileNumber(), Tiles[x, y].Position), cursor.isBulldozer);

                            if (MS.LeftButton == ButtonState.Pressed)
                            {
                                clickTimer = 0f;
                                if (!DragTilePositions.Contains(Tiles[x, y].Position) && (Tiles[x, y].CanBeReplaced() || cursor.isBulldozer))
                                {
                                    if (!(Tiles[x, y] is Capital))
                                    {
                                        DragTiles.Add(TileFactory.Get(cursor.SelectedTile.TileNumber(), Tiles[x, y].Position));
                                        DragTilePositions.Add(Tiles[x, y].Position);
                                        cursor.SetDragTiles(DragTiles);
                                    }
                                }
                            }

                            if (PMS.LeftButton == ButtonState.Released && DragTiles.Count > 0 && clickTimer >= 10f)//60f)
                            {
                                if (cursor.isBulldozer)
                                {
                                    foreach (Tile t in DragTiles)
                                    {
                                        if (onTileDestroyed != null && Tiles[t.X, t.Y].CanBeDestroyed())
                                            onTileDestroyed(this, EventArgs.Empty, Tiles[t.X, t.Y], Tiles);
                                    }
                                }

                                foreach (Tile t in DragTiles)
                                {
                                    Tiles[t.X, t.Y] = t;
                                }


                                foreach (Tile t in Tiles)
                                {
                                    t.Reset();
                                }

                                foreach (Tile t in Tiles)
                                {
                                    t.onPlace(Tiles);
                                }

                                List<Tile> checkedList = new List<Tile>();
                                foreach (Tile t in Tiles)
                                {
                                    if (t.baseStation)
                                    {
                                        t.TransmitPower(ref Tiles, ref checkedList);
                                    }
                                }


                                foreach (Tile t in DragTiles)
                                {
                                    if (onTilePlaced != null)
                                        onTilePlaced(this, EventArgs.Empty, t, Tiles);
                                }

                                clickTimer = 0f;
                                DragTiles.Clear();
                                DragTilePositions.Clear();
                                cursor.SetDragTiles(DragTiles);

                            }
                        }
                        else
                        {
                            if (MS.LeftButton == ButtonState.Pressed && PMS.LeftButton == ButtonState.Released)
                            {
                                if (MainUserInterface._DisasterSelection == MainUserInterface.DisasterSelection.Fire)
                                {
                                        Tiles[x, y].onFire = true;
                                        if (onFirePlaced != null)
                                            onFirePlaced(this, EventArgs.Empty, Tiles[x, y]);
                                }
                                else if (MainUserInterface._DisasterSelection == MainUserInterface.DisasterSelection.Riot)
                                {
                                    if (Tiles[x, y] is Road)
                                    {
                                        Road r = Tiles[x, y] as Road;
                                        r.isRiot = true;
                                        Tiles[x, y] = r;
                                        if (onRiotPlaced != null)
                                            onRiotPlaced(this, EventArgs.Empty, Tiles[x, y]); 
                                    }
                                }
                            }
                        }
                    }
                    else Tiles[x, y].drawDebug = false; 
                }
            }

            PMS = MS; 
        }

        public delegate void DisasterHandler(object sender, EventArgs e, Tile t); 
        public event DisasterHandler onFirePlaced;
        public event DisasterHandler onRiotPlaced;

        public delegate void TilePlacedHandler(object sender, EventArgs e, Tile t, Tile[,] grid); 
        public event TilePlacedHandler onTilePlaced;

        public delegate void TileDestroyedHandler(object sender, EventArgs e, Tile t, Tile[,] grid);
        public event TileDestroyedHandler onTileDestroyed; 

        public void Draw(SpriteBatch sbatch)
        {
            sbatch.Draw(Assets.BaseNight, Vector2.Zero + Camera.Pos, null, Color.White * Night.NightColor, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.00003f + Night.NightAdditive);
            sbatch.Draw(Assets.Base, Vector2.Zero + Camera.Pos, null, Color.White * Night.DayColor, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.00003f + Night.DayAdditive);

            Vector2 squareOffset = Camera.Pos; 
            int offsetX = (int)-squareOffset.X;
            int offsetY = (int)-squareOffset.Y;

            for (int y = 0; y < GridHeight; y++)
            {
                for (int x = 0; x < GridWidth; x++)
                {
                        Tiles[x, y].DrawZone(MainUserInterface._GridSelection);
                        Tiles[x, y].Draw(sbatch, offsetX, offsetY);
                        Tiles[x, y].DrawLights(sbatch, offsetX, offsetY);
                }
            }

            foreach (Tile t in Tiles)
                t.DrawDebug(sbatch, offsetX, offsetY); 

        } 
    }
}
