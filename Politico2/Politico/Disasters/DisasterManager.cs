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
    public class DisasterManager
    {
        static Random random = new Random(); 

        public struct Textures
        {
            public static Texture2D RiotTexture;
            public static Texture2D MissleTexuture;
            public static Texture2D SpaceShip;
            public static Texture2D LazerBeam;
            public static Texture2D Jet;
            public static Texture2D Bomb;
            public static Texture2D Lightning; 
        }

        public struct Animations
        {
            public static Animation RiotAnimation
                = new Animation(Vector2.Zero, 64, 64, 4, true, 16);
        }

        private Dictionary<int, Riot> Riots;

        private Nuke nuke;
        private Alien alien;
        private War war;
        private Storm storm; 

        public DisasterManager()
        {
            Riots = new Dictionary<int, Riot>(); 
        }

        public void Add(DisasterType type, Tile t)
        {
            switch(type)
            {
                case DisasterType.Riot:
                    if (!Riots.ContainsKey(t.UniqueKey))
                        Riots.Add(t.UniqueKey, new Riot(t)); 
                    break;
                case DisasterType.Nuke:
                    nuke = new Nuke();
                    nuke.onNukeHit += Nuke_onNukeHit;
                    GUI.BottomBar.AddMessage(new GUI.BottomBar.Message("NUKE!", Color.Red)); 
                    break;
                case DisasterType.Alien:
                    alien = new Alien();
                    alien.onShootTileDestroy += Alien_onShootTileDestroy;
                    GUI.BottomBar.AddMessage(new GUI.BottomBar.Message("ALIENS!", Color.Red));
                    break;
                case DisasterType.War:
                    war = new War();
                    war.onTileBombed += War_onTileBombed;
                    GUI.BottomBar.AddMessage(new GUI.BottomBar.Message("WE'RE BEING ATTACKED!", Color.Red));
                    break;
                case DisasterType.Storm:
                    storm = new Storm();
                    storm.onTileStruck += Storm_onTileStruck;
                    break;
            }
        }

        public delegate void StormTileStruck(object sender, EventArgs e, Tile t);
        public event StormTileStruck onStormTileStruck; 
        private void Storm_onTileStruck(object sender, EventArgs e, Tile t)
        {
            if (onStormTileStruck != null)
                onStormTileStruck(sender, e, t); 
        }

        public delegate void WarTileBombed(object sender, EventArgs e, Tile t);
        public event WarTileBombed onWarTileBombed; 
        private void War_onTileBombed(object sender, EventArgs e, Tile t)
        {
            if (onWarTileBombed != null)
                onWarTileBombed(sender, e, t); 
        }

        public void KillRiot(Tile t)
        {
            if (Riots.ContainsKey(t.UniqueKey))
            {
                Riots.Remove(t.UniqueKey); 
            }
        }

        public enum DisasterType
        {
            Riot, 
            Nuke, 
            Alien, 
            War, 
            Storm
        }

        public delegate void DustHandler(object sender, EventArgs e, Tile t);
        public event DustHandler onDustHandle; 

        float EarthQuakeTimer = 0f;
        public void Update(GameTime gametime, Tiles.Tile[,] Tiles)
        {
            if (nuke != null)
            {
                nuke.Update(gametime, Tiles);
                if (nuke._NukeComplete) nuke = null;
            }

            if (alien != null)
            {
                alien.Update(gametime, Tiles);
                if (alien.Kill) alien = null; 
            }

            if (war != null)
            {
                war.Update(gametime, Tiles);
                if (war.Kill) war = null; 
            }

            if (storm != null)
            {
                storm.Update(gametime, Tiles);
                if (storm.Kill) storm = null; 
            }

            for (int i = 0; i < Riots.Count; i++)
            {
                Riots[Riots.ElementAt(i).Key].Update(gametime);
                if (Riots[Riots.ElementAt(i).Key].Kill)
                    Riots.Remove(Riots.ElementAt(i).Key);  
            }

            Animations.RiotAnimation.UpdateSpriteSheet(gametime);

            if (Camera._Shaking && Camera.Earthqauake)
            {
                EarthQuakeTimer += (float)gametime.ElapsedGameTime.TotalMilliseconds; 
                if (EarthQuakeTimer >= 200f)
                {
                    EarthQuakeTimer = 0f;
                    int w = random.Next(Grid.GridWidth);
                    int h = random.Next(Grid.GridHeight); 
                    if (onDustHandle != null && Tiles[w, h].EarthQuakeDestroy())
                        onDustHandle(this, EventArgs.Empty, Tiles[w, h]); 
                }
            }
        }

        public delegate void ShootTileDestroyEvent(object sender, EventArgs e, Tile killTile);
        public event ShootTileDestroyEvent onShootTileDestroy; 
        private void Alien_onShootTileDestroy(object sender, EventArgs e, Tile killTile)
        {
            if (onShootTileDestroy != null)
                onShootTileDestroy(sender, e, killTile); 
        }

        public event EventHandler onNukeHit; 
        private void Nuke_onNukeHit(object sender, EventArgs e)
        {
            if (onNukeHit != null)
                onNukeHit(sender, e); 
        }

        public void DrawRiots(SpriteBatch sbatch)
        {
            foreach (Riot r in Riots.Values)
                r.Draw(sbatch); 
        }

        public void DrawAlien(SpriteBatch sbatch)
        {
            if (alien != null)
                alien.Draw(sbatch); 
        }

        public void DrawAlienBeams(SpriteBatch sbatch)
        {
            if (alien != null)
                alien.DrawLazerBeams(sbatch); 
        }

        public void DrawNuke(SpriteBatch sbatch)
        {
            if (nuke != null)
                nuke.Draw(sbatch);
        }

        public void DrawWar(SpriteBatch sbatch)
        {
            if (war != null)
                war.Draw(sbatch); 
        }

        public void DrawStorm(SpriteBatch sbatch)
        {
            if (storm != null)
                storm.Draw(sbatch); 
        }

    }
}
