using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryFramework
{
    public class HotbarUI : MonoBehaviour
    {
        public Hotbar hotbar;
        public Inventory inventory;
        public Transform slotParent;
        public GameObject slotPrefab;
        public ItemTooltip tooltip;
        public Transform toolsParent;

        private List<InventorySlotUI> slotUIs = new();
        private int selectedIndex = 0;

        public RectTransform dragLayer;
        public Canvas rootCanvas;

        void Start()
        {
            foreach (Transform child in slotParent) Destroy(child.gameObject);

            for (int i = 0; i < hotbar.size; i++)
            {
                var go = Instantiate(slotPrefab, slotParent);
                var ui = go.GetComponent<InventorySlotUI>();
                ui.tooltip = tooltip;
                ui.SetupHotbar(hotbar, inventory, i, this);
                slotUIs.Add(ui);
            }

            RefreshUI();
        }

        void Update()
        {
            for (int i = 0; i < hotbar.size; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                {
                    selectedIndex = i;
                    RefreshUI();
                }
            }

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll > 0f)
            {
                selectedIndex = (selectedIndex + 1) % hotbar.size;
                RefreshUI();
            }
            else if (scroll < 0f)
            {
                selectedIndex = (selectedIndex - 1 + hotbar.size) % hotbar.size;
                RefreshUI();
            }
        }

        public void RefreshUI()
        {
            for (int i = 0; i < hotbar.size; i++)
            {
                slotUIs[i].SetSlot(hotbar.slots[i]);

                var bg = slotUIs[i].transform.GetChild(0).GetComponent<Image>();
                bg.color = (i == selectedIndex) ? Color.yellow : Color.white;
            }

            InventorySlot slot = slotUIs[selectedIndex].GetComponent<InventorySlotUI>().GetSlot();

            for (int x = 0; x < toolsParent.childCount; x++)
            {
                Destroy(toolsParent.GetChild(x).gameObject);
            }

            if (slot == null) return;

            if (slot.IsEmpty) return;

            if (slot.item.model == null) return;

            Instantiate(slot.item.model, toolsParent);
        }
    }



}
