using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeCard : Card
{
    public override string[][] GetRecipe()
    {
        return new string[][]
        {
            new string[] { "rock", "rock", "rock" },
            new string[] { "", "wood", "" },
            new string[] { "", "wood", "" }
        };
    }

    public override void OnCraft()
    {
        DamageAdjacentEnemies(3);
        ServiceLocator.inventory.AddResource("diamond", GetRecipeLocations().Count);
    }
}
