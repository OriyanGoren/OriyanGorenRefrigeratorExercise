using System;
using System.Collections.Generic;

namespace RefrigeratorEx
{
    public class Initializer
    {
        public static void InitializeRefrigerators(Refrigerator refrigerator1, Refrigerator refrigerator2)
        {
            Item item1 = new Item("Milk", Item.Type.Drink, Item.Kosher.Dairy, new DateTime(2023, 10, 28), 7);
            Item item2 = new Item("Pizza", Item.Type.Food, Item.Kosher.Dairy, new DateTime(2023, 10, 28), 14);
            Item item3 = new Item("Chicken", Item.Type.Food, Item.Kosher.Meat, new DateTime(2023, 10, 25), 9);
            Item item4 = new Item("Fish", Item.Type.Food, Item.Kosher.Parve, new DateTime(2023, 10, 21), 12);
            Item item5 = new Item("Pasta", Item.Type.Food, Item.Kosher.Dairy, new DateTime(2023, 10, 26), 5);
            Item item6 = new Item("Rice", Item.Type.Food, Item.Kosher.Parve, new DateTime(2023, 10, 23), 10);
            Item item7 = new Item("Juice", Item.Type.Drink, Item.Kosher.Parve, new DateTime(2023, 10, 23), 8);
            Item item8 = new Item("Bean", Item.Type.Food, Item.Kosher.Parve, new DateTime(2023, 10, 23), 8);
            refrigerator1.AddItem(item1);
            refrigerator1.AddItem(item2);
            refrigerator1.AddItem(item3);
            refrigerator1.AddItem(item4);
            refrigerator1.AddItem(item5);
            refrigerator2.AddItem(item6);
            refrigerator2.AddItem(item7);
            refrigerator2.AddItem(item8);
        }

        public static void InitializeFunctionsDictionary(Dictionary<int, Action<Refrigerator>> _functions)
        {
            _functions[1] = new RefrigeratorManager().DisplayFridgeContents;
            _functions[2] = new RefrigeratorManager().DisplayFreeSpaceInFridge;
            _functions[3] = new RefrigeratorManager().AddItemToRefrigerator;
            _functions[4] = new RefrigeratorManager().RemoveItemFromRefrigerator;
            _functions[5] = new RefrigeratorManager().CleanRefrigerator;
            _functions[6] = new RefrigeratorManager().SearchForFoodInFridge;
            _functions[7] = new SortingManager().DisplayItemsSortedByExpirationDate;
            _functions[8] = new SortingManager().DisplayShelvesSortedByFreeSpace;
            _functions[9] = new SortingManager().DisplayFridgesSortedByFreeSpace;
            _functions[10] = new RefrigeratorManager().PrepareFridgeForShopping;
        }
    }
}
