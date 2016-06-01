using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; 

namespace Politico2.Politico.Effects
{
    internal class DustParticle : Particle
    {
        Texture2D nightParticle; 

        public DustParticle(Texture2D nightParticle)
        {
            this.nightParticle = nightParticle; 
        }

        public override void Update(GameTime gametime)
        {

            scale += 0.05f;

            timer += (float)gametime.ElapsedGameTime.TotalMilliseconds;
            if (timer >= 500 || Kill)
            {
                color.A -= 5;
                color.R -= 5;
                color.G -= 5;
                color.B -= 5;

                if (color.A <= 5) remove = true;
            }

            //if (scale > 0.5f) remove = true;

            base.Update(gametime);
        }

        Vector2 origin;
        public override void Draw(SpriteBatch sbatch)
        {
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            sbatch.Draw(texture, position + Camera.Pos, null, color * Night.DayColor, 0f, origin, scale, SpriteEffects.None, 0.99f + Night.DayAdditive);
            sbatch.Draw(nightParticle, position + Camera.Pos, null, color * Night.NightColor, 0f, origin, scale, SpriteEffects.None, 0.99f + Night.NightAdditive);
            base.Draw(sbatch);
        }
    }
}
