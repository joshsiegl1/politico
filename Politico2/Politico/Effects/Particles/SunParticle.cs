using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Politico2.Politico.Effects
{
    internal class SunParticle : Particle
    {
        public float LayerDepth;
        public float randAdditive; 

        public SunParticle()
        {
            LayerDepth = 0.00003f; 
        }

        public override void Update(GameTime gametime)
        {
            base.Update(gametime);

            double time = gametime.TotalGameTime.TotalSeconds;
            float pulsate = (float)Math.Sin(time * randAdditive * 0.01f) + 10;
            scale = (pulsate) * 0.1f;
        }

        public override void Draw(SpriteBatch sbatch)
        {
            sbatch.Draw(texture, position, null, color * Night.DayColor, 0f, new Vector2(100, 100), scale, SpriteEffects.None, LayerDepth); 
        }
    }
}
