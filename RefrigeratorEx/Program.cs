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
            program.MenuOfRefrigeratorGame();
        }

        private void MenuOfRefrigeratorGame()
        {
            bool isRunning = true;
            while (isRunning)
            {
                ShowingButtonsToUser();
                PlayingGameByUser(ref isRunning);
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

        private void PlayingGameByUser(ref bool isRunning)
        {
            int choice;
            Console.WriteLine();
            Console.Write("Enter your choice: ");
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine();
                ActivatingGameFunctions(choice, ref isRunning);
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }

        private void ActivatingGameFunctions(int choice, ref bool isRunning)
        {
            Refrigerator refrigerator = DataInitialize();
            switch (choice)
            {
                case 1:
                    Console.WriteLine(refrigerator.ToString());
                    break;
                case 2:
                    int FreeSpaceInFridge = refrigerator.SpaceLeftOnFridge();
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
                    int kosherInput = 0;
                    int typeInput = 0;
                    InputDataOfFoodToEat(ref kosherInput, ref typeInput);
                    List<Item> foodIWillEat = refrigerator.IWantToEat((Item.Kosher)kosherInput, (Item.Type)typeInput);
                    foreach (var item2 in foodIWillEat)
                    {
                        Console.WriteLine(item2);
                    }
                        break;
                case 7:
                    List<Item> sortedItems = refrigerator.SortProductsByExpirationDate();
                    foreach (var item3 in sortedItems)
                    {
                        Console.WriteLine(item3);
                    }
                    break;
                case 8:
                    List<Shelf> sortedShelves = refrigerator.SortShelvesByFreeSpace();
                    foreach (var shelf in sortedShelves)
                    {
                        Console.WriteLine(shelf);
                    }
                    break;
                case 9:
                    List<Refrigerator> sortedFridges = refrigerator.SortRefrigeratorsByFreeSpace();
                    foreach (var fridge in sortedFridges)
                    {
                        Console.WriteLine(fridge);
                    }
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

        private Refrigerator DataInitialize()
        {
            Item item1 = new Item("Milk", 1, Item.Type.Drink, Item.Kosher.Dairy, new DateTime(2023, 10, 15), 2);
            Item item2 = new Item("Pizza", 1, Item.Type.Food, Item.Kosher.Dairy, new DateTime(2023, 10, 23), 3);
            Item item3 = new Item("Chicken", 2, Item.Type.Food, Item.Kosher.Meat, new DateTime(2023, 10, 17), 5);
            Item item4 = new Item("Fish", 2, Item.Type.Food, Item.Kosher.Parve, new DateTime(2023, 10, 25), 3);
            Item item5 = new Item("Pasta", 2, Item.Type.Food, Item.Kosher.Dairy, new DateTime(2023, 10, 23), 5);
            List<Item> items1 = new List<Item>();
            items1.Add(item1);
            items1.Add(item2);
            Shelf shelf1 = new Shelf(1, items1);
            List<Item> items2 = new List<Item>();
            items2.Add(item3);
            items2.Add(item4);
            items2.Add(item5);
            Shelf shelf2 = new Shelf(2, items2);
            List<Shelf> shelves = new List<Shelf>();
            shelves.Add(shelf1);
            shelves.Add(shelf2);
            Refrigerator refrigerator = new Refrigerator("Samsung Family Hub", "Black", 5, shelves);

            return refrigerator;
        }

        private Item InputDataOfItemInsertion()
        {
            Console.WriteLine("Enter item name:");
            string name = Console.ReadLine();

            Console.WriteLine("Enter shelf floor number:");
            int shelfFloorItem = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter item type (0 for Food, 1 for Drink):");
            Item.Type type = (Item.Type)Enum.Parse(typeof(Item.Type), Console.ReadLine());

            Console.WriteLine("Enter kosher type (0 for Dairy, 1 for Meat, 2 for Parve):");
            Item.Kosher kosher = (Item.Kosher)Enum.Parse(typeof(Item.Kosher), Console.ReadLine());

            Console.WriteLine("Enter expiry date (MM/DD/YYYY):");
            DateTime expiryDate = DateTime.ParseExact(Console.ReadLine(), "MM/dd/yyyy", CultureInfo.InvariantCulture);

            Console.WriteLine("Enter space occupied by the item:");
            int spaceItem = Convert.ToInt32(Console.ReadLine());

            Item newItem = new Item(name, shelfFloorItem, type, kosher, expiryDate, spaceItem);
            return newItem;
        }
        
        private int InputDataOfItemTakeOut()
        {
            Console.WriteLine("Enter an ID number:");
            int identifier = Convert.ToInt32(Console.ReadLine());
            return identifier;
        }

        private void InputDataOfFoodToEat(ref int kosherInput, ref int typeInput)
        {
            Console.WriteLine("Enter kosher type (0 for Dairy, 1 for Meat, 2 for Parve): ");
            int kosherInt, typeInt;
            if (int.TryParse(Console.ReadLine(), out kosherInt) && Enum.IsDefined(typeof(Item.Kosher), kosherInt))
            {
                kosherInput = kosherInt;
            }
            else
            {
                Console.WriteLine("Invalid input for kosher type.");
            }
            Console.WriteLine("Enter food type (0 for Food, 1 for Drink): ");
            if (int.TryParse(Console.ReadLine(), out typeInt) && Enum.IsDefined(typeof(Item.Type), typeInt))
            {
                typeInput = typeInt;
            }
            else
            {
                Console.WriteLine("Invalid input for food type.");
            }
        }


    }
}
