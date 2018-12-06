using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class SingleExpence : ValueObject
    {
        public string Category { get; set; }
        public int Amount { get; set; }
    }
}
