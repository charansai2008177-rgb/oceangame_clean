using System.Collections.Generic;
using UnityEngine;

namespace InventoryFramework
{
    public class Inventory : MonoBehaviour
    {
        public int size = 36;
        public List<InventorySlot> slots;

        private void Awake()
        {
            slots = new List<InventorySlot>(new InventorySlot[size]);
            for (int i = 0; i < size; i++)
            {
                slots[i] = new InventorySlot();
            }
        }

        public bool AddItem(Item newItem, int amount = 1)
        {
            // Stacking if possible
            foreach (var slot in slots)
            {
                if (!slot.IsEmpty && slot.item == newItem && slot.count < newItem.maxStack)
                {
                    int space = newItem.maxStack - slot.count;
                    int add = Mathf.Min(space, amount);
                    slot.count += add;
                    amount -= add;

                    if (amount <= 0) return true;
                }
            }

            // Add to empty slot
            foreach (var slot in slots)
            {
                if (slot.IsEmpty)
                {
                    slot.item = newItem;
                    slot.count = amount;

                    return true;
                }
            }

            return false; // Inventory is full
        }

        public void MoveOrSwap(int from, int to)
        {
            if (from == to) return;

            var slotFrom = slots[from];
            var slotTo = slots[to];

            // Move to Empty Slot
            if (slotTo.IsEmpty)
            {
                slotTo.item = slotFrom.item;
                slotTo.count = slotFrom.count;

                slotFrom.item = null;
                slotFrom.count = 0;
            }
            else
            {
                // Swap Items
                var tmpItem = slotFrom.item;
                var tmpCount = slotFrom.count;

                slotFrom.item = slotTo.item;
                slotFrom.count = slotTo.count;

                slotTo.item = tmpItem;
                slotTo.count = tmpCount;
            }
        }
    }



}
