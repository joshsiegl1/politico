using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; 

using Politico2.Politico.Tiles; 

namespace Politico2.Politico.Effects
{
    internal abstract class Effect
    {
        protected static Random random = new Random();

        protected List<Particle> Particles; 

        public virtual void Update(GameTime gametime) { }
        public virtual void Update(GameTime gametime, Tile[,] Tiles) { }
        public virtual void Draw(SpriteBatch sbatch) { }

        protected bool kill;
        public bool Killed { get { return kill; } }

        public void Kill()
        {
            for (int i = 0; i < Particles.Count; i++)
                Particles[i].Kill = true;

            kill = true;
        }
    }
}
