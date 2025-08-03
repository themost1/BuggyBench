using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventorySlot> slots;

    void Start()
    {
        ServiceLocator.inventory = this;
    }

    public void OnSlotSelect(InventorySlot slot)
    {
        foreach (InventorySlot slotIt in slots)
        {
            if (slotIt != slot)
            {
                slotIt.Deselect();
            }
        }
    }

    public string GetSelectedResource()
    {
         foreach (InventorySlot slotIt in slots)
        {
            if (slotIt.selected)
            {
                return slotIt.id;
            }
        }
        return "";
    }
}
