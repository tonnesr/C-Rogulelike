using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougeLikeCSharp
{
    class Item
    {
        //Item meta data
        public string itemName { get; set; }
        public string itemType { get; set; }
        public int itemQuality { get; set; } //0 - 5 (0 Worst - 5 Best)

        //Effects
        public int itemHealthMax { get; set; }
        public int itemHealing { get; set; }
        public int itemManaMax { get; set; }
        public int itemManaRegen { get; set; }
        public int itemExpMax { get; set; }
    }
}
