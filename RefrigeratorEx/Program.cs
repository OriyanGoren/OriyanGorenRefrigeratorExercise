using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace RefrigeratorEx
{
    class Program
    {
        private Dictionary<int, Action<Refrigerator>> functions = new Dictionary<int, Action<Refrigerator>>();

        public Program()
        {
            functions[1] = DisplayFridgeContents;
            functions[2] = DisplayFreeSpaceInFridge;
            functions[3] = AddItemToRefrigerator;
            functions[4] = RemoveItemFromRefrigerator;
            functions[5] = CleanRefrigerator;
            functions[6] = SearchForFoodInFridge;
            functions[7] = DisplayItemsSortedByExpirationDate;
            functions[8] = DisplayShelvesSortedByFreeSpace;
            functions[9] = DisplayFridgesSortedByFreeSpace;
            functions[10] = PrepareFridgeForShopping;
        }

        static void Main(string[] args)
        {
            try
            {
                Program program = new Program();
                Refrigerator refrigerator1 = new Refrigerator("Samsung Family Hub", "Black", 4);
                Refrigerator refrigerator2 = new Refrigerator("Samsung Family Hub", "Black", 4);
                Initializer.InitializeData(refrigerator1, refrigerator2);
                program.RunRefrigeratorGame(refrigerator1);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private void RunRefrigeratorGame(Refrigerator refrigerator)
        {
            bool isRunning = true;
            while (isRunning)
            {
                PrintMenuOptions();
                ProcessUserChoiceAndPlayGame(ref isRunning, refrigerator);
            }
            Console.WriteLine("System shut down.");
        }

        private void PrintMenuOptions()
        {
            Console.WriteLine();
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

        private void ProcessUserChoiceAndPlayGame(ref bool isRunning, Refrigerator refrigerator)
        {
            int choice;
            Console.WriteLine();
            Console.Write("Enter your choice: ");
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine();
                ExecuteGameOption(choice, ref isRunning, refrigerator);
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }

        private void ExecuteGameOption(int choice, ref bool isRunning, Refrigerator refrigerator)
        {
            if (functions.ContainsKey(choice))
            {
                functions[choice](refrigerator);
            }
            else if (choice == 100)
            {
                isRunning = false;
            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }
        }

        private void DisplayFridgeContents(Refrigerator refrigerator)
        {
            if (refrigerator != null)
            {
                Console.WriteLine(refrigerator.ToString());
            }
            else
            {
                Console.WriteLine("Sorry but there are no items in the fridge yet, so we have nothing to display. To put items in the fridge press 3 please.");
            }
        }

        private void DisplayFreeSpaceInFridge(Refrigerator refrigerator)
        {
            int FreeSpaceInFridge = refrigerator.GetFreeSpaceInFridge();
            Console.WriteLine($"Free space in the fridge: '{FreeSpaceInFridge}' square centimete");
        }

        private void AddItemToRefrigerator(Refrigerator refrigerator)
        {
            Item item1 = GatherNewItemDetailsFromUser();
            refrigerator.AddItem(item1);
        }

        private void RemoveItemFromRefrigerator(Refrigerator refrigerator)
        {
            Guid ID = GetItemIdForRemoval();
            refrigerator.RemoveItemFromFridge(ID);
        }

        private void CleanRefrigerator(Refrigerator refrigerator)
        {
            refrigerator.CleanTheFridge();
        }

        private void SearchForFoodInFridge(Refrigerator refrigerator)
        {
            int kosherInput, typeInput;

            kosherInput = GetUserInput<Item.Kosher>("Enter kosher type (0 for Dairy, 1 for Meat, 2 for Parve): ");
            typeInput = GetUserInput<Item.Type>("Enter food type (0 for Food, 1 for Drink): ");
            List<Item> foodIWillEat = refrigerator.IWantToEat((Item.Kosher)kosherInput, (Item.Type)typeInput);
            if (foodIWillEat.Count != 0)
            {
                foodIWillEat.ForEach(item => Console.WriteLine(item));
            }
            else
            {
                Console.WriteLine("Sorry, but we have no suitable products for the options you entered.");
            }
        }

        private void DisplayItemsSortedByExpirationDate(Refrigerator refrigerator)
        {
            List<Item> sortedItems = refrigerator.SortItemsByExpirationDate();
            foreach (var item in sortedItems)
            {
                Console.WriteLine(item);
            }
        }

        private void DisplayShelvesSortedByFreeSpace(Refrigerator refrigerator)
        {
            List<Shelf> sortedShelves = refrigerator.SortShelvesByFreeSpace();
            foreach (var shelf in sortedShelves)
            {
                Console.WriteLine(shelf);
            }
        }

        private void DisplayFridgesSortedByFreeSpace(Refrigerator refrigerator)
        {
            List<Refrigerator> sortedFridges = Refrigerator.SortRefrigeratorsByFreeSpace();
            sortedFridges.ForEach(fridge => Console.WriteLine(fridge));
        }

        private void PrepareFridgeForShopping(Refrigerator refrigerator)
        {
            refrigerator.PrepareFridgeForShopping();
        }

        private Item GatherNewItemDetailsFromUser()
        {
            int kosher, type, spaceItem;
            string name = "";
            DateTime expiryDate = DateTime.Today;

            CheckName(ref name);
            type = GetUserInput<Item.Type>("Enter food type (0 for Food, 1 for Drink): ");
            kosher = GetUserInput<Item.Kosher>("Enter kosher type (0 for Dairy, 1 for Meat, 2 for Parve): ");
            expiryDate = CheckDate();
            spaceItem = CheckSpaceOccupied();
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

        private DateTime CheckDate()
        {
            while (true)
            {
                Console.WriteLine("Enter expiry date (MM/DD/YYYY):");
                string input = Console.ReadLine();
                if (TryParseDate(input, out DateTime expiryDate))
                {
                    return expiryDate;
                }
                else
                {
                    Console.WriteLine("Invalid date format. Please enter the date in MM/DD/YYYY format.");
                    Console.WriteLine("For example, 10/19/2023 for October 19, 2023.");
                }
            }
        }

        private bool TryParseDate(string input, out DateTime expiryDate)
        {
            return DateTime.TryParseExact(input, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out expiryDate);
        }

        private int CheckSpaceOccupied()
        {
            int spaceItem;
            while (true)
            {
                Console.WriteLine("Enter space occupied by the item:");
                string spaceInput = Console.ReadLine();
                if (int.TryParse(spaceInput, out spaceItem) && spaceItem > 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input for space. Please enter a positive integer.");
                }
            }
            return spaceItem;
        }

        private bool TryParseEnumInput<TEnum>(out int userInputInt) where TEnum : Enum
        {
            return int.TryParse(Console.ReadLine(), out userInputInt) && Enum.IsDefined(typeof(TEnum), userInputInt);
        }

        private int GetUserInput<TEnum>(string prompt) where TEnum : Enum
        {
            int userInput;
            while (true)
            {
                Console.WriteLine(prompt);
                if (TryParseEnumInput<TEnum>(out int userInputInt))
                {
                    userInput = userInputInt;
                    break;
                }
                else
                {
                    Console.WriteLine($"Invalid input. Please enter a valid value for {typeof(TEnum).Name}. Please try again.");
                }
            }
            return userInput;
        }

        private Guid GetItemIdForRemoval()
        {
            while (true)
            {
                Console.WriteLine("Enter a GUID:");
                string input = Console.ReadLine();
                if (Guid.TryParse(input, out Guid identifier))
                {
                    return identifier;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid GUID.");
                }
            }
        }
    }
}