using Virtual_Pets.Classes.Enums;
using Virtual_Pets.Classes.Shop.Items;

namespace Virtual_Pets.Items
{
    public partial class Food : Item
    {
        int nutritionValue { get; set; }

        public Food(string foodName,  int nutritionValue, int price) : base(foodName, price, ItemCategory.Food)
        {
            this.nutritionValue = nutritionValue;
        }

        public int GetNutritionValue()
        {
            return nutritionValue;
        }
    }
}
