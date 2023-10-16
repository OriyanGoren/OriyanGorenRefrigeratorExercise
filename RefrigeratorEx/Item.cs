using System;
using System.Collections.Generic;
using System.Text;

namespace RefrigeratorEx
{
    class Item : StorageUnit
    {
        public String _name { get; set; }
        private StorageUnit _idShelfItem;
        private bool _type; // food or drink
        private int _kosher; // dairy, meat or parve
        private DateTime _expiryDate;
        private int _spaceItem;


        public Item(String name, StorageUnit idShelfItem, bool type, int kosher, DateTime expiryDate, int spaceItem)
        {
            _name = name;
            _idShelfItem = idShelfItem;
            _type = type;
            _kosher = kosher;
            _expiryDate = expiryDate;
            _spaceItem = spaceItem;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Name: {_name}");
            sb.AppendLine($"ID Of Shelf The Item Is On: {_idShelfItem}");
            sb.AppendLine($"Type Of Food: {_type}");
            sb.AppendLine($"Kosher: {_kosher}");
            sb.AppendLine($"Expiry Date: {_expiryDate}");
            sb.AppendLine($"The Space The Item Occupies: {_spaceItem}");

            return sb.ToString();
        }
    }
}
