using System;
using System.Collections.Generic;

namespace RefrigeratorEx
{
    public class RefrigeratorManager
    {
        public void DisplayFridgeContents(Refrigerator refrigerator)
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

        public void DisplayFreeSpaceInFridge(Refrigerator refrigerator)
        {
            int FreeSpaceInFridge = refrigerator.GetFreeSpaceInFridge();
            Console.WriteLine($"Free space in the fridge: '{FreeSpaceInFridge}' square centimete");
        }

        public void AddItemToRefrigerator(Refrigerator refrigerator)
        {
            Item item1 = new UserInputManager().GatherNewItemDetailsFromUser();
            refrigerator.AddItem(item1);
        }

        public void RemoveItemFromRefrigerator(Refrigerator refrigerator)
        {
            Guid ID = new UserInputManager().GetItemIdForRemoval();
            refrigerator.RemoveItemFromFridge(ID);
        }

        public void CleanRefrigerator(Refrigerator refrigerator)
        {
            refrigerator.CleanTheFridge();
        }

        public void SearchForFoodInFridge(Refrigerator refrigerator)
        {
            int kosherInput, typeInput;

            kosherInput = new UserInputManager().GetUserInput<Item.Kosher>("Enter kosher type (0 for Dairy, 1 for Meat, 2 for Parve): ");
            typeInput = new UserInputManager().GetUserInput<Item.Type>("Enter food type (0 for Food, 1 for Drink): ");
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

        public void PrepareFridgeForShopping(Refrigerator refrigerator)
        {
            refrigerator.PrepareFridgeForShopping();
        }
    }
}

