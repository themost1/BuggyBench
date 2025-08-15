using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelCard : Card
{
    public override string[][] GetRecipe()
    {
        return new string[][] { new string[] { "coal", "coal", "wood" } };
    }

    public override void OnCraft()
    {
        foreach (RecipeLocation rl in GetRecipeLocations())
        {
            ServiceLocator.player.AddStatusEffect("strength");
        }
    }
}
