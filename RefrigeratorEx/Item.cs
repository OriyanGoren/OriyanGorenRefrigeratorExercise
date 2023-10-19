using System;
using System.Text;

namespace RefrigeratorEx
{
    class Item
    {
        public enum Type { Food, Drink }
        public enum Kosher { Dairy, Meat, Parve}

        public static int IdCounter = 0;
        public int _identifier { get; }
        public String _name { get; }
        public int _shelfNumberOfItem { get; set; }
        public Type _type { get; }
        public Kosher _kosher { get; }
        public DateTime _expiryDate { get; }
        public int _spaceItem { get; }


        public Item(String name, Type type, Kosher kosher, DateTime expiryDate, int spaceItem)
        {
            _identifier = IdCounter++;
            _name = name;
            _type = type;
            _kosher = kosher;
            _expiryDate = expiryDate;
            _spaceItem = spaceItem;
        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Product ID: {_identifier}");
            sb.AppendLine($"Name: {_name}");
            sb.AppendLine($"Number of shelf that the item is on: {_shelfNumberOfItem}");
            sb.AppendLine($"Type of food: {_type}");
            sb.AppendLine($"Kosher: {_kosher}");
            sb.AppendLine($"Expiry date: {_expiryDate}");
            sb.AppendLine($"The space the item occupies: {_spaceItem}");

            return sb.ToString();
        }
    }
}
