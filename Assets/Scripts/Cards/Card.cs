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
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        if (ServiceLocator.craftingTable == null)
        {
            GetComponent<CardUi>().UpdateTimesText(0);
            return;
        }
        GetComponent<CardUi>().UpdateTimesText(GetRecipeLocations().Count);
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
                if (resource == "")
                {
                    col++;
                    continue;
                }
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
        ServiceLocator.gameManager.LoadNextSceneAfterDraft();
    }

    public virtual void OnCraft()
    {
        DamageAllEnemies(3);
    }

    protected void DamageAllEnemies(int amt)
    {
        var slots = ServiceLocator.craftingTable.GetSlots();
        foreach (var row in slots)
        {
            foreach (var slot in row)
            {
                Bug enemy = slot.GetEnemy();
                if (enemy == null)
                {
                    continue;
                }

                enemy.TakeDamage(ServiceLocator.player.GetDamage(amt));
            }
        }
    }

    protected void DamageAdjacentEnemies(int amt)
    {
        var locs = GetRecipeLocations();
        foreach (RecipeLocation rl in locs)
        {
            List<Bug> enemies = FindAdjacentEnemies(rl);
            foreach (Bug enemy in enemies)
            {
                enemy.TakeDamage(ServiceLocator.player.GetDamage(amt));
            }
        }
    }

    protected void DamageEnemyAtLocation(int amt, RecipeLocation location)
    {
        var slots = ServiceLocator.craftingTable.GetSlots();
        if (
            location.row < 0
            || location.row >= slots.Count
            || location.col < 0
            || location.col >= slots[location.row].Count
        )
        {
            return;
        }
        Bug bug = slots[location.row][location.col].GetEnemy();
        if (bug == null)
        {
            return;
        }
        bug.TakeDamage(ServiceLocator.player.GetDamage(amt));
    }

    protected List<Bug> FindAdjacentEnemies(RecipeLocation rl)
    {
        var slots = ServiceLocator.craftingTable.GetSlots();
        List<Bug> adjacent = new List<Bug>();
        for (int row = rl.row; row < rl.row + GetRecipe().Length; ++row)
        {
            for (int col = rl.col; col < rl.col + GetRecipe()[0].Length; ++col)
            {
                List<Bug> toAdd = FindAdjacentEnemies(row, col);
                foreach (Bug b in toAdd)
                {
                    if (!adjacent.Contains(b))
                    {
                        adjacent.Add(b);
                    }
                }
            }
        }
        return adjacent;
    }

    private List<Bug> FindAdjacentEnemies(int row, int col)
    {
        var ret = new List<Bug>();
        var slots = ServiceLocator.craftingTable.GetSlots();
        if (row > 0)
        {
            Bug b = slots[row - 1][col].GetEnemy();
            if (b != null)
            {
                ret.Add(b);
            }
        }
        if (row < slots.Count - 1)
        {
            Bug b = slots[row + 1][col].GetEnemy();
            if (b != null)
            {
                ret.Add(b);
            }
        }
        if (col > 0)
        {
            Bug b = slots[row][col - 1].GetEnemy();
            if (b != null)
            {
                ret.Add(b);
            }
        }
        if (col < slots[row].Count - 1)
        {
            Bug b = slots[row][col + 1].GetEnemy();
            if (b != null)
            {
                ret.Add(b);
            }
        }
        return ret;
    }

    public List<Location> FindAdjacentLocationsToRecipe(RecipeLocation topLeftLoc)
    {
        List<Location> recipeLocs = GetResourceLocationsInRecipe(topLeftLoc);

        List<Location> ret = new List<Location>();
        foreach (Location loc in recipeLocs)
        {
            List<Location> adj = GetAdjacentLocations(loc);

            foreach (Location adjOption in adj)
            {
                bool exists = false;
                // TODO: Override necessary operators to just use list.Contains.
                foreach (Location l in ret)
                {
                    if (l.row == adjOption.row && l.col == adjOption.col)
                    {
                        exists = true;
                        break;
                    }
                }
                if (exists)
                {
                    continue;
                }

                foreach (Location l in recipeLocs)
                {
                    if (l.row == adjOption.row && l.col == adjOption.col)
                    {
                        exists = true;
                        break;
                    }
                }
                if (exists)
                {
                    continue;
                }

                ret.Add(adjOption);
            }
        }
        return ret;
    }

    private List<Location> GetAdjacentLocations(Location loc)
    {
        var ret = new List<Location>();
        var slots = ServiceLocator.craftingTable.GetSlots();
        if (loc.row > 0)
        {
            ret.Add(new Location(loc.row - 1, loc.col));
        }
        if (loc.row < slots.Count - 1)
        {
            ret.Add(new Location(loc.row + 1, loc.col));
        }
        if (loc.col > 0)
        {
            ret.Add(new Location(loc.row, loc.col - 1));
        }
        if (loc.col < slots[loc.row].Count - 1)
        {
            ret.Add(new Location(loc.row, loc.col + 1));
        }
        return ret;
    }

    public List<string> GetRecipeBasics()
    {
        var ret = new List<string>();
        foreach (var row in GetRecipe())
        {
            foreach (string basic in row)
            {
                if (basic == "")
                {
                    continue;
                }
                ret.Add(basic);
            }
        }

        return ret;
    }

    public virtual int GetPlayerDamage(int amt)
    {
        return amt;
    }

    protected List<Location> GetResourceLocationsInRecipe(RecipeLocation topLeftLoc)
    {
        List<Location> recipeLocs = new List<Location>();
        string[][] recipe = GetRecipe();
        int row = 0,
            col = 0;
        foreach (string[] recipeRow in recipe)
        {
            foreach (string val in recipeRow)
            {
                if (val == "")
                {
                    col++;
                    continue;
                }
                Location l = new Location(topLeftLoc.row + row, topLeftLoc.col + col);
                recipeLocs.Add(l);
                col++;
            }
            row++;
            col = 0;
        }
        return recipeLocs;
    }

    public List<List<Location>> GetRecipeResourceLocations()
    {
        var ret = new List<List<Location>>();
        var recipeLocations = GetRecipeLocations();
        foreach (RecipeLocation rl in recipeLocations)
        {
            ret.Add(GetResourceLocationsInRecipe(rl));
        }
        return ret;
    }
}
