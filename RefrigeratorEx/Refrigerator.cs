using System;
using System.Collections.Generic;
using System.Text;

namespace RefrigeratorEx
{
    class Refrigerator : StorageUnit
    {
        private String _model;
        private String _color;
        private int _numberOfShelves;
        private List<Shelf> _shelves;
    }
}
