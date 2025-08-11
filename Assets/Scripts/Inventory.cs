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

    public void AddResource(string id, int num)
    {
        foreach (InventorySlot slotIt in slots)
        {
            if (slotIt.id != id)
            {
                continue;
            }
            slotIt.num += num;
            return;
        }
    }

    public void GenerateRandomBasics(int num)
    {
        int numBasics = 4;
        for (int i = 0; i < numBasics; ++i)
        {
            slots[i].num = 0;
        }

        for (int i = 0; i < num; ++i)
        {
            int slot = Random.Range(0, numBasics);
            slots[slot].num++;
        }
    }
}
