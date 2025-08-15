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

    public void MoveBugs()
    {
        for (int i = 0; i < 100; ++i)
        {
            if (AttemptMoveBugs())
            {
                return;
            }
        }
    }

    public bool AttemptMoveBugs()
    {
        List<Vector2> moveLocs = new List<Vector2>();
        for (int row = 0; row < slots.Count; ++row)
        {
            for (int col = 0; col < slots[row].Count; ++col)
            {
                if (slots[row][col].GetBug() == null)
                {
                    continue;
                }

                int attempts = 0;
                while (true)
                {
                    attempts++;
                    if (attempts > 100)
                    {
                        return false;
                    }
                    int rowOption = row,
                        colOption = col;
                    int locOption = Random.Range(0, 4);
                    if (locOption == 0)
                    {
                        rowOption = row - 1;
                    }
                    else if (locOption == 1)
                    {
                        colOption = col - 1;
                    }
                    else if (locOption == 2)
                    {
                        rowOption = row + 1;
                    }
                    else if (locOption == 3)
                    {
                        colOption = col + 1;
                    }

                    if (
                        rowOption < 0
                        || rowOption >= slots.Count
                        || colOption < 0
                        || colOption >= slots[0].Count
                    )
                    {
                        continue;
                    }

                    Vector2 loc = new Vector2(rowOption, colOption);
                    if (moveLocs.Contains(loc))
                    {
                        continue;
                    }
                    moveLocs.Add(loc);
                    break;
                }
            }
        }

        int bugsFound = 0;
        for (int row = 0; row < slots.Count; ++row)
        {
            for (int col = 0; col < slots[row].Count; ++col)
            {
                if (slots[row][col].GetBug() == null)
                {
                    continue;
                }

                Vector2 moveSlot = moveLocs[bugsFound];
                Vector3 movePos = slots[(int)moveSlot.x][(int)moveSlot.y].transform.position;
                slots[row][col].GetBug().MoveTo(movePos, moveSlot, new Vector2(row, col));
                bugsFound++;
            }
        }

        return true;
    }

    public void AttackBugs()
    {
        for (int row = 0; row < slots.Count; ++row)
        {
            for (int col = 0; col < slots[row].Count; ++col)
            {
                if (slots[row][col].GetBug() == null)
                {
                    continue;
                }

                ServiceLocator.player.LoseHealth(slots[row][col].GetBug().attack);
            }
        }
    }

    public bool HasNoEnemies()
    {
        for (int row = 0; row < slots.Count; ++row)
        {
            for (int col = 0; col < slots[row].Count; ++col)
            {
                if (slots[row][col].GetEnemy() != null)
                {
                    return false;
                }
            }
        }
        return true;
    }
}
