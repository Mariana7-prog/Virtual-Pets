using Virtual_Pets.Classes.Shop.Items.Toys;
using Virtual_Pets.Items;

namespace Virtual_Pets.Interfaces
{
    internal interface IPet
    {

        void Eat(Food food);
        void Sleep();
        void Play(Toy toy);
        void  MakeSound();

    }
}
