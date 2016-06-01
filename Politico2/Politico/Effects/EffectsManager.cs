using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Politico2.Politico.Tiles;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; 

namespace Politico2.Politico.Effects
{
    public class EffectsManager
    {
        public struct ParticleTextures
        {
            public static Texture2D Smoke;
            public static Texture2D Smoke_Night;
            public static Texture2D Dust;
            public static Texture2D Dust_Night; 
            public static Texture2D Fire;
            public static Texture2D Water;
            public static Texture2D Explosion;
            public static Texture2D Cloud;
            public static Texture2D Cloud_Night;
            public static Texture2D Rain; 
        }

        List<Effect> Effects;
        List<Effect> AlphaEffects;

        Dictionary<int, FireEffect> FireDictionairy = new Dictionary<int, FireEffect>(); 


        public EffectsManager()
        {
            Effects = new List<Effect>();
            AlphaEffects = new List<Effect>(); 
        }

        public void Add(EffectType type, Tile t)
        {
            switch (type)
            {
                case EffectType.Fract:
                    if (!Night.IsNight) Effects.Add(new FractEffect(t.ParentTexture, t.ParentTexture.Bounds, t.Position));
                    else Effects.Add(new FractEffect(t.ParentTextureNight, t.ParentTextureNight.Bounds, t.Position)); 
                    break;
                case EffectType.CoalStack:
                    Effects.Add(new CoalStackEffect(ParticleTextures.Smoke, ParticleTextures.Smoke_Night, t.Position, t.UniqueKey)); 
                    break;
                case EffectType.Fire:
                    if (!FireDictionairy.ContainsKey(t.UniqueKey))
                        FireDictionairy.Add(t.UniqueKey, new FireEffect(ParticleTextures.Fire, t.Position)); 
                    break; 
            }
        }

        public void Add(EffectType type, Vector2 position)
        {
            switch(type)
            {
                case EffectType.Water:
                    AlphaEffects.Add(new WaterEffect(ParticleTextures.Water, position)); 
                    break;
                case EffectType.Explosion:
                    AlphaEffects.Add(new ExplosionEffect(ParticleTextures.Explosion, position, 25, 0.01f, 5)); 
                    break;
                case EffectType.Dust:
                    Effects.Add(new DustEffect(ParticleTextures.Dust, ParticleTextures.Dust_Night, position)); 
                    break; 
            }
        }

        public void AddCloud(bool offScreen, Vector2 spanX, Vector2 spanY, Vector2 velocity)
        {
            Effects.Add(new CloudEffect(offScreen, spanX, spanY, velocity, 250)); 
        }

        public void AddCloud(Vector2 center, Vector2 spanX, Vector2 spanY, Vector2 velocity)
        {
            Effects.Add(new CloudEffect(center, spanX, spanY, velocity, 250, 1f));
        }

        public void KillFire(Tile t)
        {
            if (FireDictionairy.ContainsKey(t.UniqueKey))
            {
                FireDictionairy[t.UniqueKey].Kill();
                FireKillTimer = 0f; 
            }
        }

        public void KillSmokeStack(Tile t)
        {
            for (int i = 0; i < Effects.Count; i++)
            {
                if (Effects[i] is CoalStackEffect)
                {
                    CoalStackEffect cs = (CoalStackEffect)Effects[i];
                    if (cs.Key == t.UniqueKey) Effects.RemoveAt(i); 
                }
            }
        }

        public void KillAllEffects()
        {
            Effects = new List<Effect>(); 
        }

        public enum EffectType
        {
            Fract, 
            CoalStack, 
            Fire, 
            Water, 
            Explosion, 
            Dust, 
            Ash, 
            Cloud
        }

        float FireKillTimer = 0f;
        float AlphaKillTimer = 0f; 
        public void Update(GameTime gametime, Tile[,] Tiles)
        {
            foreach (Effect e in Effects)
                e.Update(gametime, Tiles);

            foreach (Effect e in AlphaEffects)
                e.Update(gametime);

            for (int i = 0; i < FireDictionairy.Count; i++)
            {
                FireDictionairy[FireDictionairy.ElementAt(i).Key].Update(gametime); 
                if (FireDictionairy[FireDictionairy.ElementAt(i).Key].Killed)
                {
                    FireKillTimer += (float)gametime.ElapsedGameTime.TotalMilliseconds; 
                    if (FireKillTimer >= 5000f)
                    {
                        FireDictionairy.Remove(FireDictionairy.ElementAt(i).Key);
                        FireKillTimer = 0f; 
                    }
                }
            }

            for (int i = 0; i < AlphaEffects.Count; i++)
            {
                AlphaEffects[i].Update(gametime);
                if (AlphaEffects[i].Killed)
                {
                    AlphaKillTimer += (float)gametime.ElapsedGameTime.TotalMilliseconds; 
                    if (AlphaKillTimer >= 5000f)
                    {
                        AlphaEffects.RemoveAt(i);
                        AlphaKillTimer = 0f; 
                    }
                }     
            }



        }

        public void Draw(SpriteBatch sbatch)
        {
            foreach (Effect e in Effects)
                e.Draw(sbatch);
        }

        public void DrawWithAlphaBlend(SpriteBatch sbatch)
        {
            foreach (Effect e in AlphaEffects)
                e.Draw(sbatch); 

            foreach (FireEffect e in FireDictionairy.Values)
            {
                e.Draw(sbatch);
            }
        }
    }
}
