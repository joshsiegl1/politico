using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; 

namespace Politico2.Politico.Effects
{
    internal class WaterEffect : Effect
    {
        Texture2D texture;

        //List<WaterParticle> Particles;
        Vector2 position;

        float killTimer = 0f; 

        public WaterEffect(Texture2D p_Texture, Vector2 position)
        {
            Particles = new List<Particle>();
            this.position = position;
            this.texture = p_Texture;
            AddParticle();
        }

        void AddParticle()
        {
            WaterParticle p = new WaterParticle();
            p.position = new Vector2(random.Next((int)position.X - 7, (int)position.X + 7), position.Y + 20);
            p.texture = texture;
            p.velocity = new Vector2(0, random.Next(1, 100)) * 0.05f;
            p.color = ColorIndex[random.Next(ColorIndex.Length)];
            p.scale = 0.5f;
            p.DropLocation = (int)position.Y + 192;  
            Particles.Add(p);
        }

        Color[] ColorIndex = new Color[3] { Color.Aqua, Color.LightBlue, Color.CornflowerBlue };

        float particleaddTimer = 0f;
        public override void Update(GameTime gametime)
        {
            killTimer += (float)gametime.ElapsedGameTime.TotalMilliseconds;

            if (killTimer >= 4000f)
                Kill(); 

            if (!kill)
                particleaddTimer += (float)gametime.ElapsedGameTime.TotalMilliseconds;

            if (particleaddTimer >= 5f)
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

            base.Update(gametime);
        }

        public override void Draw(SpriteBatch sbatch)
        {
            foreach (WaterParticle p in Particles)
                p.Draw(sbatch); 

            base.Draw(sbatch);
        }
    }
}
