using System;
using System.Collections.Generic;

namespace RefrigeratorEx
{
    public class SortingManager
    {
        public void DisplayItemsSortedByExpirationDate(Refrigerator refrigerator)
        {
            List<Item> sortedItems = refrigerator.SortItemsByExpirationDate();
            foreach (var item in sortedItems)
            {
                Console.WriteLine(item);
            }
        }

        public void DisplayShelvesSortedByFreeSpace(Refrigerator refrigerator)
        {
            List<Shelf> sortedShelves = refrigerator.SortShelvesByFreeSpace();
            foreach (var shelf in sortedShelves)
            {
                Console.WriteLine(shelf);
            }
        }

        public void DisplayFridgesSortedByFreeSpace(Refrigerator refrigerator)
        {
            List<Refrigerator> sortedFridges = Refrigerator.SortRefrigeratorsByFreeSpace();
            sortedFridges.ForEach(fridge => Console.WriteLine(fridge));
        }
    }

}
