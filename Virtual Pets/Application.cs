using System.Xml.Linq;
using Virtual_Pets.Classes;
using Virtual_Pets.Classes.Enums;
using Virtual_Pets.Classes.Pets;
using Virtual_Pets.Classes.Rooms;
using Virtual_Pets.Classes.Shop;
using Virtual_Pets.Classes.Shop.Items;
using Virtual_Pets.Classes.Shop.Items.Medicine;
using Virtual_Pets.Classes.Shop.Items.Toys;
using Virtual_Pets.Items;

namespace Virtual_Pets
{
    internal class Application // internal class Application to run the main application
    {
        static Random rng = new Random(); // here i created a random number generator to be used in the application

        Player player;
        static Room room = new Room(rng.Next(10, 15));// here i created a room with a random ambient temperature between 10 and 15 degrees celsius
        Pet currentPet; // here i created a variable to hold the current pet
        private bool inGame = true; // here is a variable to control the main game loop
        private bool needsConsoleClear = false; // here is a variable to control when to clear the console

        string[] food = // here is the ascii art for food icon
        {
            "   .------.   ",
            "  /' .  '. \\  ",
            " (`-.FOOD.-') ",
            "  ;-......-;  ",
            "   '------'   "
        };

        string[] play = // here is the ascii art for play icon
        {
            "Let's play!",
            "  ___      ",
            "{~._.~}    ",
            " ( Y )     ",
            "()~*~()    ",
            "(_)-(_)    "
        };

        string[] sad = // here is the ascii art for sad icon
        {
            "Pet is sad!",
            " -    -    ",
            " O    O    ",
            "           ",
            " .------. ",
            "/        \\"
        };

        string[] temperature = // here is the ascii art for temperature icon
            {
    "┌─────────────┐",
    "│ CHECK TEMP  │",
    "│     :C      │",
    "└─────────────┘"        };

        string[] reset = // here is the ascii art for resetting the icon
        {
            "                   ",
            "                   ",
            "                   ",
            "                   ",
            "                   "
        };

        public void Run() // here is the main method to run the application
        {
            Shop.InitialShopSetup(); // here i call the initial shop setup method to populate the shop inventory

            Console.ForegroundColor = ConsoleColor.Red;
            Console.CursorVisible = false; // here the cursor visibility is set to false for better visual experience
            PrintCentered("Please play this maximised!");// here i added this message to ask the user to play the game  
            Thread.Sleep(1000);                          // maximised for a better visual experience
            Console.Clear();
            PrintCentered("Please play this maximised!!");
            Thread.Sleep(1000); // thread.sleep is used to pause the execution for a certain amount of time
            Console.Clear(); // here the console is getting cleared after each message
            PrintCentered("Please play this maximised!!!");
            Thread.Sleep(3000);
            Console.Clear();
            Console.CursorVisible = true; // here i set the cursor visibility to true again

            Console.ForegroundColor = ConsoleColor.White; // here i setted the console text back to white
            Console.Clear();
            InitialSetup(); // here i call the initial setup method to setup the player and pet


            Console.CursorVisible = false; // here the cursor visibility is set to false for better visual experience

            HandleThreads();

            while (inGame)
            {
                if (needsConsoleClear)
                {
                    Console.Clear();
                    needsConsoleClear = false;
                }
                RenderMainGame(); // here the reinder method is called to render the main game interface
                if(currentPet.IsPetDead())
                {
                    PetDies();
                }
            }

        }

        private void RenderMainGame() // here i created a method to show the main game interface
        {
            Console.SetCursorPosition(0, 0); // Set cursor position to the top-left corner
            DisplayRoomInfo(room);
            DisplayPlayerInfo();
            DisplayPetInfo(currentPet);
            Console.WriteLine("[1] Feed   [2] Play   [3] Give Medicine   [4] Heat Room   [5] Cool Room   [6] Shop   [7] Clear Console   [0] Exit");

            if (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey(true).Key; // true to not display the key

                switch (key) // here is a switch case to handle user input
                {
                    case ConsoleKey.D1:
                        {
                            FeedPet();
                            break;
                        }

                    case ConsoleKey.D2: // tbd
                        PlayWithPet();
                        break;
                    case ConsoleKey.D3: //tbd
                        UseMedicine();
                        break;
                    case ConsoleKey.D4:
                        room.WarmRoom(1);
                        break;
                    case ConsoleKey.D5:
                        room.CoolRoom(1);
                        break;
                    case ConsoleKey.D6:
                        Console.Clear();
                        Shop.shopping = true;
                        break;
                    case ConsoleKey.D7:
                        Console.Clear();
                        break;
                    case ConsoleKey.D0: // Here you exit the application
                        Environment.Exit(0);
                        break;
                }

                while (Shop.shopping)
                {
                    Console.Clear();
                    Shop.DisplayShopInventory(player);

                    ConsoleKey shopKey = Console.ReadKey(true).Key; // true to not display the key

                    switch (shopKey) // here is a switch case to handle user input
                    {
                        case ConsoleKey.D1: // here i call the Eat method from the Pet class
                            Shop.ShowCategory(ItemCategory.Food, player);
                            break;
                        case ConsoleKey.D2: // here i call the Play method from the Pet class
                            Shop.ShowCategory(ItemCategory.Toy, player);
                            break;
                        case ConsoleKey.D3:
                            Shop.ShowCategory(ItemCategory.Medicine, player);
                            break;
                        case ConsoleKey.D0: // Here you exit the shop
                            Console.Clear();
                            Shop.shopping = false;
                            break;
                    }
                }
            }
            DisplayPetStatusIndicators();

        }

        private void UseMedicine()
        {
            List<Item> allMedicineItems = player.GetInventory().GetItems(ItemCategory.Medicine);
            List<Medicine> validMedicine = new List<Medicine>();

            PetType petType = currentPet.GetPetType();

            // Filter medicine by pet type
            foreach (Item item in allMedicineItems)
            {
                if (petType == PetType.Cat && item is CatMedicine)
                    validMedicine.Add((Medicine)item);
                else if (petType == PetType.Dog && item is DogMedicine)
                    validMedicine.Add((Medicine)item);
                else if (petType == PetType.Fish && item is FishMedicine)
                    validMedicine.Add((Medicine)item);
            }

            if (validMedicine.Count == 0)
            {
                Console.WriteLine("You have no suitable medicine for this pet.");
                Console.ReadKey();
                needsConsoleClear = true;
                return;
            }

            Console.Clear();
            Console.WriteLine("Choose the medicine you want to use:");
            Console.WriteLine();

            for (int i = 0; i < validMedicine.Count; i++)
            {
                Console.WriteLine(
                    $"[{i + 1}] {validMedicine[i].GetName()} - healing value: {validMedicine[i].GetHealingValue()}"
                );
            }

            Console.WriteLine("[0] Cancel");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                needsConsoleClear = true;
                return;
            }

            if (choice == 0)
            {
                needsConsoleClear = true;
                return;
            }

            if (choice < 1 || choice > validMedicine.Count)
            {
                needsConsoleClear = true;
                return;
            }

            Medicine selectedMedicine = validMedicine[choice - 1];

            int healthBefore = currentPet.GetHealthValue();

            currentPet.TakeMedicine(selectedMedicine.GetHealingValue());
            allMedicineItems.Remove(selectedMedicine);

            Console.WriteLine(
                $"{currentPet.GetName()} was healed. Health was {healthBefore}, now {currentPet.GetHealthValue()}. Mood was reduced due to taking the medicine."
            );

            Console.ReadKey();
            needsConsoleClear = true;
        }


        private void PlayWithPet()
        {
            List<Item> allToyItems = player.GetInventory().GetItems(ItemCategory.Toy);
            List<Toy> validToys = new List<Toy>();

            PetType petType = currentPet.GetPetType();

            // Filter toys by pet type
            foreach (Item item in allToyItems)
            {
                if (petType == PetType.Cat && item is CatToy)
                    validToys.Add((Toy)item);
                else if (petType == PetType.Dog && item is DogToy)
                    validToys.Add((Toy)item);
                else if (petType == PetType.Fish && item is FishToy)
                    validToys.Add((Toy)item);
            }

            if (validToys.Count == 0)
            {
                Console.WriteLine("You have no suitable toys for this pet.");
                Console.ReadKey();
                needsConsoleClear = true;
                return;
            }

            Console.Clear();
            Console.WriteLine("Choose a toy to play with:");
            Console.WriteLine();

            for (int i = 0; i < validToys.Count; i++)
            {
                Console.WriteLine(
                    $"[{i + 1}] {validToys[i].GetName()} - Fun: {validToys[i].funValue}, Uses left: {validToys[i].toyUses}"
                );
            }

            Console.WriteLine("[0] Cancel");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                needsConsoleClear = true;
                return;
            }

            if (choice == 0)
            {
                needsConsoleClear = true;
                return;
            }

            if (choice < 1 || choice > validToys.Count)
            {
                needsConsoleClear = true;
                return;
            }

            Toy selectedToy = validToys[choice - 1];

            int moodBefore = currentPet.GetMoodValue();

            currentPet.Play(selectedToy);

            if (selectedToy.toyUses <= 0)
            {
                allToyItems.Remove(selectedToy);
                Console.WriteLine($"{selectedToy.GetName()} broke and was removed.");
            }

            Console.WriteLine(
                $"{currentPet.GetName()} played happily. Mood was {moodBefore}, now {currentPet.GetMoodValue()}."
            );

            Console.ReadKey();
            needsConsoleClear = true;
        }

        private void FeedPet()
        {
            List<Item> allFoodItems = player.GetInventory().GetItems(ItemCategory.Food);
            List<Food> validFood = new List<Food>();

            PetType petType = currentPet.GetPetType();

            // Filter food by pet type
            foreach (Item item in allFoodItems)
            {
                if (petType == PetType.Cat && item is CatFood)
                    validFood.Add((Food)item);
                else if (petType == PetType.Dog && item is DogFood)
                    validFood.Add((Food)item);
                else if (petType == PetType.Fish && item is FishFood)
                    validFood.Add((Food)item);
            }

            if (validFood.Count == 0)
            {
                Console.WriteLine("You have no suitable food for this pet.");
                return;
            }

            Console.Clear();
            Console.WriteLine("Choose the food you want to use:");
            Console.WriteLine();

            for (int i = 0; i < validFood.Count; i++)
            {
                Console.WriteLine(
                    $"[{i + 1}] {validFood[i].GetName()} - nutritional value: {validFood[i].GetNutritionValue()}"
                );
            }

            Console.WriteLine("[0] Cancel");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                needsConsoleClear = true;
                return;
            }

            if (choice == 0)
            {
                needsConsoleClear = true;
                return;
            }

            if (choice < 1 || choice > validFood.Count)
            {
                needsConsoleClear = true;

                return;
            }

            int initialHunger = currentPet.GetHungerValue();
            Food selectedFood = validFood[choice - 1];
            currentPet.Eat(selectedFood);
            allFoodItems.Remove(selectedFood);

            Console.WriteLine($"{currentPet.GetName()} ate {selectedFood.GetName()}. Pet hunger was {initialHunger} before eating. It is now {currentPet.GetHungerValue()}");
            Console.ReadKey();
            needsConsoleClear = true;
            return;
        }

        private void DisplayPetStatusIndicators()
        {
            if (currentPet.IsPetHungry()) // if pet is hungry show food icon
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                DrawAtBottom(food, 0);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                DrawAtBottom(reset, 0);
            }
            if (!currentPet.IsPetHappy()) // if pet is sad show sad icon
            {
                Console.ForegroundColor = ConsoleColor.Green;
                DrawAtBottom(play, 25);
                Console.ForegroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                DrawAtBottom(sad, 50);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                DrawAtBottom(reset, 25);
                DrawAtBottom(reset, 50);
            }
            if (!currentPet.IsTemperatureComfortable(room.GetCurrentTemperature())) // if pet is in uncomfortable temperature show temperature icon
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                DrawAtBottom(temperature, 70);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                DrawAtBottom(reset, 70);
            }
        }

        private void HandleThreads() // here i created a method to handle threads
        {
            Thread updatePetStatusThread = new Thread(() => UpdatePetStatus(currentPet));
            updatePetStatusThread.Start();

            Thread updateRoomTemperature = new Thread(() =>
            {
                while (true)
                {
                    room.UpdateRoomTemperature(1);
                    Thread.Sleep(5000); // Update room temperature every 5 seconds
                }
            });
            updateRoomTemperature.Start(); // start the thread to update room temperature

            Thread increaseCoins = new Thread(() =>
            {
                while (true) // here is a while loop to increase 1 coin every 2 seconds
                {
                    if (!Shop.shopping)
                    {
                        player.EarnCoins(1); // Increase coins by 1
                        Thread.Sleep(2000); // every 2 seconds
                    }
                }
            });
            increaseCoins.Start(); // here the thread starts
        }

        private void DisplayPlayerInfo() // here i created a method to display player info
        {
            Console.Write($"Player: {player.GetName(),-6} "); // here we display player name and reserve 6 spaces because you need to overwrite previous text
            Console.WriteLine($"|| Coins: {player.GetBalance(),-2}"); // here we display player coins and reserve 2 spaces
        }

        void DrawAtBottom(string[] art, int xOffset)
        {
            int startRow = Console.WindowHeight - art.Length - 1;

            for (int i = 0; i < art.Length; i++)
            {
                Console.SetCursorPosition(xOffset, startRow + i);
                Console.Write(art[i]);
            }
        }

        void InitialSetup()
        {
            Console.WriteLine(@"
====================================
        VIRTUAL PET SIMULATION
====================================
2025 MOD008915              2412887

");
            Console.WriteLine("Enter your name.");
            player = new Player(Console.ReadLine(), 50);
            Console.Clear();
            Console.WriteLine("Chose your starting pet type.");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("You will be able to change your pet and use multiple pets at once as you progress in the game and earn more coins.");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(
                "1 - generate a random pet.\n" +
                "2 - create a custom pet.\n");

            int choice = GetUserInput<int>();
            while (choice < 1 || choice > 2)
            {
                Console.WriteLine("Invalid choice. Please select 1 or 2.");
                choice = GetUserInput<int>();
            }
            if (choice == 1)
            {
                player.AddPet(GenerateRandomPet());
            }
            else if (choice == 2)
            {
                player.AddPet(CreateYourOwnPet());
            }
            currentPet = player.GetPets()[0];
            Console.Clear();
        }

        private Pet CreateYourOwnPet()
        {
            int choice;
            Console.WriteLine("Chose your pet type.");
            foreach (var petType in Enum.GetValues(typeof(PetType)))
            {
                Console.WriteLine($"{(int)petType + 1} - {petType}");
            }
            choice = GetUserInput<int>();
            PetType selectedPetType = (PetType)(choice - 1);

            string petName = GetUserInput<string>("Enter your pet's name: ");
            string colour = GetUserInput<string>("Enter your pet's colour: ");
            int age = GetUserInput<int>("Enter your pet's age: ");
            return PetFactory.CreateCustomPet(selectedPetType, petName, colour, age);
        }

        Pet GenerateRandomPet()
        {
            Pet pet = PetFactory.CreateRandomPet();
            return pet;
        }

        void UpdatePetStatus(Pet pet)
        {
            while (true)
            {
                pet.UpdateStats(room.GetCurrentTemperature());
                Thread.Sleep(100); // Update stats every x seconds
            }
        }

        void DisplayPetInfo(Pet pet) // here i created a method to display pet info
        {
            Console.WriteLine("______________________________________");
            Console.WriteLine(pet.GetPetBody());
            pet.DisplayInfo();
            //pet.Eat();
            //pet.Sleep();
            //pet.Play(); 
            Console.WriteLine("______________________________________\n");
        }

        void DisplayRoomInfo(Room room)
        {
            Console.Write($"Current Room Temperature: {room.GetCurrentTemperature()}°C");
            Console.WriteLine($" || Ambient: {room.GetAmbientTemperature()}°C    ");
        }

        private T GetUserInput<T>() // Generic method to get user input and convert it to the specified type
        {
            while (true)
            {
                string input = Console.ReadLine();

                try
                {
                    // Convert input to the requested type
                    return (T)Convert.ChangeType(input, typeof(T));
                }
                catch
                {
                    Console.WriteLine($"Invalid input. Please enter a valid {typeof(T).Name}.");
                }
            }
        }

        private T GetUserInput<T>(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                try
                {
                    // Convert input to the requested type
                    return (T)Convert.ChangeType(input, typeof(T));
                }
                catch
                {
                    Console.WriteLine($"Invalid input. Please enter a valid {typeof(T).Name}.");
                }
            }
        }

        static void PrintCentered(string text)
        {
            int x = (Console.WindowWidth - text.Length) / 2;
            int y = Console.WindowHeight / 2;

            if (x < 0) x = 0;

            Console.SetCursorPosition(x, y);
            Console.WriteLine(text);
        }



        public void PetDies() //  here i created a method to handle pet death
        {
            inGame = false;
            Console.Clear(); // here is gonna clean all my console before printing the death message
            PrintCentered(@" Your pet sadly died. Lord have mercy on its soul.
                         _..._
                       .-|>X<|-.
                     _//`|oxo|`\\_  
                    /xo=._\X/_.=ox\
                    |<>X<>(_)<>X<>|
                    \xo.='/X\'=.ox/
                      \\_/oxo\_//
                       ';<>X<>;'
                        |=====|
                        |<>X<>|
                        |oxoxo|
                        |<>X<>|
                       _|oxoxo|_
                RIP.--' `""""""""""` '--.");
            Thread.Sleep(3000);
            Environment.Exit(0);

        }
    }
}
