using System;
using System.Collections.Generic;
using System.Text;

namespace RefrigeratorEx
{
    class Shelf : StorageUnit
    {
        public int _floorNumber { get; set; }
        private int _spaceOnShelf;
        private List<Item> _items;


        public Shelf(int floorNumber, int SpaceOnShelf, List<Item> items)
        {
            _floorNumber = floorNumber;
            _spaceOnShelf = SpaceOnShelf;
            _items = items;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Floor Number: {_floorNumber}");
            sb.AppendLine($"The Space On The Shelf: {_spaceOnShelf}");
            sb.AppendLine("Names:");
            foreach (var item in _items)
            {
                sb.AppendLine($"- Name Of Item: {item._name}");
            }

            return sb.ToString();
        }

    }
}
