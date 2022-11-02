using System;
using System.Collections.Generic;
using System.Text;

namespace Zork.Common
{
    public class Item
    {
        public string Name { get; }

        public string LookDescription { get; }

        public string InvDescription { get; }

        public Item(string name, string lookDescription, string invDescription)
        {
            Name = name;
            LookDescription = lookDescription;
            InvDescription = invDescription;
        }
    }
}
