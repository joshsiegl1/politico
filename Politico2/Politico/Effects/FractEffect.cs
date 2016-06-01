using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Politico2.Politico.Tiles; 

namespace Politico2.Politico.Effects
{
    internal class FractEffect : Effect
    {
        private List<Fract> Fracts;

        private const int SIZE = 10; 

        public FractEffect(Texture2D texture, Rectangle textbounds, Vector2 position)
        {
            Fracts = new List<Fract>();
            
            for (int y = 0; y < textbounds.Height; y += SIZE)
            {
                for (int x = 0; x < textbounds.Width; x += SIZE)
                {
                    Vector2 pos; 
                    Rectangle fractbounds = new Rectangle(textbounds.X + x, textbounds.Y + y, SIZE, SIZE);
                    pos = new Vector2(position.X + x, position.Y + y); 
                    Fract f = new Fract(texture, fractbounds, pos);
                    Fracts.Add(f); 
                }
            } 
        }

        public override void Update(GameTime gametime, Tile[,] Tiles)
        {
            for (int i = 0; i < Fracts.Count; i++)
            {
                Fracts[i].UpdatePhysics(gametime, Tiles);
                Fracts[i].Update(gametime); 

                if (Fracts[i].remove)
                    Fracts.RemoveAt(i); 
            }
        }

        public override void Draw(SpriteBatch sbatch)
        {
            foreach (Fract f in Fracts)
                f.Draw(sbatch);
        }
    }
}
