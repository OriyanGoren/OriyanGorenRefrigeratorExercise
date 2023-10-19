using System;
using System.Collections.Generic;
using System.Text;

namespace RefrigeratorEx
{
    class Shelf
    {
        const int shelfSize = 15;

        public static int IdCounter = 0;
        public int _identifier { get; }
        public int _shelfNumber { get; }
        public int _currentSpaceShelf { get; set; }
        public List<Item> _items { get; }

        public Shelf(int shelfNumber)
        {
            _identifier = IdCounter++;
            _shelfNumber = shelfNumber;
            _currentSpaceShelf = shelfSize;
            _items = new List<Item>();
        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Shelf ID: {_identifier}");
            sb.AppendLine($"Shelf number: {_shelfNumber}");
            sb.AppendLine($"The basic size of the shelf: {shelfSize}");
            sb.AppendLine($"The current free space on the shelf: {_currentSpaceShelf}");
            sb.AppendLine("Items:");
            foreach (var item in _items)
            {
                sb.AppendLine(item.ToString());
            }

            return sb.ToString();
        }

        public int SpaceLeftOnShelf()
        {
            int spaceItemsShelf = 0;
            foreach (var item in _items)
            {
                spaceItemsShelf += item._spaceItem;
            }
            _currentSpaceShelf -= spaceItemsShelf;
            return _currentSpaceShelf;
        }
    }
}
