using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Politico2.Politico.Tiles;
using Politico2.Politico; 

namespace Politico2
{
    public class Cursor
    {
        private Tile selectedTile;
        public Tile  SelectedTile { get { return selectedTile; } }

        private List<Tile> DragTiles = new List<Tile>(); 

        private MouseState MS;
        private KeyboardState KBS;
        public static Texture2D Texture;

        public bool isBulldozer; 

        //This is used for pixel perfect collision detection
        private Color[] cursorTextureData;
        public Color[] CursorTextureData { get { return cursorTextureData; } }

        MenuSystem.ScreenManager ScreenManager; 

        public Cursor(MenuSystem.ScreenManager ScreenManager)
        {
            this.ScreenManager = ScreenManager;

            //Create and set the color data of the tile used for collision detection
            cursorTextureData = new Color[Texture.Width * Texture.Height];
            Texture.GetData(cursorTextureData);

            selectedTile = new Road(Vector2.Zero); 
        }

        Matrix Scale
        {
            get { return Matrix.CreateScale(new Vector3(Camera.Zoom, Camera.Zoom, 1)); }
        }

        Matrix InputScale
        {
            get { return Matrix.Invert(Scale); }
        }

        public Vector2 MouseCoordinates(MenuSystem.ScreenManager ScreenManager)
        {
            MS = Mouse.GetState();
            Vector2 RawMouseCoordinates = new Vector2(MS.X, MS.Y);

            Vector2 m1 = Vector2.Transform(RawMouseCoordinates - ScreenManager.InputTranslate, ScreenManager.InputScale);

            float MouseWorldX = (m1.X - (ScreenManager.Viewport.Width * 0.5f) + ((ScreenManager.Viewport.Width * 0.5f) + Camera.Pos.X) * (float)Math.Pow(Camera.Zoom, 1)) /
                  (float)Math.Pow(Camera.Zoom, 1);

            float MouseWorldY = (m1.Y - (ScreenManager.Viewport.Height * 0.5f) + ((ScreenManager.Viewport.Height * 0.5f) + Camera.Pos.Y) * (float)Math.Pow(Camera.Zoom, 1)) /
                    (float)Math.Pow(Camera.Zoom, 1);


            return new Vector2(MouseWorldX, MouseWorldY) - Camera.Pos; 
        }

        MouseState old_mouse; 
        public void Update(GameTime gametime)
        {
            MS = Mouse.GetState();
            KBS = Keyboard.GetState();

            if (Mouse.GetState().ScrollWheelValue != old_mouse.ScrollWheelValue)
            {
                if (Camera.Zoom > 1)
                {
                    Camera.Zoom += (Mouse.GetState().ScrollWheelValue - old_mouse.ScrollWheelValue) / 120.0f / 10.0f;
                }
                else
                {
                    Camera.Zoom *= 1 + (Mouse.GetState().ScrollWheelValue - old_mouse.ScrollWheelValue) / 120.0f / 20.0f;
                }
            }

            if (KBS.IsKeyDown(Keys.Left))
                Camera.Pos -= new Vector2(1f, 0);
            if (KBS.IsKeyDown(Keys.Right))
                Camera.Pos += new Vector2(1f, 0);
            if (KBS.IsKeyDown(Keys.Up))
                Camera.Pos -= new Vector2(0, 1f);
            if (KBS.IsKeyDown(Keys.Down))
                Camera.Pos += new Vector2(0, 1f);

            old_mouse = Mouse.GetState(); 

        }

        public void SetDragTiles(List<Tile> Tiles)
        {
            DragTiles = Tiles;
        }

        public void SetSelectedTile(Tile t)
        {
            selectedTile = t;
            isBulldozer = false;  
        }

        public void SetSelectedTile(Tile t, bool isBulldozer)
        {
            selectedTile = t;
            this.isBulldozer = isBulldozer; 
        }

        public bool isMouseClicked()
        {
            if (MS.LeftButton == ButtonState.Pressed)
                return true;
            else return false; 
        }

        public Rectangle ZoomTranslationBounds()
        {
            return new Rectangle((int)MouseCoordinates(ScreenManager).X, (int)MouseCoordinates(ScreenManager).Y, Texture.Width, Texture.Height); 
        }

        public Rectangle Bounds()
        {
            MS = Mouse.GetState();
            Vector2 RawMouseCoordinates = new Vector2(MS.X, MS.Y);

            Vector2 m1 = Vector2.Transform(RawMouseCoordinates - ScreenManager.InputTranslate, ScreenManager.InputScale);

            return new Rectangle((int)m1.X, (int)m1.Y, Texture.Width, Texture.Height); 
        }

        public void DrawTransparentTile(SpriteBatch sbatch, int offsetX, int offsetY)
        {
            //if (selectedTile != null && !isBulldozer)
            //{
            //    selectedTile.setSelectedTint(new Color(155,155,155,155)); 
            //    selectedTile.Draw(sbatch, offsetX, offsetY, selectedTile.LayerDepth + 0.0001f);
            //}

            for (int i = 0; i < DragTiles.Count; i++)
            {
                DragTiles[i].setSelectedTint(new Color(155, 155, 155, 155)); 
                DragTiles[i].Draw(sbatch, offsetX, offsetY, DragTiles[i].LayerDepth + 0.0001f);
            }
        }

        public void DrawDebug(SpriteBatch sbatch)
        {
            //sbatch.DrawString(Assets.debugFont, MS.ScrollWheelValue.ToString(), new Vector2(0, 650), Color.Black); 
        }

        public void Draw(SpriteBatch sbatch)
        {
            //sbatch.Draw(Texture, ZoomTranslationBounds(), Color.White); 
        }
    }
}
