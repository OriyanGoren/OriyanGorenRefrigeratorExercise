using System;
using System.Globalization;
using System.Linq;

namespace RefrigeratorEx
{
    public class UserInputManager
    {
        public Item GatherNewItemDetailsFromUser()
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

        public int GetUserInput<TEnum>(string prompt) where TEnum : Enum
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

        public Guid GetItemIdForRemoval()
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
