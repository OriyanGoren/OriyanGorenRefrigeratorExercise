using System;
using System.Collections.Generic;
using System.Text;

namespace RefrigeratorEx
{
    class Item : StorageUnit
    {
        /*public enum KosherType
        {
            Dairy,
            Meat,
            Parve
        }*/

        public String _name { get; }
        private int _shelfFloorItem;
        public bool _type { get; } // food or drink
        public String _kosher { get; }  // Dairy, Meat or Parve
        public DateTime _expiryDate { get; }
        public int _spaceItem { get; }


        public Item(String name, int shelfFloorItem, bool type, String kosher, DateTime expiryDate, int spaceItem)
        {
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
            sb.AppendLine($"Name: {_name}");
            sb.AppendLine($"Floor Of Shelf That The Item Is On: {_shelfFloorItem}");
            sb.AppendLine($"Type Of Food: {_type}");
            sb.AppendLine($"Kosher: {_kosher}");
            sb.AppendLine($"Expiry Date: {_expiryDate}");
            sb.AppendLine($"The Space The Item Occupies: {_spaceItem}");

            return sb.ToString();
        }

    }
}
