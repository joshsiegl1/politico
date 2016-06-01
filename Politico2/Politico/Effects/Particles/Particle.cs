using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; 

namespace Politico2.Politico.Effects
{
    internal abstract class Particle
    {
        protected static Random random = new Random(); 

        public Vector2 position;
        public Vector2 velocity; 
        public Color color;
        public float rotation;
        public Texture2D texture;
        public Texture2D texture_night; 
        public Rectangle bounds;
        public bool remove;
        public float timer;
        public float scale = 1f;
        public bool Kill;

        public Particle() { }

        public virtual void Update(GameTime gametime)
        {
            position += velocity; 
        }

        public virtual void Draw(SpriteBatch sbatch) { }
    }
}
