using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace RefrigeratorEx
{
    class Refrigerator
    {
        const int potentialFreeSpace = 20;
        DateTime today = DateTime.Today;

        public static int IdCounter = 0;
        public static List<Refrigerator> refrigerators = new List<Refrigerator>();
        private int _identifier;
        private String _model;
        private String _color;
        private int _numberOfShelves;
        private List<Shelf> _shelves;

        public Refrigerator(String model, String color, int numberOfShelves)
        {
            _identifier = IdCounter++;
            _model = model;
            _color = color;
            _numberOfShelves = numberOfShelves;
            _shelves = new List<Shelf>();
            for(int index = 0; index < _numberOfShelves; index++)
            {
                AddShelfToFridge(index);
            }
            refrigerators.Add(this);
        }

        private void AddShelfToFridge(int shelfNumber)
        {
            Shelf shelf = new Shelf(shelfNumber);
            _shelves.Add(shelf);
        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Refrigerator ID: {_identifier}");
            sb.AppendLine($"Model: {_model}");
            sb.AppendLine($"Color: {_color}");
            sb.AppendLine($"Number of shelves: {_numberOfShelves}");
            sb.AppendLine("Shelves:");
            foreach (var shelf in _shelves)
            {
                sb.AppendLine(shelf.ToString());
            }

            return sb.ToString();
        }

        public int GetFreeSpaceInFridge()
        {
            int freeSpaceInFridge = 0;
            foreach (var shelf in _shelves)
            {
                freeSpaceInFridge += shelf.FreeSpace;
            }

            return freeSpaceInFridge;
        }

        public void AddItem(Item item)
        {
            bool itemAdded = false;
            foreach (var shelf in _shelves)
            {
                itemAdded = AddItemToShelfIfSpaceAvailable(shelf, item);
                if (itemAdded)
                {
                    item.ShelfNumber = shelf.Number;
                    break;
                }
            }
            if (!itemAdded)
            {
                Console.WriteLine($"Cannot add item '{item.Name}' to the refrigerator. Not enough space on any shelf.");
            }
        }

        private bool AddItemToShelfIfSpaceAvailable(Shelf shelf, Item item)
        {
            if (shelf.FreeSpace >= item.Space)
            {
                shelf.Items.Add(item);
                shelf.FreeSpace -= item.Space;
                Console.WriteLine($"Item '{item.Name}' added to Shelf {shelf.Number} in the refrigerator.");

                return true;
            }
            return false;
        }

        public Item RemovingItemFromFridge(int itemId)
        {
            Item removedItem = null;
            foreach (var shelf in _shelves)
            {
                removedItem = RemoveFromShelf(shelf, itemId);
                if (removedItem != null)    //The item is in the fridge and has been removed
                {
                    Console.WriteLine($"Item with ID '{itemId}' removed from Shelf {shelf.Number} in the refrigerator.");
                    return removedItem;
                }
            }
            Console.WriteLine($"Item with ID '{itemId}' not found in the refrigerator.");

            return removedItem;
        }

        private Item RemoveFromShelf(Shelf shelf, int itemId)
        {
            Item itemToRemove = shelf.Items.Find(item => item.Identifier == itemId);
            if (itemToRemove != null)
            {
                shelf.Items.Remove(itemToRemove);
                shelf.FreeSpace += itemToRemove.Space;
            }
            return itemToRemove;
        }

        public void CleanTheFridge()
        {
            List<Item> itemsToRemove = new List<Item>();
            foreach (var shelf in _shelves)
            {
                itemsToRemove = AddItemToExpiredList(shelf);
                RemoveExpiredItems(shelf, itemsToRemove);
            }
        }

        private List<Item> AddItemToExpiredList(Shelf shelf)
        {
            List<Item> itemsToRemove = new List<Item>();
            foreach (var item in shelf.Items)
            {
                if (item.ExpiryDate <= today)
                {
                    itemsToRemove.Add(item);
                }
            }
            return itemsToRemove;
        }

        private void RemoveExpiredItems(Shelf shelf, List<Item> itemsToRemove)
        {
            foreach (var itemToRemove in itemsToRemove)
            {
                shelf.Items.Remove(itemToRemove);
                shelf.FreeSpace += itemToRemove.Space;
                Console.WriteLine($"Item '{itemToRemove.Name}' has expired and has been removed from Shelf {shelf.Number}.");
            }
        }

        public List<Item> IWantToEat(Item.Kosher kosher, Item.Type type)
        {
            Item item = null;
            List<Item> items = new List<Item>();

            foreach (var shelf in _shelves)
            {
                item = shelf.Items.Find(item => item.KosherProduct == kosher && item.TypeProduct == type && item.ExpiryDate > today);
                if (item != null)  //Matching item details found
                {
                    items.Add(item);
                }
            }

            return items;
        }

        public List<Item> SortItemsByExpirationDate()
        {
            List<Item> items = new List<Item>();
            foreach (var shelf in _shelves)
            {
                foreach (var item in shelf.Items)
                {
                    items.Add(item);
                }
            }
            
            return items.OrderBy(item => item.ExpiryDate).ToList();
        }

        public List<Shelf> SortShelvesByFreeSpace()
        {
            return _shelves.OrderByDescending(shelf => shelf.FreeSpace).ToList(); ;
        }

        public static List<Refrigerator> SortRefrigeratorsByFreeSpace()
        {
            return refrigerators.OrderByDescending(refrigerator => refrigerator.GetFreeSpaceInFridge()).ToList();
        }

        public void GettingReadyForShopping()
        {
            if (GetFreeSpaceInFridge() < potentialFreeSpace)
            {
                CleanTheFridge();
                if (GetFreeSpaceInFridge() < potentialFreeSpace)
                {
                    ThrowItemsByPriority();
                }
                else
                {
                    Console.WriteLine("Great, we've cleared the fridge of expired products, and now there's at least 20 square centimeters free in the fridge, and you're ready to go shopping.");
                }
            }
            else
            {
                Console.WriteLine("Great, there's at least 20 square centimeters free in the fridge, and you're ready to go shopping.");
            }
        }

        private void ThrowItemsByPriority()
        {
            int freeSpaceByThrowing = 0;
            freeSpaceByThrowing = CheckFreeSpace();

            DetermineProductsToDiscardIfNeeded(freeSpaceByThrowing);
        }

        private int CheckFreeSpace()
        {
            int freeSpaceByThrowing = 0;

            freeSpaceByThrowing = CheckFreeSpaceWithProductsFeatures(Item.Kosher.Dairy, 3);
            freeSpaceByThrowing += CheckFreeSpaceWithProductsFeatures(Item.Kosher.Meat, 7);
            freeSpaceByThrowing += CheckFreeSpaceWithProductsFeatures(Item.Kosher.Parve, 2);

            return freeSpaceByThrowing;
        }

        private void DetermineProductsToDiscardIfNeeded(int freeSpaceByThrowing)
        {
            if (freeSpaceByThrowing < potentialFreeSpace)
            {
                Console.WriteLine("There is not enough space in the refrigerator for new items. Some items are not yet expired and were not removed.");
            }
            else
            {
                ThrowingAwayProductsWithFeatures(Item.Kosher.Dairy, 3);
                if (GetFreeSpaceInFridge() < potentialFreeSpace)
                {
                    ThrowingAwayProductsWithFeatures(Item.Kosher.Meat, 7);
                }
                if (GetFreeSpaceInFridge() < potentialFreeSpace)
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
                foreach (var item in shelf.Items)
                {
                    if (item.KosherProduct == kosher && (item.ExpiryDate - DateTime.Today).Days < numberDaysUntilExpiration)
                    {
                        freeSpace += item.Space;
                    }
                }
            }

            return freeSpace;
        }
        
        private void ThrowingAwayProductsWithFeatures(Item.Kosher kosher, int numberDaysUntilExpiration)
        {
            List<Shelf> sortShelves = SortShelvesByFreeSpace();
            foreach (var shelf in sortShelves)
            {
                List<Item> itemsToRemove = new List<Item>(shelf.Items);
                foreach (var item in itemsToRemove)
                {
                    if (item.KosherProduct == kosher && (item.ExpiryDate - DateTime.Today).Days < numberDaysUntilExpiration)
                    {
                        shelf.Items.Remove(item);
                        shelf.FreeSpace += item.Space;
                        Console.WriteLine($"Item '{item.Name}' ('{kosher}') will expire in a few days and has been removed from the shelf {shelf.Number}.");
                    }
                }
            }
        }  
    }
}
