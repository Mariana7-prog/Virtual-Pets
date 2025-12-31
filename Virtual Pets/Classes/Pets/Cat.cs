using Virtual_Pets.Interfaces;

namespace Virtual_Pets.Classes.Pets
{
    public class Cat : Pet, IWalkable
    {
        private int Lives { get; set; } = 9; // Cats have 9 lives
        private const int MinPrefferedTemp = 20;
        private const int MaxPrefferedTemp = 26;
        public Cat(string name, string colour, int age) 
            : base(name, Enums.PetType.Cat, colour, age, "meow", MinPrefferedTemp, MaxPrefferedTemp) // Calling the base class constructor
        {

        }
        public override string GetPetBody() // Overriding the virtual method from the base class
        {
            return @"                
                \`*-.                   
                 )  _`-.                
                .  : `. .               
                : _   '  \              
                ; *` _.   `*-._         
                `-.-'          `-.      
                  ;       `       `.    
                  :.       .        \   
                  . \  .   :   .-'   .  
                  '  `+.;  ;  '      :  
                  :  '  |    ;       ;-.
                  ; '   : :`-:     _.`* ;
                .*' /  .*' ; .*`- +'  `*'
               `*-*   `*-*  `*-*'       
";
        }

        public void Walk()
        {
            Console.WriteLine("Going for a walk.");
        }
    }
}

