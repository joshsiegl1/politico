using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; 

namespace Politico2.Politico.TrafficSystem
{
    internal class FireCopter : Helicopter
    {
        public struct Textures
        {
            public static Texture2D DownLeftTexture;
            public static Texture2D DownRightTexture;
        }

        Tiles.FireStation firestation;

        List<Tiles.Tile> TilesOnFire;

        int UniqueTile;
        public int _UniqueTile { get { return UniqueTile; } }

        public FireCopter(Tiles.FireStation firestation, int Unique) : base(firestation.Position, Textures.DownLeftTexture, Textures.DownRightTexture,
            Textures.DownRightTexture, Textures.DownRightTexture, firestation)
        {
            this.firestation = firestation;
            TilesOnFire = new List<Tiles.Tile>();

            UniqueTile = Unique; 
        }

        public delegate void FireOutEvent(object sender, EventArgs e, Tiles.Tile t);
        public event FireOutEvent onFireOut;

        public delegate void WaterEffectEvent(object sender, EventArgs e, Vector2 position);
        public event WaterEffectEvent onWaterEffect; 

        public void Dispatch(Tiles.Tile t)
        {
            if (!TilesOnFire.Contains(t))
                TilesOnFire.Add(t);
        }

        bool waterpoured = false; 
        public override void Update(GameTime gametime)
        {
            stateSwitchTimer += (float)gametime.ElapsedGameTime.TotalMilliseconds;

            switch (state)
            {
                case State.Landed:
                    if (TilesOnFire.Count > 0)
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
                        Search(new Vector2(TilesOnFire[0].Position.X, TilesOnFire[0].Position.Y - 192)); 
                        stateSwitchTimer = 0f;
                    }
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

                    if (!waterpoured && TilesOnFire.Count > 0)
                    {
                        if (onWaterEffect != null)
                            onWaterEffect(this, EventArgs.Empty, this.location); 

                        waterpoured = true; 
                    }

                    if (stateSwitchTimer >= 3000f)
                    {
                        waterpoured = false; 

                        if (onFireOut != null && TilesOnFire.Count > 0)
                            onFireOut(this, EventArgs.Empty, TilesOnFire[0]); 

                        if (TilesOnFire.Count > 0) TilesOnFire.RemoveAt(0);

                        if (TilesOnFire.Count > 0)
                        {
                            Search(new Vector2(TilesOnFire[0].Position.X, TilesOnFire[0].Position.Y - 192));
                        }
                        else if (this.location.Y != YRiseToPoint)
                        {
                            Search(new Vector2(firestation.Position.X, firestation.Position.Y - 192));
                        }
                        else
                        {
                            landPosition = firestation.Position;
                            Land(landPosition);
                        }

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
    }
}
