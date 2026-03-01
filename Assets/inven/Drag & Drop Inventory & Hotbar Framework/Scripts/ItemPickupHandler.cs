using UnityEngine;

namespace InventoryFramework
{
    public class ItemPickupHandler : MonoBehaviour
    {
        public Hotbar hotbar;
        public Inventory inventory;

        public void PickupItem(Item item, int amount = 1)
        {
            bool addedToHotbar = hotbar.AddItem(item, amount);

            if (!addedToHotbar)
            {
                bool addedToInventory = inventory.AddItem(item, amount);

                if (!addedToInventory)
                {
                    Debug.Log("Both hotbar and inventory full! Dropping item...");
                }
            }

            FindAnyObjectByType<HotbarUI>().RefreshUI();
            FindAnyObjectByType<InventoryUI>().RefreshUI();
        }
    }
}


