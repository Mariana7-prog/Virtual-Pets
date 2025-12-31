using Virtual_Pets.Interfaces;

namespace Virtual_Pets.Classes.Pets
{
    public class Dog : Pet, IWalkable
    {
        private const int MinPrefferedTemp = 18;
        private const int MaxPrefferedTemp = 24;

        public Dog(string name, string colour, int age)
            : base(name, Enums.PetType.Dog, colour, age, "woof", MinPrefferedTemp, MaxPrefferedTemp)
        {
        }

        public override string GetPetBody() // Overriding the virtual method from the base class
        {
            return @"       
              _
            ,/A\,
          .//`_`\\,
        ,//`____-`\\,
      ,//`[__HOME_]`\\,
    ,//`=  ==  __-  _`\\,
   //|__=  __- == _  __|\\
   ` |  __ .-----.  _  | `
     | - _/       \-   |
     |__  | .-""-. | __=|
     |  _=|/)   (\|    |
     |-__ (/ a a \) -__|
     |___ /`\_Y_/`\____|
          \)8===8(/
           ";
        }

        public void Walk()
        {
            Console.WriteLine("Going is going for a walk.");
        }
    }
}

