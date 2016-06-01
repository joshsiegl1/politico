using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Politico2.Politico.Effects
{
    internal class ExplosionEffect : Effect
    {
        Vector2 position;
        Texture2D texture;
        float ScaleIncrease = 0.01f;
        byte DecreaseAmount = 5; 

        public ExplosionEffect(Texture2D texture, Vector2 position, int count, float ScaleIncrease, byte DecreaseAmount)
        {
            Particles = new List<Particle>();
            this.position = position;
            this.texture = texture;
            this.ScaleIncrease = ScaleIncrease;
            this.DecreaseAmount = DecreaseAmount; 

            for (int i = 0; i < count; i++)
            {
                AddParticle(); 
            }
        }

        void AddParticle()
        {
            ExplosionParticle p = new ExplosionParticle(ScaleIncrease, DecreaseAmount);
            p.position = position;
            p.texture = texture;
            p.velocity = new Vector2(random.Next(-10, 10), random.Next(-10, 10)) * 0.05f;
            p.rotation = (float)random.NextDouble();
            p.color = Color.White;
            p.scale = 0.05f; 
            Particles.Add(p);
        }

        public override void Update(GameTime gametime)
        {
            for (int i = 0; i < Particles.Count; i++)
            {
                Particles[i].Update(gametime);
                if (Particles[i].remove)
                    Particles.RemoveAt(i);
            }

            base.Update(gametime);
        }

        public override void Draw(SpriteBatch sbatch)
        {
            foreach (Particle p in Particles)
                p.Draw(sbatch); 

            base.Draw(sbatch);
        }
    }
}
