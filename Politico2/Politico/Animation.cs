using Microsoft.Xna.Framework;

namespace Politico2.Politico
{
    public class Animation //Handles all Animation
    {
        private Vector2 TextureLocation;
        private int SpriteWidth;
        private int SpriteHeight;
        private bool IsLooping;
        public int FrameCount;
        private float Interval;
        private float Timer;
        public int CurrentFrame = 0;
        public Rectangle SourceRect;

        public Animation(Vector2 texturelocation, int spritewidth, int spriteheight, int framecount,
            bool islooping, float fps)
        {
            this.TextureLocation = texturelocation;
            this.SpriteWidth = spritewidth;
            this.SpriteHeight = spriteheight;
            this.FrameCount = framecount;
            this.IsLooping = islooping;
            this.Interval = 1000f / fps;

            SourceRect = new Rectangle((int)TextureLocation.X + SpriteWidth * CurrentFrame, (int)TextureLocation.Y,
                SpriteWidth, SpriteHeight);
        }
        public void UpdateSpriteSheet(GameTime gametime)
        {
            Timer += (float)gametime.ElapsedGameTime.TotalMilliseconds;
            if (Timer > Interval)
            {
                CurrentFrame++;
                if (IsLooping)
                {
                    if (CurrentFrame > FrameCount - 1)
                    {
                        CurrentFrame = 0;
                    }
                }
                else
                {
                    if (CurrentFrame > FrameCount - 1)
                    {
                        CurrentFrame = FrameCount - 1;
                    }
                }
                Timer = 0f;

            }
            SourceRect = new Rectangle((int)TextureLocation.X + SpriteWidth * CurrentFrame, (int)TextureLocation.Y,
                SpriteWidth, SpriteHeight);
        }
    }
}
