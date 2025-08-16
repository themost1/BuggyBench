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
        ServiceLocator.player.LoseHealth(3);
    }
}
