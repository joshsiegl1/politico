using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; 

namespace Politico2.Politico.Effects.Particles
{
    internal class CloudParticle : Particle
    {
        float randAddtive; 
        public CloudParticle(Vector2 position, Vector2 velocity)
        {
            this.position = position;
            this.velocity = velocity; 
            randAddtive = (float)random.NextDouble() + 1; 
            this.texture = EffectsManager.ParticleTextures.Cloud;
            this.texture_night = EffectsManager.ParticleTextures.Cloud_Night; 
        }

        public override void Update(GameTime gametime)
        {
            double time = gametime.TotalGameTime.TotalSeconds;
            float pulsate = (float)Math.Sin(randAddtive * time + 5) * 0.5f; //time * (1 * 0.01f)) + 1;
            scale = pulsate + 7;

            base.Update(gametime);
        }

        public override void Draw(SpriteBatch sbatch)
        {
            sbatch.Draw(texture, position, null, Color.White * Night.DayColor, 0f, new Vector2(texture.Width / 2, texture.Height / 2), scale, SpriteEffects.None, 0.00001f + Night.DayAdditive);
            sbatch.Draw(texture_night, position, null, Color.White * Night.NightColor, 0f, new Vector2(texture.Width / 2, texture.Height / 2), scale, SpriteEffects.None, 0.00001f + Night.NightAdditive); 
        }
    }
}
