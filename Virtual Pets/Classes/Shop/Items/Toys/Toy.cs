using Virtual_Pets.Classes.Enums;

namespace Virtual_Pets.Classes.Shop.Items.Toys 
{
    public class Toy : Item
    {      
      public int funValue { get; set; }
      public int toyUses { get; set; }

        public Toy(string toyName, int playValue, int toyUses, int price) : base(toyName, price, ItemCategory.Toy)
        {
            this.toyUses = toyUses;
            this.funValue = playValue;
        }

        public bool CanUseToy()
        {
            if (toyUses > 0)
            {
                toyUses--;
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
