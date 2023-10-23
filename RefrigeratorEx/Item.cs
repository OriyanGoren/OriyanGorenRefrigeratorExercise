using System;
using System.Globalization;
using System.Text;

namespace RefrigeratorEx
{
    class Item
    {
        public enum Type { Food, Drink }
        public enum Kosher { Dairy, Meat, Parve}

        public Guid Identifier { get; }
        public String Name { get; }
        public int ShelfNumber { get; set; }
        public Type TypeProduct { get; }
        public Kosher KosherProduct { get; }
        public DateTime ExpiryDate { get; }
        public int Space { get; }

        public Item(String name, Type type, Kosher kosher, DateTime expiryDate, int space)
        {
            Identifier = Guid.NewGuid();
            Name = name;
            TypeProduct = type;
            KosherProduct = kosher;
            ExpiryDate = expiryDate;
            Space = space;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Product ID: {Identifier}");
            sb.AppendLine($"Name: {Name}");
            sb.AppendLine($"Shelf number: {ShelfNumber}");
            sb.AppendLine($"Type of food: {TypeProduct}");
            sb.AppendLine($"Kosher: {KosherProduct}");
            sb.AppendLine($"Expiry date: {ExpiryDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)}");
            sb.AppendLine($"The space the item occupies: {Space}");

            return sb.ToString();
        }
    }
}
