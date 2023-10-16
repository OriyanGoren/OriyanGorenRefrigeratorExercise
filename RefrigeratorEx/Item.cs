using System;
using System.Collections.Generic;
using System.Text;

namespace RefrigeratorEx
{
    class Item : StorageUnit
    {
        private String _name;
        private StorageUnit _idShelfItem;
        private bool _type; // food or drink
        private int _kosher; // dairy, meat or parve
        private DateTime _expiryDate;
        private int _spaceItem;
    }
}
