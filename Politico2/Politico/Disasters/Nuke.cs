using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Politico2.Politico.Effects; 

namespace Politico2.Politico.Disasters
{
    internal class Nuke : Disaster
    {
        private Vector2 MisslePosition;

        List<ExplosionEffect> Explosions; 

        private bool NukeComplete = false; 
        public bool _NukeComplete { get { return NukeComplete; } }

        private bool DrawMissle = false;

        public Nuke()
        {
            MisslePosition = new Vector2((1920 / 2) - (DisasterManager.Textures.MissleTexuture.Width / 2), -500);
            Explosions = new List<ExplosionEffect>();
            DrawMissle = true; 
        }

        public override void Draw(SpriteBatch sbatch)
        {
            if (DrawMissle)
                sbatch.Draw(DisasterManager.Textures.MissleTexuture, MisslePosition, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.FlipVertically, 1f);

            for (int i = 0; i < Explosions.Count; i++)
                Explosions[i].Draw(sbatch); 
        }

        public override void Update(GameTime gametime)
        {

        }

        bool drawExplosions; 

        float KillEffectTimer = 0f;
        public event EventHandler onNukeHit; 
        public void Update(GameTime gametime, Tiles.Tile[,] Tiles)
        {
            foreach (ExplosionEffect e in Explosions)
                e.Update(gametime); 

            MisslePosition.Y += 25;

            if (MisslePosition.Y >= 500)
            {
                if (onNukeHit != null && DrawMissle)
                {
                    onNukeHit(this, EventArgs.Empty); 
                }

                DrawMissle = false;

                if (!drawExplosions)
                {
                    for (int x = 0; x < Grid.GridWidth; x += 2)
                    {
                        for (int y = 0; y < Grid.GridHeight; y += 4)
                        {
                            if (!(Tiles[x, y] is Tiles.Empty))
                                Explosions.Add(new ExplosionEffect(EffectsManager.ParticleTextures.Explosion, Tiles[x, y].Position, 1, 0.1f, 2));
                        }
                        drawExplosions = true;
                    }
                }
            }

            if (!DrawMissle)
            {
                KillEffectTimer += (float)gametime.ElapsedGameTime.TotalMilliseconds; 
                if (KillEffectTimer >= 5000f)
                {
                    NukeComplete = true; 
                }
            }
        }
    }
}
