using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Politico2.Politico.Tiles; 

namespace Politico2.Politico.TrafficSystem
{
    public class Car : Vehicle
    {
        static Random random = new Random(); 

        public struct Cars
        {
            public static Texture2D Texture_upright, Texture_downright, Texture_upleft, Texture_downleft;
            public static Texture2D Texture2_upright, Texture2_downright, Texture2_upleft, Texture2_downleft;
            public static Texture2D Texture3_upright, Texture3_downright, Texture3_upleft, Texture3_downleft;
            public static Texture2D Texture4_upright, Texture4_downright, Texture4_upleft, Texture4_downleft;
        }

        public Car(Road Current) : base(Current, Cars.Texture_downright, Cars.Texture_downleft, Cars.Texture_upleft, Cars.Texture_upright)
        {
            int CarType = random.Next(4); 
            switch (CarType)
            {
                case 0:
                    ChangeBaseTextures(Cars.Texture_downright, Cars.Texture_downleft, Cars.Texture_upleft, Cars.Texture_upright); 
                    break;
                case 1:
                    ChangeBaseTextures(Cars.Texture2_downright, Cars.Texture2_downleft, Cars.Texture2_upleft, Cars.Texture2_upright);
                    break;
                case 2:
                    ChangeBaseTextures(Cars.Texture3_downright, Cars.Texture3_downleft, Cars.Texture3_upleft, Cars.Texture3_upright);
                    break;
                case 3:
                    ChangeBaseTextures(Cars.Texture4_downright, Cars.Texture4_downleft, Cars.Texture4_upleft, Cars.Texture4_upright);
                    break;
            }
        }


    }
}
