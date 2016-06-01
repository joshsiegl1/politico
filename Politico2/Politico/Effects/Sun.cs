using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; 

namespace Politico2.Politico.Effects
{
    public class Sun
    {
        static Random rand = new Random(); 

        List<SunParticle> Particles;

        public static Texture2D Texture; 

        public Sun(Vector2 position)
        {
            Particles = new List<SunParticle>(); 

            for (int i = 0; i < 10; i++)
            {
                SunParticle p = new SunParticle();
                p.color = Colors[rand.Next(Colors.Length)];
                p.position = position;
                p.scale = (float)rand.NextDouble();
                p.randAdditive = (float)rand.Next(10, 1000); 
                p.texture = Texture;
                Particles.Add(p); 
            }
        }

        Color[] Colors = new Color[] { Color.Yellow, Color.Red, Color.Orange }; 

        public void Update(GameTime gametime)
        {
            foreach (SunParticle p in Particles)
                p.Update(gametime);
        }

        public void UpdateOrbit(float angle)
        {
            for (int i = 0; i < Particles.Count; i++)
            {
                Particles[i].position.X = centerOrigin.X + (float)Math.Sin(MathHelper.ToRadians(angle)) * 750f;
                Particles[i].position.Y = centerOrigin.Y + (float)Math.Cos(MathHelper.ToRadians(angle)) * 750f;
            }
        }

        Vector2 centerOrigin = new Vector2(1920 / 2, 1080 / 2);
        public void Draw(SpriteBatch sbatch)
        {
            foreach (SunParticle p in Particles)
                p.Draw(sbatch); 
        }

    }
}
