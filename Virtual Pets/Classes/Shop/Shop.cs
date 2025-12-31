using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Virtual_Pets.Classes.Enums;
using Virtual_Pets.Classes.Shop.Items;
using Virtual_Pets.Classes.Shop.Items.Medicine;
using Virtual_Pets.Classes.Shop.Items.Toys;
using Virtual_Pets.Items;
using static Virtual_Pets.Items.Food;

namespace Virtual_Pets.Classes.Shop
{
    public static class Shop // static class is used so we only have one shop instance
    {
        static Inventory shopInventory = new Inventory();
        public static bool shopping = false;
        public static Inventory GetShopInventory()
        {
            return shopInventory;
        }

        public static void DisplayShopInventory(Player player) // static method to display shop inventory for the player
        {
            Console.Clear();
            Console.WriteLine("========== PET SHOP ==========");
            Console.WriteLine($"Coins: {player.GetBalance()}");
            Console.WriteLine();
            Console.WriteLine("[1] Food");
            Console.WriteLine("[2] Toys");
            Console.WriteLine("[3] Medicine");
            Console.WriteLine("[0] Exit Shop");
        }

        public static void ShowCategory(ItemCategory category, Player player)
        {
            List<Item> items = GetShopInventory().GetItems(category);

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"==== {category.ToString().ToUpper()} ====");
                Console.WriteLine($"Coins: {player.GetBalance()}");
                Console.WriteLine();

                for (int i = 0; i < items.Count; i++)
                {
                    Console.WriteLine($"[{i + 1}] {items[i].GetName()} - {items[i].GetPrice()}");
                }

                Console.WriteLine("[0] Back");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Please enter a valid number.");
                    Console.ReadKey();
                    continue;
                }

                if (choice == 0)
                {
                    return;
                }

                if (choice < 1 || choice > items.Count)
                {
                    continue;
                }

                PurchaseItem(items[choice - 1], player);

            }
        }

        private static void PurchaseItem(Item item, Player player)
        {
            // Check if player has enough coins
            if (!player.SpendCoins((int)item.GetPrice()))
            {
                Console.WriteLine("You do not have enough coins to buy this item.");
                Console.ReadKey();
                return;
            }
            // Add item to player inventory
            player.GetInventory().AddItem(item.Clone()); // we need to clone the base item when we add it to the list. otherwise just a reference will be added, which means all items in the list will actually be the same item

            Console.WriteLine($"{item.GetName()} purchased successfully.");
            Console.WriteLine($"Remaining coins: {player.GetBalance()}");
            Console.WriteLine($"Press any key to continue.");
            Console.ReadKey();
        }


        public static void InitialShopSetup()
        {
            // =====================
            // CAT FOOD
            // =====================
            shopInventory.AddItem(new CatFood("Cat Chrynchy Kibble", 15, 3));
            shopInventory.AddItem(new CatFood("Feline Dry Mix", 25, 4));
            shopInventory.AddItem(new CatFood("Royal Cat Jelly Meal", 40, 8));

            // =====================
            // DOG FOOD
            // =====================
            shopInventory.AddItem(new DogFood("Dog Jelly Meal", 20, 3));
            shopInventory.AddItem(new DogFood("Gourment Canine Feast", 30, 6));
            shopInventory.AddItem(new DogFood("Royal Canin Maxi", 45, 9));

            // =====================
            // FISH FOOD
            // =====================
            shopInventory.AddItem(new FishFood("Basic Fish Flakes", 10, 2));
            shopInventory.AddItem(new FishFood("Aquatic Pellet Mix", 20, 3));
            shopInventory.AddItem(new FishFood("High-Protein Fish Pellets", 30, 5));

            // =====================
            // CAT TOYS
            // =====================
            shopInventory.AddItem(new CatToy("Feather Wand", 10, 10, 4));
            shopInventory.AddItem(new CatToy("Catnip Mouse", 15, 8, 5));
            shopInventory.AddItem(new CatToy("Laser Pointer", 25, 20, 10));

            // =====================
            // DOG TOYS
            // =====================
            shopInventory.AddItem(new DogToy("Rubber Bone", 15, 15, 5));
            shopInventory.AddItem(new DogToy("Tennis Ball", 10, 20, 3));
            shopInventory.AddItem(new DogToy("Rope Tug", 25, 12, 7));

            // =====================
            // FISH TOYS 
            // =====================
            shopInventory.AddItem(new FishToy("Colourful Gravel Maze", 10, 30, 6));
            shopInventory.AddItem(new FishToy("Bubble Ring Toy", 15, 40, 5));
            shopInventory.AddItem(new FishToy("Floating Mirror Toy", 25, 50, 10));

            // =====================
            // CAT MEDICINE
            // =====================

            shopInventory.AddItem(new CatMedicine("Feline Wound Spray", 15, 4));
            shopInventory.AddItem(new CatMedicine("Cat Health Tablets", 30, 7));
            shopInventory.AddItem(new CatMedicine("Cat Recovery Syrup", 60, 13));

            // =====================
            // DOG MEDICINE
            // =====================
            shopInventory.AddItem(new DogMedicine("Dog Recovery Syrup", 20, 5));
            shopInventory.AddItem(new DogMedicine("Canine Vitality Tonic", 35, 8));
            shopInventory.AddItem(new DogMedicine("Dog Recovery Injection", 70, 15));

            // =====================
            // FISH MEDICINE
            // =====================
            shopInventory.AddItem(new FishMedicine("Fish Recovery Formula ", 15, 3));
            shopInventory.AddItem(new FishMedicine("Fish Parasite Treatement", 30, 6));
            shopInventory.AddItem(new FishMedicine("Fish Healing Solution", 50, 10));
        }

    }
}
