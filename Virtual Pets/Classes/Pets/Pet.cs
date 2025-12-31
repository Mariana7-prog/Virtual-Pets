using Virtual_Pets.Classes.Enums;
using Virtual_Pets.Classes.Shop.Items.Toys;
using Virtual_Pets.Interfaces;
using Virtual_Pets.Items;

namespace Virtual_Pets.Classes.Pets
{
    public class Pet : IPet // Base class for all pets
    {
        private string Name { get; set; }
        private PetType PetType { get; set; }
        private string Colour { get; set; }
        private double Age { get; set; }
        private int Health { get; set; } // here is the health level of the pet
        private string Sound { get; set; }
        private int MoodValue { get; set; } // here is the mood level of the pet
        private int Hunger { get; set; } // here is the mood level of the pet
        Dictionary<string, int> prefferedTemp = new Dictionary<string, int>() // dictionary to hold min and max preffered temperature
        {
            {"minTemp", 0}, //you call the value of min temp with prefferedTemp["minTemp"]
            {"maxTemp", 0}
        };

        //Constructor
        public Pet(string name, PetType petType, string colour, int age, string sound, int minPrefferedTemp, int maxPrefferedTemp)
        {
            Name = name;
            PetType = petType;
            Colour = colour;
            Age = age;
            Health = 100;
            Sound = sound;
            MoodValue = 100;
            Hunger = 0;
            prefferedTemp["minTemp"] = minPrefferedTemp;
            prefferedTemp["maxTemp"] = maxPrefferedTemp;
        }

        // Methods
        public int GetMinPrefferedTemp()
        {
            return prefferedTemp["minTemp"];
        }

        public void TakeMedicine(int medicineValue)
        {
            IncreaseHealth(medicineValue);
            DecreaseMood(medicineValue / 2); // taking medicine makes pet a bit sad
            IncreaseHunger(medicineValue / 2); // taking medicine makes pet a bit hungry
        }

        public int GetMaxPrefferedTemp()
        {
            return prefferedTemp["maxTemp"];
        }

        public void Eat(Food food)
        {
            Hunger -= food.GetNutritionValue();
            if(Hunger <= 0)
            {
                Hunger = 0;
            }
        }
        public void Sleep()
        {
            Console.WriteLine($"{Name} is sleeping.");
        }
        public void Play(Toy toy)
        {
            if(toy.CanUseToy())
            {
                MoodValue += toy.funValue;
                if (MoodValue > 100)
                {
                    MoodValue = 100;
                }
                Console.WriteLine($"{Name} is playing with {toy.GetName()}.");
            }
            else
            {
                Console.WriteLine($"{toy.GetName()} cannot be used anymore.");
            }
        }

        public bool IsPetDead()
        {
            if (Health <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void MakeSound()
        {
            Console.WriteLine($"{Name} is {Sound}.");
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Name: {Name} || Breed: {PetType} || Age: {Age} || Mood: {GetMoodStatus(),-5} || Hunger : {GetHungerStatus(),-10} || Health : {Health,-10}");
        }

        public virtual string GetPetBody() // Virtual method to be overridden by derived classes
        {
            return "";
        }

        string GetHungerStatus() // here is a method to get the hunger status of the pet , its private because its only used within the class
        {
            if (Hunger <= 30)
            {
                return "Not Hungry";
            }
            else if (Hunger <= 70)
            {
                return "Hungry";
            }
            else
            {
                return "Very Hungry";
            }
        }

        public int GetHealthValue()
        {
            return Health;
        }

        public void DecreaseHealth(int amount) // here is a method to decrease the health of the pet
        {
            Health -= amount;
            if (Health < 0)
            {
                Health = 0;
            }
        }

        public void IncreaseHealth(int amount) // here is a method to increase the health of the pet
        {
            Health += amount;
            if (Health > 100)
            {
                Health = 100;
            }
        }

        public void UpdateStats(int roomTemp) // here is a method to update the pet's stats over time
        {
            IncreaseHunger(1); // here is a method to increase the hunger of the pet over time
            DecreaseHealthBasedOnHunger();

            DecreaseMood(1); // here is a method to decrease the mood of the pet over time
            UpdateHungerBasedOnHealth(); // here is a method to update the hunger based on health
            UpdateStatsBasedOnRoomTemp(roomTemp); // here is a method to update the pet's stats based on room temperature
        }

        private void DecreaseHealthBasedOnHunger()
        {
            if (Hunger > 70)
            {
                DecreaseHealth(1); // decrease health if hunger is above 70
            }
        }

     

        public string GetName()
        {
            return Name;
        }

        public bool IsPetHungry() // here is a method to check if the pet is hungry
        {
            return Hunger > 70; // Pet is considered hungry if hunger level is above 70
        }

        public bool IsPetHappy() // here is a method to check if the pet is happy
        {
            return MoodValue > 70; // Pet is considered happy if mood level is above 70
        }

        public int GetMoodValue()
        {
            return MoodValue;
        }

        public bool IsTemperatureComfortable(int roomTemperature)
        {
            return roomTemperature >= prefferedTemp["minTemp"] && roomTemperature <= prefferedTemp["maxTemp"];
        }

        public void UpdateStatsBasedOnRoomTemp(int roomTemperature)
        {
            if (roomTemperature >= prefferedTemp["minTemp"] && roomTemperature <= prefferedTemp["maxTemp"]) //if room temp is within preffered range
            {
                IsTemperatureComfortable(roomTemperature);
                IncreaseMood(1);
            }
            else if (roomTemperature < prefferedTemp["minTemp"]) //if room temp is below preffered range
            {
                DecreaseHealth(1); // decrease health and mood if temp is not comfortable
                DecreaseMood(1); // decrease health and mood if temp is not comfortable
            }
            else if (roomTemperature > prefferedTemp["maxTemp"]) //if room temp is above preffered range
            {
                DecreaseHealth(1); // decrease health and mood if temp is not comfortable
                DecreaseMood(1); // decrease health and mood if temp is not comfortable
            }
        }

        public void IncreaseMood(int amount) // here is a method to increase the mood of the pet
        {
            MoodValue += amount; // increase mood by specified amount
            if (MoodValue > 100) // cap mood at 100
            {
                MoodValue = 100;// cap mood at 100
            }
        }

        private string GetMoodStatus() // here is a method to get the mood status of the pet
        {
            if (MoodValue >= 70) // if mood is 70 or above
            {
                return "Happy"; // return happy
            }
            else if (MoodValue >= 40) // if mood is between 40 and 69
            {
                return "Neutral"; // return neutral
            }
            else
            {
                return "Sad"; // return sad
            }
        }

        public void DecreaseMood(int amount) // here is a method to decrease the mood of the pet
        {
            MoodValue -= amount;// decrease mood by specified amount
            if (MoodValue < 0)// cap mood at 0
            {
                MoodValue = 0;// cap mood at 0
            }
        }

        public void ReduceHunger(int amount) // here is a method to decrease the hunger of the pet
        {
            Hunger -= amount; // decrease hunger by specified amount
            if (Hunger < 0) // cap hunger at 0
            {
                Hunger = 0; // cap hunger at 0
            }
        }

        public void IncreaseHunger(int amount) // here is a method to increase the hunger of the pet
        {
            Hunger += amount; // increase hunger by specified amount
            if (Hunger > 100) // cap hunger at 100
            {
                Hunger = 100; // cap hunger at 100
            }
        }

        public void UpdateHungerBasedOnHealth() // here is a method to update the hunger based on health
        {
            if (Health < 30) // if health is below 30
            {
                IncreaseHunger(3); // Increase hunger faster if health is low
            }
            else
            {
                IncreaseHunger(1); // Normal hunger increase
            }
        }

        public PetType GetPetType() // here is a method to get the pet type 
        {
            return PetType; // return the pet type

        }

        public int GetHungerValue() // here is a method to get the hunger value
        {
            return Hunger; // return the hunger value
        }

       
    }
}

