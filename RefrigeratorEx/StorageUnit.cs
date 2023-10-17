using System;
using System.Collections.Generic;
using System.Text;

namespace RefrigeratorEx
{
    class StorageUnit
    {
        public static int uniqueId = 1;
        public int _identifier { get; }


        public StorageUnit()
        {
            _identifier = uniqueId++;
        }

        public override string ToString()
        {
            return $"Identifier: {_identifier}";
        }
    }
}
