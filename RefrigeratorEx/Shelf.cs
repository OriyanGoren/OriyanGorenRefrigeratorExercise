using System;
using System.Collections.Generic;
using System.Text;

namespace RefrigeratorEx
{
    class Shelf
    {
        const int basicSpaceShelf = 20;

        public static int IdCounter = 1;
        public int _identifier { get; }
        public int _floorNumber { get; }
        private int _currentSpaceShelf;
        public List<Item> _items { get; }


        public Shelf(int floorNumber, List<Item> items)
        {
            _identifier = IdCounter++;
            _floorNumber = floorNumber;
            _currentSpaceShelf = basicSpaceShelf;
            _items = items;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Identifier: {_identifier}");
            sb.AppendLine($"Floor number: {_floorNumber}");
            sb.AppendLine($"The space left on the shelf: {_currentSpaceShelf}");
            sb.AppendLine("Items:");
            foreach (var item in _items)
            {
                sb.AppendLine($"Item: {item}");
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
