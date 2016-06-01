using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics; 
using Microsoft.Xna.Framework; 

namespace Politico2.Politico.Effects
{
    internal class FireParticle : Particle
    {
        public FireParticle() : base()
        {
            
        }

        public override void Update(GameTime gametime)
        {

            scale -= 0.01f; 

            timer += (float)gametime.ElapsedGameTime.TotalMilliseconds;
            if (timer >= 2000 || Kill)
            {
                color.A -= 5;
                color.R -= 5;
                color.G -= 5;
                color.B -= 5;

                if (color.A <= 5) remove = true;
            }

            if (scale < 0.1f) remove = true; 

            base.Update(gametime);
        }

        Vector2 origin; 
        public override void Draw(SpriteBatch sbatch)
        {
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            sbatch.Draw(texture, position + origin + Camera.Pos, null, color, 0f, origin, scale, SpriteEffects.None, 1f); 
            base.Draw(sbatch);
        }
    }
}
