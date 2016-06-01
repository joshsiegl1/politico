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
    internal class CloudEffect : Effect
    {
        Vector2 center; 
        public CloudEffect(bool offScreen, Vector2 spanX, Vector2 spanY, Vector2 velocity, int particleCount)
        {
            Particles = new List<Particle>();

            if (offScreen) center = new Vector2(random.Next(-500, -100), random.Next(1080));
            else center = new Vector2(random.Next(1920), random.Next(1080)); 

            CreateCloud(spanX, spanY, velocity, particleCount); 
        }

        public CloudEffect(Vector2 center, Vector2 spanX, Vector2 spanY, Vector2 velocity, int particleCount, float particleScale)
        {
            Particles = new List<Particle>();

            this.center = center; 

            CreateCloud(spanX, spanY, velocity, particleCount, particleScale);
        }

        private void CreateCloud(Vector2 spanX, Vector2 spanY, Vector2 velocity, int particleCount)
        {
            Particles.Add(new CloudParticle(center, velocity));
            for (int i = 0; i < particleCount; i++)
            {
                Particles.Add(new CloudParticle(center + new Vector2(random.Next((int)spanX.X, (int)spanX.Y), random.Next((int)spanY.X, (int)spanY.Y)), velocity)); 
            }
        }

        private void CreateCloud(Vector2 spanX, Vector2 spanY, Vector2 velocity, int particleCount, float scale)
        {
            Particles.Add(new CloudParticle(center, velocity));
            for (int i = 0; i < particleCount; i++)
            {
                CloudParticle p = new CloudParticle(center + new Vector2(random.Next((int)spanX.X, (int)spanX.Y), random.Next((int)spanY.X, (int)spanY.Y)), velocity);
                p.scale = scale;
                Particles.Add(p);
            }
        }

        public override void Update(GameTime gametime, Tiles.Tile[,] Tiles)
        {
            foreach (Particle p in Particles)
                p.Update(gametime); 

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
