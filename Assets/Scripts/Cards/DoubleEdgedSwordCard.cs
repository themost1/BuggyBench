using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleEdgedSwordCard : Card
{
    public override string[][] GetRecipe()
    {
        return new string[][] { new string[] { "metal", "wood", "metal" }, };
    }

    public override void OnCraft()
    {
        DamageAdjacentEnemies(3);
        for (int i = 0; i < GetRecipeLocations().Count; ++i)
        {
            ServiceLocator.player.LoseHealth(2);
        }
    }
}
