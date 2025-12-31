using Virtual_Pets.Classes.Pets;
using Virtual_Pets.Classes.Shop;
using Virtual_Pets.Interfaces;

namespace Virtual_Pets.Classes
{
    public class Player : IPlayer
    {
        private string playerName { get; set; }
        private int coins { get; set; }
        private Inventory inventory { get; set; } = new Inventory();

        private List<Pet> pets { get; set; } = new List<Pet>();
        public Player(string playerName, int coins)
        {
            this.playerName = playerName;
            this.coins = coins;
        }
        // Methods
        public string GetName()
        {
            return playerName;
        }

        public int GetBalance()
        {
            return coins;
        }

        public void EarnCoins(int amount)
        {
            coins += amount;
        }

        public bool SpendCoins(int amount)
        {
            if (amount > coins)
            {
                return false;
            }
            else
            {
                coins -= amount;
                return true;
            }
        }

        public void AddPet(Pet pet)
        {
            pets.Add(pet);
        }

        public List<Pet> GetPets()
        {
            return pets;
        }

        public Inventory GetInventory()
        {
            return inventory;
        }
    }
}
