using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerCard : Card
{
    public override string[][] GetRecipe()
    {
        return new string[][] { new string[] { "rock" }, new string[] { "wood" }, };
    }

    public override void OnCraft()
    {
        List<RecipeLocation> locs = GetRecipeLocations();
        foreach (RecipeLocation loc in locs)
        {
            RecipeLocation rl = loc;
            rl.row -= 1;
            DamageEnemyAtLocation(4, rl);
        }
    }
}
