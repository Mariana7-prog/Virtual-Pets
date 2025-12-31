using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Virtual_Pets.Classes.Enums;

namespace Virtual_Pets.Classes.Shop.Items
{
    public partial class Item
    {
        int Price { get; set; }
        string Name { get; set; }
        public ItemCategory itemCategory { get; set; }
        public Item(string name, int price, ItemCategory itemCategory) // Constructor
        {
            Name = name;
            Price = price;
            this.itemCategory = itemCategory;
        }
        public int GetPrice() //here is method to get the price of the item
        {
            return Price;
        }

        public string GetName() // here is method to get the name of the item
        {
            return Name;
        }

        public Item Clone() // here is method is used to clone items because otherwise reference types would point to the same object in memory
        {
            return (Item)this.MemberwiseClone(); // here we return a shallow copy of the current item
        }
    }
}
