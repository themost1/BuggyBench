using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private string[][] recipe;

    [SerializeField]
    private string id;

    [SerializeField]
    private string title;

    [SerializeField]
    private string description;

    public bool inDraft = false;

    void Start()
    {
        recipe = GetRecipe();
        GenerateImage();
    }

    void Update()
    {
        /*
        List<RecipeLocation> locs = GetRecipeLocations();
        foreach (RecipeLocation rl in locs)
        {
            Debug.Log(rl.row + " " + rl.col);
        }
        */
    }

    public virtual string[][] GetRecipe()
    {
        return new string[][]
        {
            new string[] { "", "", "", "" },
            new string[] { "", "", "", "" },
            new string[] { "", "", "", "" },
            new string[] { "", "", "", "" }
        };
    }

    public void GenerateImage()
    {
        int row = 0,
            col = 0;
        Vector3 topLeft = new Vector3(
            0.12f + recipe[0].Length / 2.0f * -0.25f,
            0.3f + 0.25f * recipe.Length / 2.0f,
            0f
        );
        foreach (string[] resourceRow in recipe)
        {
            foreach (string resource in resourceRow)
            {
                GameObject resourceObj = ServiceLocator.resourceCache.GetResource(resource);
                resourceObj.transform.SetParent(transform);
                resourceObj.transform.position = transform.position;
                resourceObj.GetComponent<Renderer>().sortingLayerName = "Card";
                resourceObj.GetComponent<Renderer>().sortingOrder = 1;
                resourceObj.transform.position +=
                    topLeft + new Vector3(0.25f * col, -0.25f * row, 0f);
                resourceObj.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                col++;
            }
            row++;
            col = 0;
        }
    }

    public string GetId()
    {
        return id;
    }

    public string GetTitle()
    {
        return title;
    }

    public string GetDescription()
    {
        return description;
    }

    public List<RecipeLocation> GetRecipeLocations()
    {
        List<RecipeLocation> locs = new List<RecipeLocation>();

        List<List<CraftingSlot>> slots = ServiceLocator.craftingTable.GetSlots();
        for (int row = 0; row < slots.Count; ++row)
        {
            for (int col = 0; col < slots.Count; ++col)
            {
                if (!RecipeExists(slots, row, col))
                {
                    continue;
                }

                RecipeLocation rl = new RecipeLocation();
                rl.row = row;
                rl.col = col;
                locs.Add(rl);
            }
        }

        return locs;
    }

    private bool RecipeExists(List<List<CraftingSlot>> slots, int row, int col)
    {
        string[][] recipe = GetRecipe();
        for (int i = 0; i < recipe.Length; i++)
        {
            for (int j = 0; j < recipe[i].Length; j++)
            {
                int rowCheck = row + i;
                int colCheck = col + j;
                if (rowCheck < 0 || rowCheck >= slots.Count)
                {
                    return false;
                }

                if (colCheck < 0 || colCheck >= slots[rowCheck].Count)
                {
                    return false;
                }

                if (!slots[rowCheck][colCheck].HasResource(recipe[i][j]))
                {
                    return false;
                }
            }
        }

        return true;
    }

    void OnMouseDown()
    {
        if (!inDraft)
        {
            return;
        }
        ServiceLocator.player.GainCard(id);
        Destroy(gameObject);
    }
}
