using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Virtual_Pets.Classes.Pets;
using Virtual_Pets.Classes.Shop.Items;

namespace Virtual_Pets.Interfaces
{
    internal interface IPlayer
    {
        public string GetName();
        public int GetBalance();
        public void EarnCoins(int balance);
        public bool SpendCoins(int balance);
        public List<Pet> GetPets();

    }
}
