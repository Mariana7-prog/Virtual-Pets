using System;
using Virtual_Pets.Classes.Pets;
using Virtual_Pets.Classes.Enums;

public static class PetFactory // this class is static because we don't need to create instances of it
{
    private static Random rng = new Random();

    private static string[] names = { "Buddy", "Luna", "Milo", "Bella", "Charlie", "Max" };
    private static string[] colours = { "Black", "White", "Brown", "Ginger", "Grey" };
    

    public static Pet CreateRandomPet() 
    {
        int choice = rng.Next(3); // 0 = Cat, 1 = Dog, 2 = Fish

        string name = names[rng.Next(names.Length)];
        string colour = colours[rng.Next(colours.Length)];
        int age = rng.Next(0, 16); // age range

        switch (choice)
        {
            case 0:
                return new Cat(name, colour, age);

            case 1:
                return new Dog(name, colour, age);

            case 2:
                return new Fish(name, colour, age);

            default:
                throw new Exception("Invalid pet type generated."); //should never reach here
        }
    }

    public static Pet CreateCustomPet(PetType type, string name, string colour, int age)
    {
        switch (type)
        {
            case PetType.Cat:
                return new Cat(name, colour, age);

            case PetType.Dog:
                return new Dog(name, colour, age);

            case PetType.Fish:
                return new Fish(name, colour, age);

            default:
                throw new Exception("Invalid pet type generated.");
        }
    }

    
}
