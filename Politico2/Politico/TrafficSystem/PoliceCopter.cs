using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Politico2.Politico.TrafficSystem
{
    internal class PoliceCopter : Helicopter
    {
        public struct Textures
        {
            public static Texture2D DownLeftTexture;
            public static Texture2D DownRightTexture;
        }

        Tiles.PoliceStation policestation;

        List<Tiles.Tile> TilesRiot;

        int UniqueTile; 
        public int _UniqueTile { get { return UniqueTile; } }

        public PoliceCopter(Tiles.PoliceStation policestation, int Unique) : base(policestation.Position, Textures.DownLeftTexture, Textures.DownRightTexture,
            Textures.DownRightTexture, Textures.DownRightTexture, policestation)
        {
            this.policestation = policestation;
            TilesRiot = new List<Tiles.Tile>();

            UniqueTile = Unique; 
        }

        public delegate void RiotDoneEvent(object sender, EventArgs e, Tiles.Tile t);
        public event RiotDoneEvent onRiotDone;

        public void Dispatch(Tiles.Tile t)
        {
            if (!TilesRiot.Contains(t))
                TilesRiot.Add(t);
        }

        public override void Update(GameTime gametime)
        {
            stateSwitchTimer += (float)gametime.ElapsedGameTime.TotalMilliseconds;

            switch (state)
            {
                case State.Landed:
                    if (TilesRiot.Count > 0)
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
                        Search(new Vector2(TilesRiot[0].Position.X, TilesRiot[0].Position.Y - 192));
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
                    if (stateSwitchTimer >= 3000f)
                    {
                        if (onRiotDone != null && TilesRiot.Count > 0)
                            onRiotDone(this, EventArgs.Empty, TilesRiot[0]);

                        if (TilesRiot.Count > 0) TilesRiot.RemoveAt(0);

                        if (TilesRiot.Count > 0)
                            Search(new Vector2(TilesRiot[0].Position.X, TilesRiot[0].Position.Y - 192));
                        else if (this.location.Y != YRiseToPoint)
                            Search(new Vector2(policestation.Position.X, policestation.Position.Y - 192));
                        else
                        {
                            landPosition = policestation.Position;
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