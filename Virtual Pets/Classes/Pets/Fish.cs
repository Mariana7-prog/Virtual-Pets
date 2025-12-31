using System.Drawing;
using System.Xml.Linq;
using Virtual_Pets.Interfaces;

namespace Virtual_Pets.Classes.Pets
{
    public class Fish : Pet, IAquatic
    {
        private const int MinPrefferedTemp = 22;
        private const int MaxPrefferedTemp = 28;
        public Fish(string name, string colour, int age)
            : base(name, Enums.PetType.Fish, colour, age, "blop", MinPrefferedTemp, MaxPrefferedTemp)
        {
        }
        public override string GetPetBody() // Overriding the virtual method from the base class
        {
            return @"       
       
           ,-.           ,.---'''^\                  O
          {   \       ,__\,---'''''`-.,      O    O
           I   \    K`,'^           _  `'.     o
           \  ,.J..-'`          // (O)   ,,X,    o
           /  (_               ((   ~  ,;:''`  o
          /   ,.X'.,            \\      ':;;;:
         (_../      -._                  ,'`
                     K.=,;.__ /^~/___..'`
                             /  /`
                             ~~~ ";
        }

        public void Swim()
        {
            Console.WriteLine("Swimming.");
        }
    }
}

