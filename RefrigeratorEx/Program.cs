using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Globalization;

namespace RefrigeratorEx
{
    class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            Refrigerator refrigerator = new Refrigerator("Samsung Family Hub", "Black", 5);
            program.DataInitialize(refrigerator);
            program.MenuOfRefrigeratorGame(refrigerator);
        }

        public void DataInitialize(Refrigerator refrigerator)
        {
            Item item1 = new Item("Milk", Item.Type.Drink, Item.Kosher.Dairy, new DateTime(2023, 10, 15), 5);
            Item item2 = new Item("Pizza", Item.Type.Food, Item.Kosher.Dairy, new DateTime(2023, 10, 23), 10);
            Item item3 = new Item("Chicken", Item.Type.Food, Item.Kosher.Meat, new DateTime(2023, 10, 17), 12);
            Item item4 = new Item("Fish", Item.Type.Food, Item.Kosher.Parve, new DateTime(2023, 10, 25), 8);
            Item item5 = new Item("Pasta", Item.Type.Food, Item.Kosher.Dairy, new DateTime(2023, 10, 23), 15);
            refrigerator.PutItemToFridge(item1);
            refrigerator.PutItemToFridge(item2);
            refrigerator.PutItemToFridge(item3);
            refrigerator.PutItemToFridge(item4);
            refrigerator.PutItemToFridge(item5);
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
            foreach (var item2 in foodIWillEat)
            {
                Console.WriteLine(item2);
            }
        }

        private void DoOption7(Refrigerator refrigerator)
        {
            List<Item> sortedItems = refrigerator.SortProductsByExpirationDate();
            foreach (var item3 in sortedItems)
            {
                Console.WriteLine(item3);
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

            Console.WriteLine("Enter item name:");
            name = Console.ReadLine();
            GetUserInput<Item.Kosher>(ref kosher, "Enter kosher type (0 for Dairy, 1 for Meat, 2 for Parve): ");
            GetUserInput<Item.Type>(ref type, "Enter food type (0 for Food, 1 for Drink): ");
            CheckDate(ref expiryDate);
            CheckSpaceOccupied(ref spaceItem);

            Item newItem = new Item(name, (Item.Type)type, (Item.Kosher)kosher, expiryDate, spaceItem);  // If all inputs are valid, create and return the new item
            return newItem;
        }

        private void CheckDate(ref DateTime expiryDate)
        {
            while (true)
            {
                Console.WriteLine("Enter expiry date (MM/DD/YYYY):");
                try
                {
                    expiryDate = DateTime.ParseExact(Console.ReadLine(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    break; // Exit the loop if the date is parsed successfully
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
                    break; // Exit the loop if space is parsed successfully
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
                    break;  // Exit the loop if valid input is provided
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

        private void InputDataOfFoodToEat(ref int kosherInput, ref int typeInput)
        {
            GetUserInput<Item.Kosher>(ref kosherInput, "Enter kosher type (0 for Dairy, 1 for Meat, 2 for Parve): ");
            GetUserInput<Item.Type>(ref typeInput, "Enter food type (0 for Food, 1 for Drink): ");
        }

    }
}
//Item.IdCounter = 1;