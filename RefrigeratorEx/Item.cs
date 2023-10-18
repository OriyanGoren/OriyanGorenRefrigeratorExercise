using System;
using System.Collections.Generic;
using System.Text;

namespace RefrigeratorEx
{
    class Item
    {
        public enum KosherType { Dairy, Meat, Parve}
        public static int IdCounter = 1;
        public int _identifier { get; }
        public String _name { get; }
        private int _shelfFloorItem;
        public bool _type { get; } // food or drink
        public String _kosher { get; }  // Dairy, Meat or Parve
        public DateTime _expiryDate { get; }
        public int _spaceItem { get; }


        public Item(String name, int shelfFloorItem, bool type, String kosher, DateTime expiryDate, int spaceItem)
        {
            _identifier = IdCounter++;
            _name = name;
            _shelfFloorItem = shelfFloorItem;
            _type = type;
            _kosher = kosher;
            _expiryDate = expiryDate;
            _spaceItem = spaceItem;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Identifier: {_identifier}");
            sb.AppendLine($"Name: {_name}");
            sb.AppendLine($"Floor of shelf that the item is on: {_shelfFloorItem}");
            sb.AppendLine($"Type of food: {_type}");
            sb.AppendLine($"Kosher: {_kosher}");
            sb.AppendLine($"Expiry date: {_expiryDate}");
            sb.AppendLine($"The space the item occupies: {_spaceItem}");

            return sb.ToString();
        }

    }
}
