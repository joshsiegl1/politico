using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; 

namespace Politico2.Politico.Effects.Particles
{
    internal class RainParticle : Particle
    { 
        public RainParticle(Vector2 position)
        {
            this.position = position; 
            rotation = MathHelper.ToRadians(315f);
            velocity = new Vector2(20, 10); 
        }

        public void Reset(Vector2 position)
        {
            this.position = position; 
        }

        public override void Draw(SpriteBatch sbatch)
        {
            sbatch.Draw(EffectsManager.ParticleTextures.Rain, position, null, Color.White, rotation, Vector2.Zero, 1f, SpriteEffects.None, 1f); 
            base.Draw(sbatch);
        }
    }
}
