using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; 

namespace Politico2.Politico.GUI
{
    public class BottomBar
    {
        public struct Textures
        {
            public static Texture2D Background;
        }

        public struct Fonts
        {
            public static SpriteFont MessageFont; 
        }

        static Queue<Message> Messages;

        private bool show, inTransition;

        public bool Show { get { return show; } }

        readonly Vector2 HidePosition = new Vector2(0, 1180);
        readonly Vector2 ShowPosition = new Vector2(0, 980);

        Vector2 position, offset;

        public BottomBar()
        {
            position = new Vector2(0, 980); 

            Messages = new Queue<Message>();
            Message.Font = Fonts.MessageFont; 

            AddMessage(new Message("Hello, World!", Color.White));
            AddMessage(new Message("A fire has borken out!", Color.Red));
            AddMessage(new Message("Traffic is Steady", Color.White));

            show = true; 
        }

        public static void AddMessage(Message message)
        {
            Messages.Enqueue(message);
        }

        public void Toggle()
        {
            if (!inTransition)
                show = !show;
        }

        public void Update(GameTime gametime)
        {
            if (show)
            {
                if (position.Y + offset.Y > ShowPosition.Y)
                {
                    offset.Y -= 10f;
                    inTransition = true;
                }
                else inTransition = false;
            }
            else
            {
                if (position.Y + offset.Y < HidePosition.Y)
                {
                    offset.Y += 10f;
                    inTransition = true;
                }
                else inTransition = false;
            }

            if (show)
            {
                Message.TotalMessageLength -= 2;
                if (Message.TotalMessageLength <= 0)
                    Message.TotalMessageLength = 0;

                if (Messages.Count > 0)
                {
                    Message m = Messages.ElementAt(0);
                    if (m.Kill)
                        Messages.Dequeue();
                }
            }
        }

        public void Draw(SpriteBatch sbatch)
        {
            sbatch.Draw(Textures.Background, position + offset, Color.White);
            foreach (Message m in Messages)
            {
                m.Draw(sbatch, new Vector2(1920, position.Y + offset.Y)); 
            }
            
        }

        public class Message
        {
            string message;
            Color color;
            int length;
            public int Length { get { return length; } }
            public static SpriteFont Font;
            int scrollPosition;
            public int ScrollPosition { get { return scrollPosition; } }

            int lengthOffset; 
            public int LengthOffset { get { return lengthOffset; } }

            public static int TotalMessageLength;

            bool kill = false; 
            public bool Kill { get { return kill; } }

            public Message(string message, Color color)
            {
                this.message = message;
                this.color = color;
                length = (int)Font.MeasureString(message).X;

                lengthOffset = TotalMessageLength; 

                TotalMessageLength += length + 50;
            }

            public void Draw(SpriteBatch sbatch, Vector2 position)
            {
                scrollPosition -= 2;

                sbatch.DrawString(Font, message, position + new Vector2(scrollPosition + lengthOffset, 0), color);
                if (position.X + scrollPosition + lengthOffset < 0 - length)
                    kill = true; 
            }
        }
    }
}
