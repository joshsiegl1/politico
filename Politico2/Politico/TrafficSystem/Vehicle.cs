using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Politico2.Politico.Tiles; 

namespace Politico2.Politico.TrafficSystem
{
    public class Vehicle
    {
        static Random random = new Random(); 

        public enum Direction { topright, topleft, bottomright, bottomleft, None}

        float speed;

        Road Current, Next;

        public Road _Next { get { return Next; } }
        public Road _Current { get { return Current; } }

        Direction CurrentDirection, NextDirection, LastDirection;

        public Direction _CurrentDirection { get { return CurrentDirection; } }

        Vector2 offset; 

        Texture2D Texture_downright, Texture_downleft, Texture_upleft, Texture_upright;

        public int UniqueKey { get { return Current.UniqueKey; } }

        public Vehicle(Road current, Texture2D texture_downright, Texture2D texture_downleft, Texture2D texture_upleft, 
            Texture2D texture_upright)
        {
            Texture_downright = texture_downright;
            Texture_downleft = texture_downleft;
            Texture_upright = texture_upright;
            Texture_upleft = texture_upleft; 
            Current = current;

            

            CurrentDirection = Direction.topright;

            LastDirection = Direction.None; 

            Next = MakeDecision(); 
        }

        public bool isOppositeDirection(Direction d)
        {
            if (CurrentDirection == Direction.bottomleft && d == Direction.topright)
                return true;
            else if (CurrentDirection == Direction.topleft && d == Direction.bottomright)
                return true;
            else if (CurrentDirection == Direction.bottomright && d == Direction.topleft)
                return true;
            else if (CurrentDirection == Direction.topright && d == Direction.bottomleft)
                return true;
            else return false; 
        }

        protected void ChangeBaseTextures(Texture2D downright, Texture2D downleft, Texture2D upleft, Texture2D upright)
        {
            this.Texture_downright = downright;
            this.Texture_downleft = downleft;
            this.Texture_upleft = upleft;
            this.Texture_upright = upright; 
        }

        public void UpdateRoadMap(Tiles.Tile[,] tiles)
        {
            Current.onPlace(tiles);
            Next.onPlace(tiles); 
        }

        Road MakeDecision()
        {
            List<Road> options = new List<Road>();
            List<Direction> directions = new List<Direction>();

            NextDirection = CurrentDirection; 

            if (Current.topright is Road)
            {
                Road r = Current.topright as Road; 
                options.Add(r);
                directions.Add(Direction.topright); 
            }

            if (Current.topleft is Road)
            {
                Road r = Current.topleft as Road; 
                options.Add(r);
                directions.Add(Direction.topleft); 
            }

            if (Current.bottomleft is Road)
            {
                Road r = Current.bottomleft as Road; 
                options.Add(r);
                directions.Add(Direction.bottomleft); 
            }

            if (Current.bottomright is Road)
            {
                Road r = Current.bottomright as Road; 
                options.Add(r);
                directions.Add(Direction.bottomright); 
            }

            if (options.Count <= 0) return Current;

            
            //if we have more than one, then don't go back the way we came
            if (options.Count >= 2)
            {
                //Make sure we do not turn around on a curve

                RemoveOppositeDirection(CurrentDirection, ref directions, ref options, Direction.bottomleft, Direction.topright);
                RemoveOppositeDirection(CurrentDirection, ref directions, ref options, Direction.topright, Direction.bottomleft);
                RemoveOppositeDirection(CurrentDirection, ref directions, ref options, Direction.bottomright, Direction.topleft);
                RemoveOppositeDirection(CurrentDirection, ref directions, ref options, Direction.topleft, Direction.bottomright); 
            }

            //Get a random option from out options
            int option = random.Next(options.Count);

            NextDirection = directions[option]; 
            return (options[option]); 
        }

        void RemoveOppositeDirection(Direction current, ref List<Direction> directs, ref List<Road> road, Direction remove, Direction opposite)
        {
            if (current == remove)
            {
                if (directs.Contains(opposite))
                {
                    int index = directs.IndexOf(opposite); 
                    directs.RemoveAt(index);
                    road.RemoveAt(index); 
                }
            }
        }

        float waitTimer = 0f;
        public static float waittime = 800f;//937.5f; 
        bool waiting = false; 
        public void Wait(bool waiting)
        {
            this.waiting = waiting; 
        }

        void trafficLightWait(bool wait)
        {
            this.TrafficLightWait = wait; 

            if (wait)
            {
                waitTimer = 0f;
                offset = Vector2.Zero;
            }
        }

        float ForcedWaitTimer = 0f; 
        void ForceDecision()
        {
            Next = MakeDecision();
            ForcedWaitTimer = 0f; 
        }

        public bool TrafficLightWait = false; 
        void HandleTrafficLights()
        {
            if (Next.pieceType == Road.PieceType.tri || Next.pieceType == Road.PieceType.cross)
            {
                if (CurrentDirection == Direction.topleft || CurrentDirection == Direction.bottomright)
                {
                    if (Next.TrafficCanGo == Road.CanGo.upright_downleft)
                    {
                        Wait(true);
                        trafficLightWait(true);
                        ForceDecision(); 
                    }
                    else
                    {
                        trafficLightWait(false);
                    }
                }
                else
                {
                    if (Next.TrafficCanGo == Road.CanGo.downright_upleft)
                    {
                        Wait(true);
                        trafficLightWait(true);
                        ForceDecision(); 
                    }
                    else
                    {
                        trafficLightWait(false);
                    }
                }
            }
        }

        public virtual void Update(GameTime gametime)
        {
            if (!waiting)
                waitTimer += (float)gametime.ElapsedGameTime.TotalMilliseconds;
            else ForcedWaitTimer += (float)gametime.ElapsedGameTime.TotalMilliseconds;

            if (ForcedWaitTimer >= waittime)
                ForceDecision(); 

            if (waitTimer >= waittime)
            {
                Current = Next;
                LastDirection = CurrentDirection;
                Next = MakeDecision();
                CurrentDirection = NextDirection; 
                waitTimer = 0f;
                offset = Vector2.Zero;
                waiting = false; 
            }

            HandleTrafficLights();

            if (!waiting)
            {

                if (CurrentDirection == Direction.topleft)
                {
                    offset += new Vector2(-2f / 3, -1f / 3);
                }
                else if (CurrentDirection == Direction.bottomright)
                {
                    offset += new Vector2(2f / 3, 1f / 3);
                }
                else if (CurrentDirection == Direction.topright)
                {
                    offset += new Vector2(2f / 3, -1f / 3);
                }
                else if (CurrentDirection == Direction.bottomleft)
                {
                    offset += new Vector2(-2f / 3, 1f / 3);
                }
            }
        }

        float LayerDepth = 0.011f; 
        public virtual void Draw(SpriteBatch sbatch, Vector2 CameraOffset)
        {
            if (CurrentDirection == Direction.bottomright)
                sbatch.Draw(Texture_downright, Current.Position + offset + CameraOffset, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, LayerDepth); 
            else if (CurrentDirection == Direction.bottomleft)
                sbatch.Draw(Texture_downleft, Current.Position + offset + CameraOffset, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, LayerDepth);
            else if (CurrentDirection == Direction.topright)
                sbatch.Draw(Texture_upright, Current.Position + offset + CameraOffset, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, LayerDepth);
            else sbatch.Draw(Texture_upleft, Current.Position + offset + CameraOffset, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, LayerDepth);
        }
    }
}
