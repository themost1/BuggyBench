using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCard : Card
{
    public override string[][] GetRecipe()
    {
        return new string[][]
        {
            new string[] { "metal", "", "" },
            new string[] { "", "rock", "" },
            new string[] { "", "", "metal" }
        };
    }

    public override void OnCraft()
    {
        /*
        foreach (RecipeLocation rl in GetRecipeLocations())
        {
            for (int i = 0; i < 4; ++i)
            {
                ServiceLocator.player.AddStatusEffect("safety");
            }
        }
        */
    }

    public override int GetPlayerDamage(int amt)
    {
        return Mathf.Max(0, amt - 1 * GetRecipeLocations().Count);
    }
}
