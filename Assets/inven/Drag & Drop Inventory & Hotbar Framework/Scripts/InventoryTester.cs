using UnityEngine;

namespace InventoryFramework
{
    public class InventoryTester : MonoBehaviour
    {
        public Inventory inventory;
        public Item testItem;
        public Item testItem2;
        public Item testItem3;

        private ItemPickupHandler itemPickupHandler;

        void Start()
        {
            itemPickupHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<ItemPickupHandler>();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                AddItem(testItem);
            }
            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                AddItem(testItem2);
            }
            if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                AddItem(testItem3);
            }
        }

        public void AddItem(Item item)
        {
            itemPickupHandler.PickupItem(item);
        }
    }

}
