using System;
using System.Collections.Generic;
using System.Text;

namespace RefrigeratorEx
{
    class Shelf : StorageUnit
    {
        private int _floorNumber;
        private int _SpaceOnShelf;
        private List<Item> _items;
    }
}
