using System;
using System.Collections.Generic;
using System.Text;

namespace RefrigeratorEx
{
    class Shelf : StorageUnit
    {
        const int basicSpaceShelf = 30;

        public int _floorNumber { get; }
        private int _currentSpaceShelf;
        public List<Item> _items { get; }


        public Shelf(int floorNumber, int currentSpaceShelf, List<Item> items)
        {
            _floorNumber = floorNumber;
            _currentSpaceShelf = basicSpaceShelf;
            _items = items;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Floor Number: {_floorNumber}");
            sb.AppendLine($"The Space Left On The Shelf: {_currentSpaceShelf}");
            sb.AppendLine("Names:");
            foreach (var item in _items)
            {
                sb.AppendLine($"- Name Of Item: {item._name}");
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
