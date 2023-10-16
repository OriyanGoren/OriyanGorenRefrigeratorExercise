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


        public Refrigerator(String model, String color, int numberOfShelves, List<Shelf> shelves)
        {
            _model = model;
            _color = color;
            _numberOfShelves = numberOfShelves;
            _shelves = shelves;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Model: {_model}");
            sb.AppendLine($"Color: {_color}");
            sb.AppendLine($"Number Of Shelves: {_numberOfShelves}");
            sb.AppendLine("Shelves:");
            foreach (var shelf in _shelves)
            {
                sb.AppendLine($"- Floor Of Shelf : {shelf._floorNumber}");
            }

            return sb.ToString();
        }


    }

}
