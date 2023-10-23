using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace RefrigeratorEx
{
    class Refrigerator
    {
        DateTime today = DateTime.Today;

        private Guid _identifier;
        public static List<Refrigerator> refrigerators = new List<Refrigerator>();
        private String _model;
        private String _color;
        private int _numberOfShelves;
        private List<Shelf> _shelves;

        public Refrigerator(String model, String color, int numberOfShelves)
        {
            _identifier = Guid.NewGuid();
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

        public Item RemoveItemFromFridge(Guid itemId)
        {
            Item removedItem = null;
            foreach (var shelf in _shelves)
            {
                removedItem = RemoveFromShelf(shelf, itemId);
                if (removedItem != null)
                {
                    Console.WriteLine($"Item with ID '{itemId}' removed from Shelf {shelf.Number} in the refrigerator.");
                    return removedItem;
                }
            }
            Console.WriteLine($"Item with ID '{itemId}' not found in the refrigerator.");

            return removedItem;
        }

        private Item RemoveFromShelf(Shelf shelf, Guid itemId)
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
            List<Item> items = new List<Item>();
            items = SearchItems(kosher, type);
            
            return items;
        }

        private List<Item> SearchItems(Item.Kosher kosher, Item.Type type)
        {
            List<Item> items = new List<Item>();
            Item item = null;
            foreach (var shelf in _shelves)
            {
                item = shelf.Items.Find(item => item.KosherProduct == kosher && item.TypeProduct == type && item.ExpiryDate > today);
                if (item != null)
                {
                    items.Add(item);
                }
            }

            return items;
        }

        public List<Item> SortItemsByExpirationDate()
        {
            return _shelves.SelectMany(shelf => shelf.Items).OrderBy(item => item.ExpiryDate).ToList();
        }


        public List<Shelf> SortShelvesByFreeSpace()
        {
            return _shelves.OrderByDescending(shelf => shelf.FreeSpace).ToList(); ;
        }

        public static List<Refrigerator> SortRefrigeratorsByFreeSpace()
        {
            return refrigerators.OrderByDescending(refrigerator => refrigerator.GetFreeSpaceInFridge()).ToList();
        }

        public void PrepareFridgeForShopping()
        {
            if (GetFreeSpaceInFridge() < Constants.REQUIRED_SPACE)
            {
                PrepareFridgeForShoppingInternal();
            }
            else
            {
                Console.WriteLine("Great, there's at least 20 square centimeters free in the fridge, and you're ready to go shopping.");
            }
        }

        private void PrepareFridgeForShoppingInternal()
        {
            CleanTheFridge();
            if (GetFreeSpaceInFridge() < Constants.REQUIRED_SPACE)
            {
                ThrowItemsByPriority();
            }
            else
            {
                Console.WriteLine("Great, we've cleared the fridge of expired products, and now there's at least 20 square centimeters free in the fridge, and you're ready to go shopping.");
            }
        }


        private void ThrowItemsByPriority()
        {
            DetermineItemsToDiscardIfNeeded(CheckFreeSpace());
        }

        private int CheckFreeSpace()
        {
            int freeSpaceByThrowing = 0;

            freeSpaceByThrowing = CheckFreeSpaceWithItemsFeatures(Item.Kosher.Dairy, 3);
            freeSpaceByThrowing += CheckFreeSpaceWithItemsFeatures(Item.Kosher.Meat, 7);
            freeSpaceByThrowing += CheckFreeSpaceWithItemsFeatures(Item.Kosher.Parve, 2);

            return freeSpaceByThrowing;
        }

        private void DetermineItemsToDiscardIfNeeded(int freeSpaceByThrowing)
        {
            if (freeSpaceByThrowing < Constants.REQUIRED_SPACE)
            {
                Console.WriteLine("There is not enough space in the refrigerator for new items. Some items are not yet expired and were not removed.");
            }
            else
            {
                DiscardItemsBasedOnKosherType(Item.Kosher.Dairy, 3);
                DiscardItemsBasedOnKosherType(Item.Kosher.Meat, 7);
                DiscardItemsBasedOnKosherType(Item.Kosher.Parve, 2);
            }
        }

        private void DiscardItemsBasedOnKosherType(Item.Kosher kosherType, int requiredSpace)
        {
            if (GetFreeSpaceInFridge() < Constants.REQUIRED_SPACE)
            {
                DiscardItemsWithFeatures(kosherType, requiredSpace);
            }
        }

        private int CheckFreeSpaceWithItemsFeatures(Item.Kosher kosher, int numberDaysUntilExpiration)
        {
            int freeSpace = 0;
            foreach (var shelf in _shelves)
            {
                foreach (var item in shelf.Items)
                {
                    if (ShouldDiscardItem(item, kosher, numberDaysUntilExpiration))
                    {
                        freeSpace += item.Space;
                    }
                }
            }

            return freeSpace;
        }

        private bool ShouldDiscardItem(Item item, Item.Kosher kosher, int numberDaysUntilExpiration)
        {
            return item.KosherProduct == kosher && (item.ExpiryDate - DateTime.Today).Days < numberDaysUntilExpiration;
        }

        private void DiscardItemsWithFeatures(Item.Kosher kosher, int numberDaysUntilExpiration)
        {
            List<Shelf> sortShelves = SortShelvesByFreeSpace();
            foreach (var shelf in sortShelves)
            {
                List<Item> itemsToRemove = new List<Item>(shelf.Items);
                foreach (var item in itemsToRemove)
                {
                    if (ShouldDiscardItem(item, kosher, numberDaysUntilExpiration))
                    {
                        RemoveItemFromShelf(item, shelf);
                    }
                }
            }
        }

        private void RemoveItemFromShelf(Item item, Shelf shelf)
        {
            shelf.Items.Remove(item);
            shelf.FreeSpace += item.Space;
            Console.WriteLine($"Item '{item.Name}' ('{item.KosherProduct}') will expire in a few days and has been removed from the shelf {shelf.Number}.");
        }
    }
}
