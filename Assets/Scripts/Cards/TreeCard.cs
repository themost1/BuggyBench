using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCard : Card
{
    public override string[][] GetRecipe()
    {
        return new string[][]
        {
            new string[] { "wood" },
            new string[] { "wood" },
            new string[] { "wood" }
        };
    }

    public override void OnCraft()
    {
        foreach (RecipeLocation rl in GetRecipeLocations())
        {
            ServiceLocator.player.AddStatusEffect("vigor");
        }
    }
}
