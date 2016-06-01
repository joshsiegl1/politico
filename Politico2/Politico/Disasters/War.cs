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
    internal class War : Disaster
    {
        public delegate void TileBombed(object sender, EventArgs e, Tile t); 

        public static Random random; 
        List<Jet> Jets; 

        public War()
        {
            Jets = new List<Jet>();
            random = new Random();  
        }

        public override void Draw(SpriteBatch sbatch)
        {
            foreach (Jet j in Jets)
                j.Draw(sbatch); 
        }

        float AddJetTimer = 0f;
        float EffectTimer = 0f;
        float EndEffectTimer = 0f;
        bool kill; 
        public bool Kill { get { return kill; } }
        public override void Update(GameTime gametime) { }

        public event TileBombed onTileBombed; 
        public void Update(GameTime gametime, Tile[,] Tiles)
        {
            AddJetTimer += (float)gametime.ElapsedGameTime.TotalMilliseconds;
            EffectTimer += (float)gametime.ElapsedGameTime.TotalMilliseconds;
            if (AddJetTimer >= 500)
            {
                Jet j = new Jet(new Vector2(-100, random.Next(0, 300)), Tiles);
                j.onTileBombed += J_onTileBombed;
                Jets.Add(j);
                AddJetTimer = 0f;
            }

            for (int i = 0; i < Jets.Count; i++)
            {
                Jets[i].Update(gametime);
                if (Jets[i].Kill)
                    Jets.RemoveAt(i);
            }

            if (EffectTimer >= 5000)
            {
                AddJetTimer = 0f;
                EndEffectTimer += (float)gametime.ElapsedGameTime.TotalMilliseconds;
                if (EndEffectTimer >= 3000f)
                {
                    kill = true;
                }
            }
        }

        private void J_onTileBombed(object sender, EventArgs e, Tile t)
        {
            if (onTileBombed != null)
                onTileBombed(sender, e, t); 
        }

        class Jet
        {
            Vector2 position, bombDropPosition;
            bool kill; 
            public bool Kill { get { return kill; } }
            Bomb bomb;

            static Random rand = new Random();
            Tile toDestroy; 

            public Jet(Vector2 position, Tile[,] Tiles)
            {
                this.position = position;

                Point randTile = new Point(rand.Next(Grid.GridWidth), rand.Next(Grid.GridHeight));
                toDestroy = Tiles[randTile.X, randTile.Y];

                while (!toDestroy.CanBeBombed())
                {
                    randTile = new Point(rand.Next(Grid.GridWidth), rand.Next(Grid.GridHeight));
                    toDestroy = Tiles[randTile.X, randTile.Y];
                }

                bombDropPosition = new Vector2(toDestroy.Position.X, position.Y); 
            }

            public event TileBombed onTileBombed;
            bool dropped = false;  
            public void Update(GameTime gametime)
            {
                position.X += 20;
                if (position.X >= 2500) kill = true;

                if (bomb == null && !dropped)
                {
                    if (position.X >= bombDropPosition.X)
                        bomb = new Bomb(bombDropPosition, toDestroy.Position); 
                }


                if (bomb != null)
                {
                    bomb.Update(gametime);
                    if (bomb.Kill && onTileBombed != null)
                    {
                        onTileBombed(this, EventArgs.Empty, toDestroy);
                        bomb = null;
                        dropped = true; 
                    }
                }
            }

            public void Draw(SpriteBatch sbatch)
            {
                sbatch.Draw(DisasterManager.Textures.Jet, position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                if (bomb != null)
                    bomb.Draw(sbatch); 
            }

        }

        class Bomb
        {
            Vector2 position, LandPosition;
            bool kill; 
            public bool Kill { get { return kill; } }
            public Bomb(Vector2 position, Vector2 landPosition)
            {
                this.position = position;
                this.LandPosition = landPosition; 
            }

            public void Update(GameTime gametime)
            {
                position.Y += 10;
                if (position.Y >= LandPosition.Y)
                    kill = true; 
            }

            public void Draw(SpriteBatch sbatch)
            {
                sbatch.Draw(DisasterManager.Textures.Bomb, position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.999f); 
            }

        }
    }
}
