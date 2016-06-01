using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; 

namespace Politico2.Politico.Effects
{
    internal class DustEffect : Effect
    {
        Vector2 position;
        Texture2D texture, nightTexture;

        public DustEffect(Texture2D p_texture, Texture2D p_texture_night, Vector2 position)
        {
            Particles = new List<Particle>();
            this.texture = p_texture;
            this.nightTexture = p_texture_night; 
            this.position = position;

            for (int i = 0; i < 10; i++)
                AddParticle(); 
        }

        void AddParticle()
        {
            DustParticle p = new DustParticle(nightTexture);
            p.position = position + new Vector2(25, 25);
            p.texture = texture;
            p.velocity = new Vector2(random.Next(-10, 10), random.Next(-10, 10)) * 0.05f;
            p.rotation = (float)random.NextDouble();
            p.color = Color.White;
            p.scale = 0.5f;
            Particles.Add(p);
        }

        public override void Update(GameTime gametime, Tiles.Tile[,] Tiles)
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
