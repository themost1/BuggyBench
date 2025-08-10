using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventorySlot> slots;

    void Awake()
    {
        ServiceLocator.inventory = this;
        DontDestroyOnLoad(this);
        gameObject.SetActive(false);
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

    public void SpendResource()
    {
        foreach (InventorySlot slotIt in slots)
        {
            if (slotIt.selected)
            {
                --slotIt.num;
                if (slotIt.num <= 0)
                {
                    slotIt.Deselect();
                }
                return;
            }
        }
    }
}
