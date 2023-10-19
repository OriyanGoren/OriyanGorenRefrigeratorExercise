using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace RefrigeratorEx
{
    class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            Refrigerator refrigerator1 = new Refrigerator("Samsung Family Hub", "Black", 4);
            Refrigerator refrigerator2 = new Refrigerator("Samsung Family Hub", "Black", 4);
            program.DataInitialize(refrigerator1, refrigerator2);
            program.MenuOfRefrigeratorGame(refrigerator1);
        }

        public void DataInitialize(Refrigerator refrigerator1, Refrigerator refrigerator2)
        {
            Item item1 = new Item("Milk", Item.Type.Drink, Item.Kosher.Dairy, new DateTime(2023, 10, 15), 7);
            Item item2 = new Item("Pizza", Item.Type.Food, Item.Kosher.Dairy, new DateTime(2023, 10, 20), 14);
            Item item3 = new Item("Chicken", Item.Type.Food, Item.Kosher.Meat, new DateTime(2023, 10, 21), 13);
            Item item4 = new Item("Fish", Item.Type.Food, Item.Kosher.Parve, new DateTime(2023, 10, 22), 8);
            Item item5 = new Item("Pasta", Item.Type.Food, Item.Kosher.Dairy, new DateTime(2023, 10, 20), 15);
            Item item6 = new Item("Rice", Item.Type.Food, Item.Kosher.Parve, new DateTime(2023, 10, 23), 10);
            Item item7 = new Item("Juice", Item.Type.Drink, Item.Kosher.Parve, new DateTime(2023, 10, 24), 8);
            Item item8 = new Item("Bean", Item.Type.Food, Item.Kosher.Parve, new DateTime(2023, 10, 25), 8);
            refrigerator1.PutItemToFridge(item1);
            refrigerator1.PutItemToFridge(item2);
            refrigerator1.PutItemToFridge(item3);
            refrigerator1.PutItemToFridge(item4);
            refrigerator1.PutItemToFridge(item5);
            refrigerator2.PutItemToFridge(item6);
            refrigerator2.PutItemToFridge(item7);
            refrigerator2.PutItemToFridge(item8);
        }

        private void MenuOfRefrigeratorGame(Refrigerator refrigerator)
        {
            bool isRunning = true;
            while (isRunning)
            {
                ShowingButtonsToUser();
                PlayingGameByUser(ref isRunning, refrigerator);
            }
            Console.WriteLine("System shut down.");
        }

        private void ShowingButtonsToUser()
        {
            Console.WriteLine();    //So that the console looks better
            Console.WriteLine("Menu:");
            Console.WriteLine("1: Print all the items inside the refrigerator and all its contents.");
            Console.WriteLine("2: Print remaining space in the fridge");
            Console.WriteLine("3: Put an item in the fridge");
            Console.WriteLine("4: Remove an item from the fridge");
            Console.WriteLine("5: Clean the refrigerator");
            Console.WriteLine("6: What do I want to eat?");
            Console.WriteLine("7: Print items sorted by expiration date");
            Console.WriteLine("8: Print shelves by free space");
            Console.WriteLine("9: Print refrigerators by free space");
            Console.WriteLine("10: Prepare the refrigerator for shopping");
            Console.WriteLine("100: Shutdown");
        }

        private void PlayingGameByUser(ref bool isRunning, Refrigerator refrigerator)
        {
            int choice;
            Console.WriteLine();
            Console.Write("Enter your choice: ");
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine();
                OptionsInGame(choice, ref isRunning, refrigerator);
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }

        private void OptionsInGame(int choice, ref bool isRunning, Refrigerator refrigerator)
        {
            switch (choice)
            {
                case 1:
                    DoOption1(refrigerator);
                    break;
                case 2:
                    int FreeSpaceInFridge = refrigerator.FreeSpaceInFridge();
                    Console.WriteLine($"Free space in the fridge: '{FreeSpaceInFridge}' square centimete");
                    break;
                case 3:
                    Item item1 = InputDataOfItemInsertion();
                    refrigerator.PutItemToFridge(item1);
                    break;
                case 4:
                    int ID = InputDataOfItemTakeOut();
                    refrigerator.RemovingItemFromFridge(ID);
                    break;
                case 5:
                    refrigerator.CleaningTheFridge();
                    break;
                case 6:
                    DoOption6(refrigerator);
                    break;
                case 7:
                    DoOption7(refrigerator);
                    break;
                case 8:
                    DoOption8(refrigerator);
                    break;
                case 9:
                    DoOption9(refrigerator);
                    break;
                case 10:
                    refrigerator.GettingReadyForShopping();
                    break;
                case 100:
                    isRunning = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        private void DoOption1(Refrigerator refrigerator)
        {
            if (refrigerator.ToString() != null)
            {
                Console.WriteLine(refrigerator.ToString());
            }
            else
            {
                Console.WriteLine("Sorry but there are no items in the fridge yet, so we have nothing to display. To put items in the fridge press 3 please.");
            }
        }

        private void DoOption6(Refrigerator refrigerator)
        {
            int kosherInput = 0;
            int typeInput = 0;
            GetUserInput<Item.Kosher>(ref kosherInput, "Enter kosher type (0 for Dairy, 1 for Meat, 2 for Parve): ");
            GetUserInput<Item.Type>(ref typeInput, "Enter food type (0 for Food, 1 for Drink): ");
            List<Item> foodIWillEat = refrigerator.IWantToEat((Item.Kosher)kosherInput, (Item.Type)typeInput);
            if(foodIWillEat.Count != 0)
            {
                foreach (var item in foodIWillEat)
                {
                    Console.WriteLine(item);
                }
            }
            else
            {
                Console.WriteLine("Sorry, but we have no suitable products for the options you entered.");
            }
        }

        private void DoOption7(Refrigerator refrigerator)
        {
            List<Item> sortedItems = refrigerator.SortProductsByExpirationDate();
            foreach (var item in sortedItems)
            {
                Console.WriteLine(item);
            }
        }

        private void DoOption8(Refrigerator refrigerator)
        {
            List<Shelf> sortedShelves = refrigerator.SortShelvesByFreeSpace();
            foreach (var shelf in sortedShelves)
            {
                Console.WriteLine(shelf);
            }
        }

        private void DoOption9(Refrigerator refrigerator)
        {
            List<Refrigerator> sortedFridges = Refrigerator.SortRefrigeratorsByFreeSpace();
            foreach (var fridge in sortedFridges)
            {
                Console.WriteLine(fridge);
            }
        }

        private Item InputDataOfItemInsertion()
        {
            int kosher = 0, type = 0, spaceItem = 0; ;
            string name = "";
            DateTime expiryDate = DateTime.Today;

            CheckName(ref name);
            GetUserInput<Item.Type>(ref type, "Enter food type (0 for Food, 1 for Drink): ");
            GetUserInput<Item.Kosher>(ref kosher, "Enter kosher type (0 for Dairy, 1 for Meat, 2 for Parve): ");
            CheckDate(ref expiryDate);
            CheckSpaceOccupied(ref spaceItem);

            Item newItem = new Item(name, (Item.Type)type, (Item.Kosher)kosher, expiryDate, spaceItem);
            return newItem;
        }

        private void CheckName(ref String name)
        {
            while (true)
            {
                Console.WriteLine("Enter item name:");
                name = Console.ReadLine();
                if (IsAllLetters(name))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input for item name. Please enter letters only.");
                }
            }
        }

        private bool IsAllLetters(string input)
        {
            return input.All(char.IsLetter);
        }

        private void CheckDate(ref DateTime expiryDate)
        {
            while (true)
            {
                Console.WriteLine("Enter expiry date (MM/DD/YYYY):");
                try
                {
                    expiryDate = DateTime.ParseExact(Console.ReadLine(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid date format. Please enter the date in MM/DD/YYYY format.");
                    Console.WriteLine("For example, 10/19/2023 for October 19, 2023.");
                }
            }
        }

        private void CheckSpaceOccupied(ref int spaceItem)
        {
            while (true)
            {
                Console.WriteLine("Enter space occupied by the item:");
                string spaceInput = Console.ReadLine();
                try
                {
                    spaceItem = int.Parse(spaceInput);
                    if (spaceItem <= 0)
                    {
                        throw new FormatException();
                    }
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input for space. Please enter a positive integer.");
                }
            }
        }

        private void GetUserInput<TEnum>(ref int userInput, string prompt) where TEnum : Enum
        {
            while (true)
            {
                Console.WriteLine(prompt);
                if (int.TryParse(Console.ReadLine(), out int userInputInt) && Enum.IsDefined(typeof(TEnum), userInputInt))
                {
                    userInput = userInputInt;
                    break;
                }
                else
                {
                    Console.WriteLine($"Invalid input. Please enter a valid value for {typeof(TEnum).Name}. Please try again.");
                }
            }
        }

        private int InputDataOfItemTakeOut()
        {
            Console.WriteLine("Enter an ID number:");
            int identifier = Convert.ToInt32(Console.ReadLine());
            return identifier;
        }
    }
}