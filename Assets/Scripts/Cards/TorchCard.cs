using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchCard : Card
{
    public override string[][] GetRecipe()
    {
        return new string[][] { new string[] { "coal" }, new string[] { "wood" }, };
    }

    public override void OnCraft()
    {
        int count = GetRecipeLocations().Count;
        for (int i = 0; i < count; ++i)
        {
            DamageAllEnemies(1);
        }
    }
}
