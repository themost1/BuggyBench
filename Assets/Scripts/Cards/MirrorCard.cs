using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorCard : Card
{
    public override string[][] GetRecipe()
    {
        return new string[][]
        {
            new string[] { "glass" },
            new string[] { "glass" },
            new string[] { "glass" }
        };
    }

    public override void OnCraft()
    {
        var slots = ServiceLocator.craftingTable.GetSlots();
        foreach (RecipeLocation rl in GetRecipeLocations())
        {
            int count = 0;
            List<Location> adj = FindAdjacentLocationsToRecipe(rl);
            foreach (Location loc in adj)
            {
                if (slots[loc.row][loc.col].HasAnyResource())
                {
                    ++count;
                }
            }

            DamageAllEnemies(count);
        }
    }
}
