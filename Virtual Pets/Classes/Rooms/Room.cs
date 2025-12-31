using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Virtual_Pets.Classes.Rooms
{
    internal class Room
    {
        private int currentTemperature { get; set; }
        private int ambientTemperature { get; set; }
        public Room(int ambientTemperature)
        {
            this.ambientTemperature = ambientTemperature;
            currentTemperature = ambientTemperature + 5;
        }

        public int GetCurrentTemperature()
        {
            return currentTemperature;
        }

        public int GetAmbientTemperature()
        {
            return ambientTemperature;
        }

        public int UpdateRoomTemperature(int amount)
        {
            if (currentTemperature > ambientTemperature)
            {
                currentTemperature -= amount;
            }
            else if (currentTemperature < ambientTemperature)
            {
                currentTemperature += amount;
            }
            return currentTemperature;
        }

        public void WarmRoom(int amount)
        {
            if (currentTemperature < 40)
            {
                currentTemperature += amount;
            }
        }

        public void CoolRoom(int amount)
        {
            if(currentTemperature > -10)
            {
                currentTemperature -= amount;
            }
        }
    }
}
