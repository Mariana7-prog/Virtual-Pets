using Virtual_Pets.Classes.Enums;
using Virtual_Pets.Classes.Shop.Items;
using Virtual_Pets.Classes.Shop.Items.Medicine;
using Virtual_Pets.Classes.Shop.Items.Toys;
using Virtual_Pets.Items;

namespace Virtual_Pets.Classes.Shop
{
    public class Inventory
    {
        Dictionary<ItemCategory, List<Item>> ownedItems = new Dictionary<ItemCategory, List<Item>>
        {
            { ItemCategory.Toy, new List<Item>() },
            { ItemCategory.Food, new List<Item>() },
            { ItemCategory.Medicine, new List<Item>()}
        };

        public List<Item> GetItems(ItemCategory itemsType)
        {
            return ownedItems[itemsType];
        }

        public void AddItem(Item item) // Method to add an item to the inventory
        {
            ownedItems[item.itemCategory].Add(item); // Add item to the appropriate category list
        }

        public void RemoveItem(Item item) // Method to remove an item from the inventory
        {
            ownedItems[item.itemCategory].Remove(item); // Remove item from the appropriate category list
        }
    }
}
