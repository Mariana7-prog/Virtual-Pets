using Microsoft.VisualStudio.TestTools.UnitTesting;
using Virtual_Pets.Classes;
using Virtual_Pets.Classes.Enums;
using Virtual_Pets.Classes.Pets;
using Virtual_Pets.Classes.Shop.Items.Toys;
using Virtual_Pets.Items;


namespace Virtual_Pets.Testing
{
    [TestClass]
    public sealed class Behavior_Tests
    {
        [TestMethod]
        public void EatingFood_ReducesHunger()
        {
            // Arrange
            Pet pet = new Dog("Buddy", "Brown", 3);
            Food food = new DogFood("Dog Food", 20, 10);

            pet.IncreaseHunger(50);
            int hungerBefore = pet.GetHungerValue();

            // Act
            pet.Eat(food);

            // Assert
            Assert.IsTrue(pet.GetHungerValue() < hungerBefore);
        }

        [TestMethod]
        public void PlayingWithToy_IncreasesMood()
        {
            // Arrange
            Pet pet = new Cat("Whiskers", "Black", 2);
            Toy toy = new CatToy("Cat Toy", 15, 10, 9);
            pet.DecreaseMood(40);
            int moodBefore = pet.GetMoodValue();
            // Act
            pet.Play(toy);
            // Assert
            Assert.IsTrue(pet.GetMoodValue() > moodBefore);
        }

        [TestMethod]

        public void TakingMedicine_IncreasesHealth()

        {
            // Arrange
            Pet pet = new Fish("Bubble", "Yellow", 3);
            pet.DecreaseHealth(30);
            int healthBefore = pet.GetHealthValue();
            // Act
            pet.TakeMedicine(20);
            // Assert
            Assert.IsTrue(pet.GetHealthValue() > healthBefore);


        }

        [TestMethod]

        public void TakingMedicine_DecreaseMood()
        {
            // Arrange
            Pet pet = new Dog("Nero", "Black", 6);
            int moodBefore = pet.GetMoodValue();
            // Act
            pet.TakeMedicine(20);
            // Assert
            Assert.IsTrue(pet.GetMoodValue() < moodBefore);




        }

        [TestMethod]

        public void TakingMedicine_IncreaseHunger()
        {
            // Arrange
            Pet pet = new Cat("Luna", "White", 4);
            int hungerBefore = pet.GetHungerValue();
            // Act
            pet.TakeMedicine(20);
            // Assert
            Assert.IsTrue(pet.GetHungerValue() > hungerBefore);
        }

        [TestMethod]

        public void Health_Decreases_WhenHungerIsHigh()
        {
            // Arrange
            Pet pet = new Dog("Max", "Golden", 5);
            pet.IncreaseHunger(80);
            int healthBefore = pet.GetHealthValue();
            // Act
            pet.UpdateStats(20); // here we call UpdateStats to simulate time passing
            // Assert
            Assert.IsTrue(pet.GetHealthValue() < healthBefore);
        }

        [TestMethod]
        public void TemperatureOutside_PrefferedRange_IsUncomfortable()
        {
            // Arrange
            Pet pet = new Cat("Luna", "White", 3);

            // Act
            bool comfortable = pet.IsTemperatureComfortable(5);

            // Assert
            Assert.IsFalse(comfortable);
        }

        [TestMethod]

        public void TemperatureInside_PrefferedRange_IsComfortable()
        {
            // Arrange
            Pet pet = new Fish("Goldie", "Gold", 1);
            // Act
            bool comfortable = pet.IsTemperatureComfortable(22);
            // Assert
            Assert.IsTrue(comfortable);
        }

        [TestMethod]
        public void Mood_DoesNotExceed_Maximum()
        {
            // Arrange
            Pet pet = new Dog("Rex", "Brown", 4);
            // Act
            pet.IncreaseMood(150);
            // Assert
            Assert.AreEqual(100, pet.GetMoodValue());
        }

        [TestMethod]
        public void Hunger_DoesNotGoBelow_Zero()
        {
            // Arrange
            Pet pet = new Cat("Mittens", "Gray", 2);
            // Act
            pet.ReduceHunger(50);
            // Assert
            Assert.AreEqual(0, pet.GetHungerValue());

        }

        [TestMethod]

        public void PetDies_WhenHealthReachesZero()
        {
            // Arrange
            Pet pet = new Fish("Nemo", "Orange", 2);
            // Act
            pet.DecreaseHealth(100);
            // Assert
            Assert.IsTrue(pet.IsPetDead());
        }

        [TestMethod]

        public void BuyingItem_ReducesPlayerBalance()
        {
            // Arrange
            Player player = new Player("Mariana", 100);
            int balanceBefore = player.GetBalance();

            // Act
            bool result = player.SpendCoins(30);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(balanceBefore - 30, player.GetBalance());
        }

        [TestMethod]
        public void BuyingItem_Fails_WhenInsufficientFunds()
        {
            // Arrange
            Player player = new Player("Alex", 20);
            int balanceBefore = player.GetBalance();
            // Act
            bool result = player.SpendCoins(50);
            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(balanceBefore, player.GetBalance());
        }

        [TestMethod]

        public void BuyingItem_AddsItemToInventory()
        {
            // Arrange
            Player player = new Player("Sophia", 100);
            Toy toy = new CatToy("Wand ", 20, 15, 5);

            // Act
            bool purchaseResult = player.SpendCoins(toy.GetPrice());
            if (purchaseResult)
            {
                player.GetInventory().AddItem(toy);
            }

            // Assert
            Assert.IsTrue(purchaseResult);
            Assert.IsTrue(player.GetInventory()
                .GetItems(ItemCategory.Toy)
                .Contains(toy));
        }
    }
}

