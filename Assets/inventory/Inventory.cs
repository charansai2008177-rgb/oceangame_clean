using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    [Header("References")]
    public GameObject Hotbar;
    public GameObject InventorySlotParent;

    [Header("Test items")]
    public itemso wooditem;
    public itemso axeitem;

    private List<slots> inventorySlots = new List<slots>();
    private List<slots> hotbarSlots = new List<slots>();
    private List<slots> allSlots = new List<slots>();

    private void Awake()
    {
        inventorySlots.Clear();
        hotbarSlots.Clear();
        allSlots.Clear();

        if (InventorySlotParent != null)
        {
            inventorySlots.AddRange(InventorySlotParent.GetComponentsInChildren<slots>());
        }
        else
        {
            Debug.LogWarning("InventorySlotParent is not assigned on " + name);
        }

        if (Hotbar != null)
        {
            hotbarSlots.AddRange(Hotbar.GetComponentsInChildren<slots>());
        }
        else
        {
            Debug.LogWarning("Hotbar is not assigned on " + name);
        }

        allSlots.AddRange(inventorySlots);
        allSlots.AddRange(hotbarSlots);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            AddItem(wooditem, 3);
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            AddItem(axeitem, 1);
        }
    }

    public void AddItem(itemso item, int amount = 1)
    {
        if (item == null || amount <= 0)
            return;

        int remaining = amount;

        // First pass: try to add to existing stacks of the same item
        foreach (var slot in allSlots)
        {
            if (remaining <= 0)
                break;

            if (slot == null)
                continue;

            if (slot.hasItem() && slot.GetItem() == item)
            {
                int currentAmount = slot.GetAmount();
                int spaceLeft = item.maxStackSize - currentAmount;
                if (spaceLeft > 0)
                {
                    int toAdd = Mathf.Min(remaining, spaceLeft);
                    slot.AddAmount(toAdd);
                    remaining -= toAdd;
                }
            }
        }

        // Second pass: place into empty slots
        if (remaining > 0)
        {
            foreach (var slot in allSlots)
            {
                if (remaining <= 0)
                    break;

                if (slot == null)
                    continue;

                if (!slot.hasItem())
                {
                    int toPlace = Mathf.Min(item.maxStackSize, remaining);
                    slot.SetItem(item, toPlace);
                    remaining -= toPlace;
                }
            }
        }

        if (remaining > 0)
        {
            Debug.LogWarning($"Not enough space in inventory to add all items. {remaining} of '{item.itemName}' were not added.");
        }
    }
}
    

