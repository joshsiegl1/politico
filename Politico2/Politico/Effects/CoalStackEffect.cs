using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Politico2.Politico.Tiles;

namespace Politico2.Politico.Effects
{
    internal class CoalStackEffect : Effect
    {
        Texture2D texture, nightTexture; 
        Vector2 position;

        int key; 
        public int Key { get { return key; } }

        public CoalStackEffect(Texture2D p_Texture, Texture2D p_NightTexture, Vector2 position, int key)
        {
            Particles = new List<Particle>();
            this.position = position;
            this.texture = p_Texture;
            this.nightTexture = p_NightTexture;
            this.key = key; 
            AddParticle(); 
        }

        float particleaddTimer = 0f; 

        public override void Update(GameTime gametime, Tile[,] Tiles)
        {
            particleaddTimer += (float)gametime.ElapsedGameTime.TotalMilliseconds;

            if (particleaddTimer >= Global.SmokeParticleAddTimer)
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
            base.Update(gametime, Tiles);
        }

        void AddParticle()
        {
            SmokeParticle p = new SmokeParticle(nightTexture);
            p.position = position;
            p.texture = texture;
            p.velocity = new Vector2(random.Next(-5, 0), random.Next(1, 10)) * -0.05f;
            p.color = Color.White; 
            Particles.Add(p); 
        }

        public override void Draw(SpriteBatch sbatch)
        {
            foreach (SmokeParticle p in Particles)
                p.Draw(sbatch);

            base.Draw(sbatch);
        }
    }
}
