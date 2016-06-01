using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Politico2.Politico.Tiles; 

namespace Politico2.Politico.Effects
{
    internal class Fract : Particle
    {

        private Rectangle textureBounds; 

        private float DropLocationY;

        private int bounce_count = 1;

        private bool shouldFallBehind = false; 

        public Fract(Texture2D texture, Rectangle textureBounds, Vector2 position) : base()
        {
            this.texture = texture;
            this.textureBounds = textureBounds;
            this.position = position; 

            velocity = new Vector2(random.Next(-500, 500), random.Next(-1000, -100)) * 0.015f;
            color = Color.White; 

            DropLocationY = random.Next(bounds.Y, 1500);

            shouldFallBehind = Convert.ToBoolean(random.Next(2)); 
        }

        bool hasReachedTop = false; 
        float fallingLayerDepth = 0.00002f; 
        float LayerDepth = 1f; 
        public void UpdatePhysics(GameTime gametime, Tile[,] Tiles)
        {
            if (position.Y >= 1080 || position.X <= -5 || position.X >= 1920) { timer = 5000f; }

            if (!hasReachedTop)
            {
                if (position.Y < 200)
                {
                    hasReachedTop = true; 
                }
            }

            //if (X >= 0 && X < Grid.GridWidth - 1 && Y >= 0 && Y < Grid.GridHeight - 1)
            {
                if (position.Y >= DropLocationY - 5 && timer < 5000f)
                {
                    int Y = (int)position.Y / Tile.TileStepY;

                    int rowOffset = 0;
                    if (Y % 2 == 1)
                    {
                        rowOffset = Tile.OddRowXOffset;
                    }

                    int xPos = (int)position.X - rowOffset;

                    int X = xPos / Tile.TileStepX;

                    if (X >= 0 && X <= Grid.GridWidth - 1 && Y >= 0 && Y <= Grid.GridHeight)
                    {
                        if (Tiles[X, Y] is Empty)
                        {
                            DropLocationY = 2000;
                        }
                        else if (bounce_count <= 5)
                        {
                            velocity = new Vector2(velocity.X, -2f / bounce_count);
                            bounce_count++;
                        }
                        else
                        {
                            velocity = Vector2.Zero;
                            timer = 5000f;
                        }
                    }
                    else DropLocationY = 2000;
                }
            }

            timer += (float)gametime.ElapsedGameTime.TotalMilliseconds; 
            if (timer >= 5000)
            {
                color.A -= 5;
                color.R -= 5;
                color.G -= 5;
                color.B -= 5; 

                if (color.A <= 5) remove = true; 
            }
        }

        public override void Update(GameTime gametime)
        {
            if (velocity != Vector2.Zero)
                velocity += new Vector2(0, 0.15f); 
            base.Update(gametime);
        }

        public override void Draw(SpriteBatch sbatch)
        {
            float layerdepth = LayerDepth;
            if (shouldFallBehind && velocity.Y > 0 && hasReachedTop) layerdepth = fallingLayerDepth; 
            sbatch.Draw(texture, position, textureBounds, color, 0f, Vector2.Zero, 1f, SpriteEffects.None, layerdepth); 
        }
    }
}
