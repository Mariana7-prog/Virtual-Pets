using Virtual_Pets.Classes.Enums;

namespace Virtual_Pets.Classes.Shop.Items.Medicine
{
    internal class Medicine: Item
    {
        int healingValue { get; set; }
        public Medicine(string medicineName, int healingValue, int price) : base(medicineName, price, ItemCategory.Medicine)
        {
            this.healingValue = healingValue; 
        }
        public int GetHealingValue()
        {
            return healingValue;
        }
    }
}
