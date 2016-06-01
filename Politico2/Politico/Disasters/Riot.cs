using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Politico2.Politico.Tiles; 

namespace Politico2.Politico.Disasters
{
    internal class Riot : Disaster
    {
        Tile t;

        float RiotTimer = 0f;
        bool kill; 
        public Riot(Tile t)
        {
            this.t = t; 
        }

        public bool Kill { get { return kill; } }
        public override void Update(GameTime gametime)
        {
            RiotTimer += (float)gametime.ElapsedGameTime.TotalMilliseconds; 
            if (RiotTimer >= 15000f)
            {
                kill = true; 
            }
        }

        public override void Draw(SpriteBatch sbatch)
        {
            sbatch.Draw(DisasterManager.Textures.RiotTexture, t.Position + Camera.Pos, DisasterManager.Animations.RiotAnimation.SourceRect, Color.White, 0f, Vector2.Zero, 1f,
                SpriteEffects.None, t.LayerDepth + 0.001f);
        }
    }
}
