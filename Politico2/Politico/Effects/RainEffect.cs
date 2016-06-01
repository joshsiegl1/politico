using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Politico2.Politico.Effects.Particles; 

namespace Politico2.Politico.Effects
{
    internal class RainEffect : Effect
    {
        List<RainParticle> Rain; 

        public RainEffect()
        {
            Rain = new List<RainParticle>(); 
            for (int i = 0; i < 50; i++)
            {
                Rain.Add(new RainParticle(new Vector2(random.Next(-1920, 1920), -15))); 
            }
        }

        public override void Update(GameTime gametime, Tiles.Tile[,] Tiles)
        {
            for (int i = 0; i < Rain.Count; i++)
            {
                Rain[i].Update(gametime); 
                if (Rain[i].position.X >= 1920 || Rain[i].position.Y >= 1080)
                {
                    Rain[i].Reset(new Vector2(random.Next(-1920, 1920), -15)); 
                }
            }

            base.Update(gametime);
        }

        public override void Draw(SpriteBatch sbatch)
        {
            foreach (RainParticle r in Rain)
                r.Draw(sbatch); 

            base.Draw(sbatch);
        }
    }
}
