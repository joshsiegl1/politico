using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework; 

namespace Politico2.Politico.Effects
{
    internal class SmokeParticle : Particle
    {

        private Texture2D nightTexture; 
        public SmokeParticle(Texture2D nightParticle)
        {
            this.nightTexture = nightParticle; 
        }

        public override void Update(GameTime gametime)
        {
            timer += (float)gametime.ElapsedGameTime.TotalMilliseconds;
            if (timer >= 1000)
            {
                color.A -= 5;
                color.R -= 5;
                color.G -= 5;
                color.B -= 5;

                if (color.A <= 5) remove = true;
            }

            base.Update(gametime);
        }

        public override void Draw(SpriteBatch sbatch)
        {
            sbatch.Draw(texture, position + Camera.Pos, null, color * Night.DayColor, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.95f + Night.DayAdditive); 
            sbatch.Draw(nightTexture, position + Camera.Pos, null, color * Night.NightColor, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.95f + Night.NightAdditive);
            base.Draw(sbatch);
        }
    }
}
