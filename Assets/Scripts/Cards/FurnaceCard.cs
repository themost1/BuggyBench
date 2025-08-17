using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceCard : Card
{
    public override string[][] GetRecipe()
    {
        return new string[][] { new string[] { "rock", "rock" }, new string[] { "coal", "" }, };
    }

    public override void OnCraft()
    {
        foreach (RecipeLocation rl in GetRecipeLocations())
        {
            ServiceLocator.player.AddStatusEffect("strength");
            ServiceLocator.inventory.AddResource("diamond", 1);
        }
    }
}
