using UnityEngine;

namespace InventoryFramework
{
    public class Player : MonoBehaviour
    {
        public FPSController fPSController;
        public GameObject inventory;

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                fPSController.canMove = !fPSController.canMove;
                inventory.SetActive(!inventory.activeSelf);

                if (fPSController.canMove)
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
}

