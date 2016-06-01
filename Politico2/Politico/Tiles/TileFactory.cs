using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework; 

namespace Politico2.Politico.Tiles
{
    public static class TileFactory
    {
        public static Tile Get(int number, Vector2 position)
        {
            switch (number)
            {
                case 0:
                    return new Empty(position);
                case 1:
                    return new Grass(position);
                case 2:
                    return new Capital(position);
                case 3:
                    return new Road(position);
                case 4:
                    return new House(position);
                case 5:
                    return new WindTurbine(position);
                case 6:
                    return new Coal(position);
                case 7:
                    return new Corporation(position);
                case 8:
                    return new CorpFactory(position);
                case 9:
                    return new Apartment(position);
                case 10:
                    return new Condo(position);
                case 11:
                    return new PoliceStation(position);
                case 12:
                    return new FireStation(position);
                case 13:
                    return new Water(position);
                case 14:
                    return new Tree(position); 
                    
                    
            }

            return new Grass(position); 
        }
    }
}
