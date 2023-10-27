using System;
using System.Collections.Generic;

namespace RefrigeratorEx
{
    class RefrigeratorGame
    {
        private Refrigerator _refrigerator;
        private Dictionary<int, Action<Refrigerator>> _functions = new Dictionary<int, Action<Refrigerator>>();

        public RefrigeratorGame()
        {
            _refrigerator = new Refrigerator("Samsung Family Hub", "Black", 4);
            Refrigerator refrigerator2 = new Refrigerator("Samsung Family Hub", "Black", 4);
            Initializer.InitializeRefrigerators(_refrigerator, refrigerator2);
            Initializer.InitializeFunctionsDictionary(_functions);  
        }

        public void Run()
        {
            bool isRunning = true;
            while (isRunning)
            {
                PrintMenuOptions();
                ProcessUserChoiceAndPlayGame(ref isRunning);
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

        private void ProcessUserChoiceAndPlayGame(ref bool isRunning)
        {
            int choice;
            Console.WriteLine();
            Console.Write("Enter your choice: ");
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine();
                ExecuteGameOption(choice, ref isRunning);
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }

        private void ExecuteGameOption(int choice, ref bool isRunning)
        {
            if (_functions.ContainsKey(choice))
            {
                _functions[choice](_refrigerator);
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
