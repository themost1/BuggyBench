using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : MonoBehaviour
{
    [SerializeField]
    private GameObject craftingSlotPrefab;

    private List<List<CraftingSlot>> slots = new List<List<CraftingSlot>>();

    void Awake()
    {
        ServiceLocator.craftingTable = this;
    }

    void Start()
    {
        for (int row = 0; row < 4; row++)
        {
            List<CraftingSlot> slotRow = new List<CraftingSlot>();
            for (int col = 0; col < 4; col++)
            {
                GameObject obj = Instantiate(craftingSlotPrefab);
                obj.transform.position = new Vector3(
                    1.3f * (col - 1.5f),
                    1.3f * ((3 - row) - 1.5f) - 0.8f,
                    0f
                );
                obj.transform.SetParent(transform);
                slotRow.Add(obj.GetComponent<CraftingSlot>());
            }
            slots.Add(slotRow);
        }

        for (int i = 0; i < ServiceLocator.gameManager.GetNumBugs(); ++i)
        {
            SpawnBug();
        }
    }

    private void SpawnBug()
    {
        int attempts = 0;
        while (attempts < 100)
        {
            int row = Random.Range(0, slots.Count);
            int col = Random.Range(0, slots[0].Count);
            if (slots[row][col].IsFull())
            {
                attempts++;
                Debug.Log("fail");
                continue;
            }
            GameObject bug = ServiceLocator.bugCache.CreateRandomBug();
            slots[row][col].SetBug(bug);
            return;
        }
    }

    public List<List<CraftingSlot>> GetSlots()
    {
        return slots;
    }
}
