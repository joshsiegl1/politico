using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Politico2.Politico.Effects; 

namespace Politico2.Politico
{
    public class Night
    {
        static bool isNight; 
        public static bool IsNight { get { return isNight; } }

        static float nightAdditive, dayAdditive; 
        public static float NightAdditive { get { return nightAdditive;  } }
        public static float DayAdditive { get { return dayAdditive; } }

        static float nightColor, dayColor; 
        public static float DayColor { get { return dayColor; } }
        public static float NightColor { get { return nightColor; } }

        static Random random;

        public static Texture2D Moon; 

        public class Star
        {
            public Star() { }
            public static Texture2D Texture; 
            public Vector2 position;
            public float scale;
            public int randadditive; 
            public Color color; 
        }

        public static Texture2D BackgroundTexture;
        public static Texture2D StormBackgroundTexture; 

        private List<Star> stars, starstop;

        private static Sun sun;

        public Night()
        {
            random = new Random(); 

            stars = new List<Star>();
            starstop = new List<Star>(); 
            for (int i = 0; i < Global.numStarCount; i++)
            {
                Star s = new Star();
                s.color = Color.White;
                s.position = new Vector2(random.Next(1920), random.Next(1080));
                s.scale = (float)(random.NextDouble() / 5);
                s.randadditive = random.Next(1, 100);
                stars.Add(s);
                starstop.Add(s); 
            }

            dayColor = 1f;
            nightColor = 0f;

            sun = new Sun(new Vector2(1920 / 2, 1080 / 2));

            NightAngle = angle; 
        }

        static float NightAngle; 
        static float backgroundColor = 0f; 
        public void Update(GameTime gametime)
        {
            angle += 0.1f;
            NightAngle += 0.1f;

            if (angle >= 440)
            {
                angle = 80f;
                NightAngle = 260f; 
            }

            if (NightAngle >= 260)
            {
                if (isNight)
                {
                    nightColor -= 0.01f;
                    backgroundColor -= 0.01f; 
                    dayColor = 1f;

                    if (nightColor <= 0f) nightColor = 0f;
                    if (backgroundColor <= 0f) backgroundColor = 0f; 
                    if (dayColor >= 1f) dayColor = 1f; 
                }
                else
                {
                    nightColor = 1f;
                    backgroundColor += 0.01f; 
                    dayColor -= 0.01f;

                    if (nightColor >= 1f) nightColor = 1f;
                    if (backgroundColor >= 1f) backgroundColor = 1f; 
                    if (dayColor <= 0f) dayColor = 0f; 
                }
            }

            if (NightAngle >= 270f)
            {
                isNight = !isNight; 
                NightAngle = 80f; 
            }

            dayAdditive = nightAdditive = 0f;

            if (isNight || isStorm)
                nightAdditive = 0.0000001f;
            else dayAdditive = 0.0000001f;

            double time = gametime.TotalGameTime.TotalSeconds;
            

            for (int i = 0; i < stars.Count; i++)
            {
                float pulsate = (float)Math.Sin(time * (stars[i].randadditive * 0.01f)) + 1;
                stars[i].scale = ( pulsate) * 0.05f; 
            }
 
            moonPosition.X = centerOrigin.X + (float)Math.Sin(MathHelper.ToRadians(angle + 180f)) * 750f;
            moonPosition.Y = centerOrigin.Y + (float)Math.Cos(MathHelper.ToRadians(angle + 180f)) * 750f;

            moonPosition.X -= 100;
            moonPosition.Y -= 100;

            sun.UpdateOrbit(angle);
            sun.Update(gametime);



        }

        static bool isStorm = false; 
        public static bool IsStorm { get { return isStorm; } }
        public static void ForceStorm(bool storm)
        {
            isStorm = storm;
            if (storm)
            {
                nightColor = 1f;
                dayColor = 0f;
            }
            else
            {
                nightColor = 0f;
                dayColor = 1f;
                angle = 80f;
                NightAngle = 80f;
                sun = new Sun(new Vector2(1920 / 2, 1080 / 2));
                moonPosition = Vector2.Zero;
                backgroundColor = 0f;
                isNight = false; 
            }
        }

        Vector2 centerOrigin = new Vector2(1920 / 2, 1080 / 2); 
        static Vector2 moonPosition;
        static float angle = 80f; 
        public void Draw(SpriteBatch sbatch, Matrix ScreenMatrix)
        {
            //if (isNight)
            //{
            if (isStorm)
            {
                sbatch.Draw(StormBackgroundTexture, new Rectangle(0, 0, 1920, 1200), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.00001f);
            }
            else
            {
                sbatch.Draw(BackgroundTexture, new Rectangle(0, 0, 1920, 1200), null, Color.White * backgroundColor, 0f, Vector2.Zero, SpriteEffects.None, 0.00001f);
            }

            if (!IsStorm)
            {
                sbatch.End();
                sbatch.Begin(SpriteSortMode.FrontToBack, BlendState.Additive, null, null, null, null, ScreenMatrix);

                foreach (Star s in stars)
                    sbatch.Draw(Star.Texture, s.position, null, s.color * backgroundColor, 0f, new Vector2(32, 32), s.scale, SpriteEffects.None, 0.00002f);

                foreach (Star s in starstop)
                    sbatch.Draw(Star.Texture, s.position, null, s.color * backgroundColor, 0f, new Vector2(32, 32), -s.scale, SpriteEffects.None, 0.00003f);

                sun.Draw(sbatch);

                sbatch.Draw(Moon, moonPosition, null, Color.White * backgroundColor, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.00004f);



                sbatch.End();
                sbatch.Begin();
            }
            //}
        }

        public void DrawDebug(SpriteBatch sbatch)
        {
            if(Global.DrawDebug)
                sbatch.DrawString(Assets.debugFont, "Angle: " + angle.ToString(), new Vector2(300, 700), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.00005f);
        }

    }
}
