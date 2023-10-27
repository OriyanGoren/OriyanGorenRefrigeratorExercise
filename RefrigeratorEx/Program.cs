using System;
using System.Collections.Generic;

namespace RefrigeratorEx
{
    class Program
    {
        private Dictionary<int, Action<Refrigerator>> functions = new Dictionary<int, Action<Refrigerator>>();

        public Program()
        {
            functions[1] = new RefrigeratorManager().DisplayFridgeContents;
            functions[2] = new RefrigeratorManager().DisplayFreeSpaceInFridge;
            functions[3] = new RefrigeratorManager().AddItemToRefrigerator;
            functions[4] = new RefrigeratorManager().RemoveItemFromRefrigerator;
            functions[5] = new RefrigeratorManager().CleanRefrigerator;
            functions[6] = new RefrigeratorManager().SearchForFoodInFridge;
            functions[7] = new SortingManager().DisplayItemsSortedByExpirationDate;
            functions[8] = new SortingManager().DisplayShelvesSortedByFreeSpace;
            functions[9] = new SortingManager().DisplayFridgesSortedByFreeSpace;
            functions[10] = new RefrigeratorManager().PrepareFridgeForShopping;
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
    }
}