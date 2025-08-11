using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCard : Card
{
    public override string[][] GetRecipe()
    {
        return new string[][]
        {
            new string[] { "rock" },
            new string[] { "wood" },
            new string[] { "wood" }
        };
    }

    public override void OnCraft()
    {
        DamageAdjacentEnemies(2);
    }
}
