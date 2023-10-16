using System;
using System.Collections.Generic;
using System.Text;

namespace RefrigeratorEx
{
    class StorageUnit
    {
        protected int _identifier;


        public override string ToString()
        {
            return $"Identifier: {_identifier}";
        }
    }
}
