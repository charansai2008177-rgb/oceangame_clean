using System.Collections.Generic;
using UnityEngine;

namespace InventoryFramework
{
    public class InventoryUI : MonoBehaviour
    {
        public Inventory inventory;
        public Hotbar hotbar;
        public Transform slotParent;
        public GameObject slotPrefab;
        public ItemTooltip tooltip;

        public RectTransform dragLayer;
        public Canvas rootCanvas;

        private List<InventorySlotUI> slotUIs;

        void Start()
        {
            slotUIs = new List<InventorySlotUI>();
            foreach (Transform child in slotParent) Destroy(child.gameObject);

            for (int i = 0; i < inventory.size; i++)
            {
                var slotGO = Instantiate(slotPrefab, slotParent);
                var slotUI = slotGO.GetComponent<InventorySlotUI>();
                slotUI.tooltip = tooltip;
                slotUI.Setup(inventory, hotbar, i, this);
                slotUIs.Add(slotUI);
            }

            RefreshUI();
        }

        public void RefreshUI()
        {
            for (int i = 0; i < inventory.size; i++)
            {
                slotUIs[i].SetSlot(inventory.slots[i]);
            }
        }
    }

}
