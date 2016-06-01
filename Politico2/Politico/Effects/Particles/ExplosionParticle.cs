using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; 

namespace Politico2.Politico.Effects
{
    internal class ExplosionParticle : Particle
    {
        float scaleIncrease = 0.01f;
        byte decreaseAmount = 5; 
        public ExplosionParticle(float scaleIncrease, byte decreaseAmount)
        {
            this.scaleIncrease = scaleIncrease;
            this.decreaseAmount = decreaseAmount; 
        }

        public override void Update(GameTime gametime)
        {

            scale += scaleIncrease;

            timer += (float)gametime.ElapsedGameTime.TotalMilliseconds;
            if (timer >= 1 || Kill)
            {
                color.A -= decreaseAmount;
                color.R -= decreaseAmount;
                color.G -= decreaseAmount;
                color.B -= decreaseAmount;

                if (color.A <= 5) remove = true;
            }

            //if (scale > 0.5f) remove = true;

            base.Update(gametime);
        }

        Vector2 origin;
        public override void Draw(SpriteBatch sbatch)
        {
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            sbatch.Draw(texture, position + Camera.Pos, null, color, 0f, origin, scale, SpriteEffects.None, 1f);
            base.Draw(sbatch);
        }
    }
}
