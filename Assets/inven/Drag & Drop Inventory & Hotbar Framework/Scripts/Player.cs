using UnityEngine;

namespace InventoryFramework
{
    public class Player : MonoBehaviour
    {
        public MonoBehaviour movementController; // Drag your ThirdPersonController here
        public GameObject inventory;

        private bool inventoryOpen = false;

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

            inventory.SetActive(inventoryOpen);

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