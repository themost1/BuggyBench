using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : MonoBehaviour
{
    [SerializeField]
    private GameObject craftingSlotPrefab;

    private List<List<CraftingSlot>> slots = new List<List<CraftingSlot>>();

    void Start()
    {
        for (int row = 0; row < 4; row++)
        {
            List<CraftingSlot> slotRow = new List<CraftingSlot>();
            for (int col = 0; col < 4; col++)
            {
                GameObject obj = Instantiate(craftingSlotPrefab);
                obj.transform.position = new Vector3(1.3f * (col - 1.5f), 1.3f * (row - 1.5f), 0f);
                obj.transform.SetParent(transform);
                slotRow.Add(obj.GetComponent<CraftingSlot>());
            }
            slots.Add(slotRow);
        }
    }
}
