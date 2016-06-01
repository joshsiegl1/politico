using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; 

namespace Politico2.Politico.Disasters
{
    internal abstract class Disaster
    {
        public abstract void Update(GameTime gametime);
        public abstract void Draw(SpriteBatch sbatch); 
    }
}
