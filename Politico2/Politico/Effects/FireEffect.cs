using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; 

namespace Politico2.Politico.Effects
{
    internal class FireEffect : Effect
    {
        Texture2D texture;

        Vector2 position;

        public FireEffect(Texture2D p_Texture, Vector2 position)
        {
            Particles = new List<Particle>();
            this.position = position;
            this.texture = p_Texture;
            AddParticle();
        }

        float particleaddTimer = 0f;
        float killTimer = 0f; 
        public override void Update(GameTime gametime)
        {
            if (!kill)
                particleaddTimer += (float)gametime.ElapsedGameTime.TotalMilliseconds;

            if (particleaddTimer >= 25f)
            {
                AddParticle();
                particleaddTimer = 0f;
            }

            for (int i = 0; i < Particles.Count; i++)
            {
                Particles[i].Update(gametime);
                if (Particles[i].remove)
                    Particles.RemoveAt(i);
            }

            killTimer += (float)gametime.ElapsedGameTime.TotalMilliseconds; 
            if (killTimer >= 15000f)
            {
                kill = true; 
            }

            base.Update(gametime);
        }

        void AddParticle()
        {
            FireParticle p = new FireParticle();
            p.position = position;
            p.texture = texture;
            p.velocity = new Vector2(random.Next(-3, 3), random.Next(1, 10)) * -0.1f;
            p.rotation = (float)random.NextDouble();
            p.color = ColorIndex[random.Next(ColorIndex.Length)];
            Particles.Add(p);
        }

        static Color[] ColorIndex = new Color[3] { Color.Orange, Color.Crimson, Color.Red }; 

        public override void Draw(SpriteBatch sbatch)
        {
            foreach (FireParticle p in Particles)
                p.Draw(sbatch);

            base.Draw(sbatch);
        }
    }
}
