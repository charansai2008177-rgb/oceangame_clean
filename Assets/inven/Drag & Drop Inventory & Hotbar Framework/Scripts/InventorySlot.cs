using UnityEngine;

namespace InventoryFramework
{
    public class InventorySlot
    {
        public Item item;
        public int count;

        public bool IsEmpty => item == null;
    }
}

