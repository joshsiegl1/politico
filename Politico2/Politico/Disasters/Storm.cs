using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Politico2.Politico.Effects;
using Politico2.Politico.Tiles; 

namespace Politico2.Politico.Disasters
{
    internal class Storm : Disaster
    {
        private CloudEffect cloudEffect;
        private RainEffect rainEffect;

        static Random random = new Random(); 

        float StormTimer = 0f;
        float LightningTimer = 0f;
        float LightningDisplayTimer = 0f; 
        public Storm()
        {
            cloudEffect = new CloudEffect(Vector2.Zero, new Vector2(0, 1920), new Vector2(0, 1080), Vector2.Zero, 100, 5f);
            rainEffect = new RainEffect();
            Night.ForceStorm(true);
            LightningDisplayTimer = 100f; 
        }

        public override void Draw(SpriteBatch sbatch)
        {
            cloudEffect.Draw(sbatch);
            rainEffect.Draw(sbatch); 

            if (LightningDisplayTimer <= 100f)
            {
                sbatch.Draw(DisasterManager.Textures.Lightning, lightningPos, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f); 
            }
        }

        public override void Update(GameTime gametime) { }

        private bool kill; 
        public bool Kill { get { return kill; } }

        Vector2 lightningPos = new Vector2(); 

        public void Update(GameTime gametime, Tile[,] Tiles)
        {
            StormTimer += (float)gametime.ElapsedGameTime.TotalMilliseconds;
            if (StormTimer >= 17000)
            {
                Night.ForceStorm(false); 
                kill = true;
            }

            LightningTimer += (float)gametime.ElapsedGameTime.TotalMilliseconds; 
            if (LightningTimer >= 5000f)
            {
                Point p = new Point(random.Next(Grid.GridWidth), random.Next(Grid.GridHeight));
                Tile t = Tiles[p.X, p.Y]; 
                lightningPos = new Vector2(t.Position.X, 0 - (1080 - t.Position.Y));
                LightningDisplayTimer = 0f; 

                if (onTileStruck != null)
                    onTileStruck(this, EventArgs.Empty, t); 

                LightningTimer = 0f; 
            }

            LightningDisplayTimer += (float)gametime.ElapsedGameTime.TotalMilliseconds; 

            cloudEffect.Update(gametime, Tiles);
            rainEffect.Update(gametime, Tiles); 
        }

        public delegate void TileStruckEvent(object sender, EventArgs e, Tile t);
        public event TileStruckEvent onTileStruck; 
    }
}
