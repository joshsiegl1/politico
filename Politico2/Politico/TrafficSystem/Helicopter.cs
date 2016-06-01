using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; 

namespace Politico2.Politico.TrafficSystem
{
    internal abstract class Helicopter
    {
        protected Animation animation;

        protected Vector2 location;
        public Vector2 Position { get { return location; } }

        private Texture2D DownLeft, DownRight, UpLeft, UpRight;

        private Tiles.Tile tile;

        protected State state = State.Landed;

        private bool facingRight = false;

        public static Texture2D Shadow; 

        public Helicopter(Vector2 location, Texture2D DownLeft, Texture2D DownRight, Texture2D UpLeft, Texture2D UpRight, 
            Tiles.Tile tile)
        {
            this.location = location;
            this.DownLeft = DownLeft;
            this.DownRight = DownRight;
            this.UpLeft = UpLeft;
            this.UpRight = UpRight;
            this.tile = tile;

            animation = new Animation(Vector2.Zero, 64, 64, 4, true, 50); 
        }

        protected float stateSwitchTimer = 0f; 
        public virtual void Update(GameTime gametime)
        {
            stateSwitchTimer += (float)gametime.ElapsedGameTime.TotalMilliseconds; 

            switch(state)
            {
                case State.Landed:
                    if (stateSwitchTimer >= 10000f)
                    {
                        Rise(); 
                        stateSwitchTimer = 0f; 
                    }
                    break;
                case State.Rising:
                    if (location.Y > YRiseToPoint)
                    {
                        location.Y -= 1f;
                        animation.UpdateSpriteSheet(gametime);
                    }
                    else
                    {
                        Roam();
                        stateSwitchTimer = 0f;
                    }
                    break;
                case State.Roaming:
                    MoveToPoint(RoamToPoint); 
                    if (location == RoamToPoint)
                    {
                        if (stateSwitchTimer >= 10000)
                        {
                            Search(new Vector2(tile.Position.X, tile.Position.Y - 192)); 
                            stateSwitchTimer = 0f; 
                        }
                        else
                        {
                            Roam(); 
                        }
                    }
                    animation.UpdateSpriteSheet(gametime); 
                    break;
                case State.Searching:
                    MoveToPoint(searchPosition);
                    if (location == searchPosition)
                    {
                        Hover();
                        stateSwitchTimer = 0f; 
                    }
                    animation.UpdateSpriteSheet(gametime);
                    break;
                case State.Hovering:
                    if (stateSwitchTimer >= 3000f)
                    {
                        Land(tile.Position);
                        stateSwitchTimer = 0f; 
                    }
                    animation.UpdateSpriteSheet(gametime);
                    break;
                case State.Landing:
                    MoveToPoint(landPosition); 
                    if (location == landPosition)
                    {
                        state = State.Landed;
                        stateSwitchTimer = 0f; 
                    }
                    animation.UpdateSpriteSheet(gametime);
                    break;
            }

        }

        protected Vector2 landPosition; 
        protected void Land(Vector2 position)
        {
            state = State.Landing;
            landPosition = position; 
        }

        protected void Hover()
        {
            state = State.Hovering; 
        }

        protected Vector2 searchPosition; 
        protected void Search(Vector2 position)
        {
            searchPosition = position;
            state = State.Searching; 
        }

        static Random random = new Random();
        protected Vector2 RoamToPoint = Vector2.Zero; 
        protected void Roam()
        {
            state = State.Roaming;
            RoamToPoint = new Vector2(random.Next(445, 1445), random.Next(150, 650));
        }

        protected float YRiseToPoint; 
        protected void Rise()
        {
            state = State.Rising;
            YRiseToPoint = tile.Position.Y - 192; 
        }

        protected void MoveToPoint(Vector2 position)
        {
            if (location.X < position.X)
            {
                location.X++;
                facingRight = true;
            }
            if (location.X > position.X)
            {
                location.X--;
                facingRight = false;
            }
            if (location.Y < position.Y)
                location.Y++;
            if (location.Y > position.Y)
                location.Y--;
        }

        public virtual void Draw(SpriteBatch sbatch, Vector2 CameraOffset)
        {
            float layerdepth = tile.LayerDepth + 0.00001f;
            if (state != State.Landed && state != State.Rising && state != State.Landing)
                layerdepth = 1f;

            Texture2D facingTexture;
            if (facingRight)
                facingTexture = DownRight;
            else facingTexture = DownLeft;

            sbatch.Draw(facingTexture, location + CameraOffset, animation.SourceRect, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, layerdepth);

            if (state != State.Landed && state != State.Rising && state != State.Landing)
                sbatch.Draw(Shadow, new Vector2(location.X + 32 - 15, location.Y + 192 + 32) + CameraOffset, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.99f); 

            if (state == State.Rising || state == State.Landing)
                sbatch.Draw(Shadow, new Vector2(location.X + 32 - 15, tile.Position.Y + 32) + CameraOffset, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.99f);
        }

        protected enum State
        {
            Roaming, 
            Rising, 
            Landed,
            Landing, 
            Hovering,  
            Searching
        }

    }
}
