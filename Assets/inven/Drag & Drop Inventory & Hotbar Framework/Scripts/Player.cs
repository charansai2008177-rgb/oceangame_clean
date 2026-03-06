using UnityEngine;

namespace InventoryFramework
{
    public class Player : MonoBehaviour
    {
        public MonoBehaviour movementController; // Drag your ThirdPersonController here
        public GameObject inventory;
        public GameObject desk;

        private bool inventoryOpen = false;
        private bool deskOpen = false;

        void Start()
        {
            LockCursor(true);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                ToggleInventory();
            }
        }

        void ToggleInventory()
        {
            inventoryOpen = !inventoryOpen;
            deskOpen = !deskOpen;

            inventory.SetActive(inventoryOpen);
            desk.SetActive(deskOpen);

            if (movementController != null)
                movementController.enabled = !inventoryOpen;

            LockCursor(!inventoryOpen);
        }

        void LockCursor(bool lockState)
        {
            if (lockState)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}