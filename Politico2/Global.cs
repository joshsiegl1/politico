using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Politico2
{
    public static class Global
    {
        #region debug

        public static bool DrawDebug = false;

        #endregion

        #region Options

        public static bool Mute = false; 

        #endregion

        #region performance

        const int numStarsHigh = 500;
        const int numStarsMed = 250;
        const int numStarsLow = 100;

        public static int numStarCount = numStarsLow; 


        public static float SmokeParticleAddTimer = 300f; 

        #endregion
    }
}
