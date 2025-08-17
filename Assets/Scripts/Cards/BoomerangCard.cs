using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangCard : Card
{
    public override string[][] GetRecipe()
    {
        return new string[][]
        {
            new string[] { "", "coal" },
            new string[] { "wood", "" },
            new string[] { "", "coal" }
        };
    }

    public override void OnCraft() { }

    public override List<RecipeLocation> GetRecipeLocations()
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
}
