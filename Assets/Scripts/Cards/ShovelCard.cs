using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelCard : Card
{
    public override string[][] GetRecipe()
    {
        return new string[][] { new string[] { "", "rock" }, new string[] { "metal", "" } };
    }

    public override void OnCraft()
    {
        List<RecipeLocation> locs = GetRecipeLocations();
        var slots = ServiceLocator.craftingTable.GetSlots();

        foreach (RecipeLocation loc in locs)
        {
            RecipeLocation rl = loc;
            DoDamage(rl);

            rl.row += 2;
            DoDamage(rl);

            rl.row -= 1;
            rl.col -= 1;
            DoDamage(rl);

            rl.row += 2;
            DoDamage(rl);
        }
    }

    private void DoDamage(RecipeLocation rl)
    {
        int dmgAmt = 2;

        var slots = ServiceLocator.craftingTable.GetSlots();
        if (rl.row < 0 || rl.row >= slots.Count || rl.col < 0 || rl.col >= slots[rl.row].Count)
        {
            return;
        }
        Bug bug = slots[rl.row][rl.col].GetEnemy();
        if (bug == null)
        {
            return;
        }
        int bugHealth = bug.health;
        DamageEnemyAtLocation(dmgAmt, rl);
        if (bugHealth > 0 && bugHealth <= 2)
        {
            ServiceLocator.inventory.AddResource("diamond", 1);
        }
    }
}
