using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Politico2.Politico.Tiles; 

namespace Politico2.Politico.Disasters
{
    internal class Alien : Disaster
    {
        bool kill;
        readonly float SpaceShipWidth; 
        const float WaitTime = 10000; 
        Vector2 spaceShipLocation;

        List<LazerBeam> LazerBeams;

        public delegate void ShootTileDestroy(object sender, EventArgs e, Tile killTile);

        public Alien()
        {
            spaceShipLocation = new Vector2(-500, 0);
            LazerBeams = new List<LazerBeam>();
            SpaceShipWidth = (DisasterManager.Textures.SpaceShip.Width / 2); 
        }

        public void DrawLazerBeams(SpriteBatch sbatch)
        {
            foreach (LazerBeam lazer in LazerBeams)
            {
                lazer.Draw(sbatch); 
            }
        }

        public override void Draw(SpriteBatch sbatch)
        {
            sbatch.Draw(DisasterManager.Textures.SpaceShip, spaceShipLocation, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f); 
        }

        float spaceShipWaitTimer = 0f;
        float lazerBeamAddTimer = 0f; 
        bool hasStopped = false; 
        public bool Kill { get { return kill; } }
        static Random rand = new Random(); 
        public override void Update(GameTime gametime) { }

        public void Update(GameTime gametime, Tile[,] Tiles)
        {
            if (spaceShipLocation.X < (1920 / 2) - SpaceShipWidth && !hasStopped)
            {
                spaceShipLocation.X += 2;
            }
            else if (spaceShipLocation.X >= (1920 / 2) - SpaceShipWidth && !hasStopped)
            {
                lazerBeamAddTimer += (float)gametime.ElapsedGameTime.TotalMilliseconds;
                if (lazerBeamAddTimer >= 500)
                {
                    Point randTile = new Point(rand.Next(Grid.GridWidth), rand.Next(Grid.GridHeight));
                    Tile toDestroy = Tiles[randTile.X, randTile.Y];

                    while (!toDestroy.CanBeShot())
                    {
                        randTile = new Point(rand.Next(Grid.GridWidth), rand.Next(Grid.GridHeight));
                        toDestroy = Tiles[randTile.X, randTile.Y];
                    }

                    LazerBeam beam = new LazerBeam(toDestroy.Position, spaceShipLocation + new Vector2(SpaceShipWidth, 75), toDestroy);
                    beam.ShootTileDestroyEvent += Beam_ShootTileDestroyEvent;
                    LazerBeams.Add(beam); 
                    lazerBeamAddTimer = 0f;
                }

                spaceShipWaitTimer += (float)gametime.ElapsedGameTime.TotalMilliseconds;
                if (spaceShipWaitTimer >= WaitTime)
                {
                    hasStopped = true;
                }
            }
            else
            {
                spaceShipLocation.X += 2;
            }

            for (int i = 0; i < LazerBeams.Count; i++)
            {
                LazerBeams[i].Update(gametime);
                if (LazerBeams[i].Kill)
                    LazerBeams.RemoveAt(i); 
            }


            if (spaceShipLocation.X >= 1920)
            {
                kill = true;
            }
        }

        public event ShootTileDestroy onShootTileDestroy; 
        private void Beam_ShootTileDestroyEvent(object sender, EventArgs e, Tile killTile)
        {
            if (onShootTileDestroy != null)
                onShootTileDestroy(sender, e, killTile); 
        }

        class LazerBeam
        {
            Vector2 position, ShootPoint, Velocity, StartPosition;
            float rotation;
            bool kill; 
            public bool Kill { get { return kill; } }
            Rectangle bounds, shootBounds;
            Tile ShootTile; 
            public LazerBeam(Vector2 shootPoint, Vector2 position, Tile shootTile)
            {
                this.ShootPoint = shootPoint;
                this.position = position;
                this.StartPosition = position; 
                rotation = MathHelper.ToRadians((float)Math.Atan2((double)ShootPoint.X, (double)ShootPoint.Y));
                shootBounds = new Rectangle((int)ShootPoint.X, (int)ShootPoint.Y, 25, 25);
                this.ShootTile = shootTile; 
            }

            Vector2 direction; 
            public void Update(GameTime gametime)
            {
                bounds = new Rectangle((int)position.X, (int)position.Y, 25, 25); 

                direction = (ShootPoint - StartPosition);
                direction.Normalize();

                rotation = Vector2ToRadian(direction); 

                Velocity = new Vector2((float)Math.Sin(rotation), -(float)Math.Cos(rotation)); 

                position += Velocity * 20;

                if (bounds.Intersects(shootBounds))
                {
                    kill = true;
                    if (ShootTileDestroyEvent != null)
                        ShootTileDestroyEvent(this, EventArgs.Empty, ShootTile); 
                }
            }

            public event ShootTileDestroy ShootTileDestroyEvent; 
            private float Vector2ToRadian(Vector2 direction)
            {
                return (float)Math.Atan2(direction.X, -direction.Y);
            }

            public void Draw(SpriteBatch sbatch)
            {
                sbatch.Draw(DisasterManager.Textures.LazerBeam, position, null, Color.Purple, rotation,
                    new Vector2(DisasterManager.Textures.LazerBeam.Width / 2, DisasterManager.Textures.LazerBeam.Height / 2), 1f, SpriteEffects.None, 0.999f);
            }
        }
    }
}
