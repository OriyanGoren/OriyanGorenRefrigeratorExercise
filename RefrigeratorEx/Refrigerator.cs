using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace RefrigeratorEx
{
    class Refrigerator
    {
        const int shelfSize = 20;
        const int requiredSpace = 20;
        DateTime today = DateTime.Today;

        public static int IdCounter = 1;
        public static List<Refrigerator> refrigerators = new List<Refrigerator>();
        public int _identifier { get; }
        private String _model;
        private String _color;
        private int _numberOfShelves;
        private List<Shelf> _shelves;


        public Refrigerator(String model, String color, int numberOfShelves, List<Shelf> shelves)
        {
            _identifier = IdCounter++;
            _model = model;
            _color = color;
            _numberOfShelves = numberOfShelves;
            _shelves = shelves;
            refrigerators.Add(this);
        }

        public Refrigerator() { }

        public string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Identifier: {_identifier}");
            sb.AppendLine($"Model: {_model}");
            sb.AppendLine($"Color: {_color}");
            sb.AppendLine($"Number of shelves: {_numberOfShelves}");
            sb.AppendLine("Shelves:");
            foreach (var shelf in _shelves)
            {
                sb.AppendLine($"Shelf: {shelf}");
            }

            return sb.ToString();
        }

        public int SpaceLeftOnFridge()
        {
            int spaceLeftFridge = 0;
            foreach (var shelf in _shelves)
            {
                spaceLeftFridge += shelf.SpaceLeftOnShelf();
            }
            return spaceLeftFridge;
        }

        public void PutItemToFridge(Item item)
        {
            bool itemAdded = false;
            foreach (var shelf in _shelves)
            {
                CheckSpaceOnShelf(shelf, item, ref itemAdded);
                if (itemAdded)  //The item has been added
                {
                    break;
                }
            }
            if (!itemAdded)
            {
                Console.WriteLine($"Cannot add item '{item._name}' to the refrigerator. Not enough space on any shelf.");
            }
        }

        private void CheckSpaceOnShelf(Shelf shelf, Item item, ref bool itemAdded)
        {
            if (shelf.SpaceLeftOnShelf() >= item._spaceItem)
            {
                shelf._items.Add(item);
                Console.WriteLine($"Item '{item._name}' added to Shelf {shelf._floorNumber} in the refrigerator.");
                itemAdded = true;
            }
        }

        public Item RemovingItemFromFridge(int itemId)
        {
            Item removedItem = null;
            foreach (var shelf in _shelves)
            {
                ItemRemove(shelf, ref removedItem, itemId);
                if (removedItem != null)    //The item is in the fridge and has been removed
                {
                    Console.WriteLine($"Item with ID '{itemId}' removed from Shelf {shelf._floorNumber} in the refrigerator.");
                    return removedItem;
                }
            }
            Console.WriteLine($"Item with ID '{itemId}' not found in the refrigerator.");
            return removedItem;
        }

        private void ItemRemove(Shelf shelf, ref Item removedItem, int itemId)
        {
            Item itemToRemove = shelf._items.Find(item => item._identifier == itemId);
            if (itemToRemove != null)
            {
                shelf._items.Remove(itemToRemove);
                removedItem = itemToRemove;
            }
        }

        public void CleaningTheFridge()
        {
            foreach (var shelf in _shelves)
            {
                List<Item> itemsToRemove = new List<Item>();
                AddingItemToExpiredList(shelf, ref itemsToRemove);
                RemovingItemExpiredFromFridge(shelf, itemsToRemove);
            }
        }

        private void AddingItemToExpiredList(Shelf shelf, ref List<Item> itemsToRemove)
        {
            foreach (var item in shelf._items)
            {
                if (item._expiryDate <= today)
                {
                    itemsToRemove.Add(item);
                }
            }
        }

        private void RemovingItemExpiredFromFridge(Shelf shelf, List<Item> itemsToRemove)
        {
            foreach (var itemToRemove in itemsToRemove)
            {
                shelf._items.Remove(itemToRemove);
                Console.WriteLine($"Item '{itemToRemove._name}' has expired and has been removed from Shelf {shelf._floorNumber}.");
            }
        }

        public List<Item> IWantToEat(Item.Kosher kosher, Item.Type type)
        {
            Item itemToEat = null;
            List<Item> foodIWillEat = new List<Item>();

            foreach (var shelf in _shelves)
            {
                itemToEat = shelf._items.Find(item => item._kosher == kosher && item._type == type && item._expiryDate > today);
                if (itemToEat != null)  //Matching item details found
                {
                    foodIWillEat.Add(itemToEat);
                }
            }

            return foodIWillEat;
        }

        public List<Item> SortProductsByExpirationDate()
        {
            List<Item> itemsInFridge = new List<Item>();
            foreach (var shelf in _shelves)
            {
                foreach (var item in shelf._items)
                {
                    itemsInFridge.Add(item);
                }
            }
            List<Item> sortedItems = itemsInFridge.OrderBy(item => item._expiryDate).ToList();
            return sortedItems;
        }

        public List<Shelf> SortShelvesByFreeSpace()
        {
            List<Shelf> sortedShelves = _shelves.OrderByDescending(shelf => shelf.SpaceLeftOnShelf()).ToList();
            return sortedShelves;
        }

        //static??
        public List<Refrigerator> SortRefrigeratorsByFreeSpace()
        {
            List<Refrigerator> sortedRefrigerators = refrigerators.OrderByDescending(refrigerator => refrigerator.SpaceLeftOnFridge()).ToList();
            return sortedRefrigerators;
        }

        public void GettingReadyForShopping()
        {
            if (SpaceLeftOnFridge() < requiredSpace)
            {
                CleaningTheFridge();
                if (SpaceLeftOnFridge() < requiredSpace)
                {
                    ThrowingAwayItemsByPriority();
                }
                else
                {
                    Console.WriteLine($"Great, we've cleared the fridge of expired products, and now there's at least 20 square centimeters free in the fridge, and you're ready to go shopping.");
                }
            }
            else
            {
                Console.WriteLine("Great, there's at least 20 square centimeters free in the fridge, and you're ready to go shopping.");
            }
        }

        private void ThrowingAwayItemsByPriority()
        {
            int freeSpaceByThrowing = 0;
            freeSpaceByThrowing = CheckFreeSpaceWithProductsFeatures(Item.Kosher.Dairy, 3);
            freeSpaceByThrowing += CheckFreeSpaceWithProductsFeatures(Item.Kosher.Meat, 7);
            freeSpaceByThrowing += CheckFreeSpaceWithProductsFeatures(Item.Kosher.Parve, 2);

            if(freeSpaceByThrowing < requiredSpace)
            {
                Console.WriteLine("There is not enough space in the refrigerator for new items. Some items are not yet expired and were not removed.");
            }
            else
            {
                ThrowingAwayProductsWithFeatures(Item.Kosher.Dairy, 3);
                if (SpaceLeftOnFridge() < requiredSpace)
                {
                    ThrowingAwayProductsWithFeatures(Item.Kosher.Meat, 7);
                }
                if (SpaceLeftOnFridge() < requiredSpace)
                {
                    ThrowingAwayProductsWithFeatures(Item.Kosher.Parve, 2);
                }
            }
        }

        private int CheckFreeSpaceWithProductsFeatures(Item.Kosher kosher, int numberDaysUntilExpiration)
        {
            int freeSpace = 0;
            foreach (var shelf in _shelves)
            {
                foreach (var item in shelf._items)
                {
                    if (item._kosher == kosher && (item._expiryDate - DateTime.Today).Days < numberDaysUntilExpiration)
                    {
                        freeSpace += item._spaceItem;
                    }
                }
            }
            return freeSpace;
        }
        
        private void ThrowingAwayProductsWithFeatures(Item.Kosher kosher, int numberDaysUntilExpiration)
        {
            foreach (var shelf in SortShelvesByFreeSpace())
            {
                foreach (var item in shelf._items)
                {
                    if (item._kosher == kosher && (item._expiryDate - DateTime.Today).Days < numberDaysUntilExpiration)
                    {
                        shelf._items.Remove(item);
                        Console.WriteLine($"Item '{item._name}' ('{kosher}') will expire in a few days and has been removed from the shelf {shelf._floorNumber}.");
                    }
                }
            }
        }
        

    }
}
