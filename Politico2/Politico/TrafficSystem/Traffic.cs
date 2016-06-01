using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Politico2.Politico.Tiles; 

namespace Politico2.Politico.TrafficSystem
{
    public class Traffic
    {
        static Random random = new Random(); 

        private List<Car> cars; 

        private List<FireCopter> fireCopters;
        private List<PoliceCopter> policeCopters; 
        
        public Traffic()
        {
            cars = new List<Car>();
            fireCopters = new List<FireCopter>();
            policeCopters = new List<PoliceCopter>(); 
        }

        public void AddCar(Tiles.Road road)
        {
            cars.Add(new Car(road)); 
        }

        public void ClearAllTraffic()
        {
            cars = new List<Car>();
            fireCopters = new List<FireCopter>();
            policeCopters = new List<PoliceCopter>(); 
        }

        public void DispatchFireCopter(Tiles.Tile t)
        {
            if (fireCopters.Count > 0)
            {
                fireCopters[random.Next(fireCopters.Count)].Dispatch(t); 
            }
        }

        public void DispatchPoliceCopter(Tiles.Tile t)
        {
            if (policeCopters.Count > 0)
            {
                policeCopters[random.Next(policeCopters.Count)].Dispatch(t); 
            }
        }

        public void AddPoliceCopter(Tiles.PoliceStation policestation)
        {
            PoliceCopter p = new PoliceCopter(policestation, policestation.UniqueKey);
            p.onRiotDone += P_onRiotDone;
            policeCopters.Add(p); 
        }

        private void P_onRiotDone(object sender, EventArgs e, Tile t)
        {
            if (onRiotDone != null)
                onRiotDone(sender, e, t); 
        }

        public delegate void ChopperRemoveEvent(object sender, EventArgs e, Vector2 position);
        public event ChopperRemoveEvent onChopperRemove; 
        public void RemoveChopper(int unique)
        {
            for (int i = 0; i < policeCopters.Count; i++)
            {
                if (policeCopters[i]._UniqueTile == unique)
                {
                    if (onChopperRemove != null)
                        onChopperRemove(this, EventArgs.Empty, policeCopters[i].Position); 

                    policeCopters.RemoveAt(i);

                }
            }

            for (int i = 0; i < fireCopters.Count; i++)
            {
                if (fireCopters[i]._UniqueTile == unique)
                {
                    if (onChopperRemove != null)
                        onChopperRemove(this, EventArgs.Empty, fireCopters[i].Position);

                    fireCopters.RemoveAt(i);
                }
            }
        }

        public void RemoveChopperRandom()
        {
            int rand = random.Next(2);

            if (rand == 0)
            {
                if (fireCopters.Count > 0)
                {
                    int remove = random.Next(fireCopters.Count);
                    if (onChopperRemove != null)
                        onChopperRemove(this, EventArgs.Empty, fireCopters[remove].Position);

                    fireCopters.RemoveAt(remove);
                }

                if (policeCopters.Count > 0)
                {
                    int remove = random.Next(policeCopters.Count);
                    if (onChopperRemove != null)
                        onChopperRemove(this, EventArgs.Empty, policeCopters[remove].Position);

                    policeCopters.RemoveAt(remove);
                }
            }
            else
            {
                if (policeCopters.Count > 0)
                {
                    int remove = random.Next(policeCopters.Count);
                    if (onChopperRemove != null)
                        onChopperRemove(this, EventArgs.Empty, policeCopters[remove].Position);

                    policeCopters.RemoveAt(remove);
                }

                if (fireCopters.Count > 0)
                {
                    int remove = random.Next(fireCopters.Count);
                    if (onChopperRemove != null)
                        onChopperRemove(this, EventArgs.Empty, fireCopters[remove].Position);

                    fireCopters.RemoveAt(remove);
                }
            }
        }

        public delegate void RiotDoneEvent(object sender, EventArgs e, Tiles.Tile t);
        public event RiotDoneEvent onRiotDone;

        public void AddFireCopter(Tiles.FireStation firestation)
        {
            FireCopter f = new FireCopter(firestation, firestation.UniqueKey);
            f.onFireOut += F_onFireOut;
            f.onWaterEffect += F_onWaterEffect;
            fireCopters.Add(f); 
        }

        public delegate void WaterEffectEvent(object sender, EventArgs e, Vector2 position);
        public event WaterEffectEvent onWaterEffect; 
        private void F_onWaterEffect(object sender, EventArgs e, Vector2 position)
        {
            if (onWaterEffect != null)
                onWaterEffect(sender, e, position);
        }

        public delegate void FireOutEvent(object sender, EventArgs e, Tiles.Tile t);
        public event FireOutEvent onFireOut;
        private void F_onFireOut(object sender, EventArgs e, Tile t)
        {
            if (onFireOut != null)
                onFireOut(sender, e, t); 
        }

        public void UpdateRoadMap(Tiles.Tile[,] Grid)
        {
            foreach (Car c in cars)
                c.UpdateRoadMap(Grid); 
        }

        public void Update(GameTime gametime)
        {
            for (int i = 0; i < cars.Count; i++)
            {
                if (!cars[i].TrafficLightWait)
                    cars[i].Wait(false); 

                for (int x = 0; x < cars.Count; x++)
                {
                    if (cars[i]._Next == cars[x]._Current && !cars[i].isOppositeDirection(cars[x]._CurrentDirection)) //&& cars[x]._CurrentDirection == cars[i]._CurrentDirection
                    {

                        cars[i].Wait(true);
                        break; 
                    }
                }

                cars[i].Update(gametime); 
            }

            for (int i = 0; i < fireCopters.Count; i++)
            {
                fireCopters[i].Update(gametime);
            }

            for (int i = 0; i < policeCopters.Count; i++)
            {
                policeCopters[i].Update(gametime); 
            }
        }

        public void Draw(SpriteBatch sbatch)
        {
            foreach (Car c in cars)
                c.Draw(sbatch, Camera.Pos);

            foreach (FireCopter h in fireCopters)
                h.Draw(sbatch, Camera.Pos);

            foreach (PoliceCopter h in policeCopters)
                h.Draw(sbatch, Camera.Pos); 
        }
    }
}
