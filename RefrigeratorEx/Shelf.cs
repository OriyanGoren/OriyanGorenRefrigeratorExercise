﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RefrigeratorEx
{
    class Shelf
    {
        const int shelfSize = 15;

        private Guid _identifier;
        public int Number { get; }
        public int FreeSpace { get; set; }
        public List<Item> Items { get; }

        public Shelf(int shelfNumber)
        {
            _identifier = Guid.NewGuid();
            Number = shelfNumber;
            FreeSpace = shelfSize;
            Items = new List<Item>();
        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Shelf ID: {_identifier}");
            sb.AppendLine($"Shelf number: {Number}");
            sb.AppendLine($"The basic size of the shelf: {shelfSize}");
            sb.AppendLine($"Free space: {FreeSpace}");
            sb.AppendLine("Items:");
            foreach (var item in Items)
            {
                sb.AppendLine(item.ToString());
            }

            return sb.ToString();
        }
    }
}
