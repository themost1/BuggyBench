using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoltenMetalCard : Card
{
    public override string[][] GetRecipe()
    {
        return new string[][] { new string[] { "coal", "metal" } };
    }

    public override void OnCraft()
    {
        foreach (RecipeLocation rl in GetRecipeLocations())
        {
            OnCraft(rl);
        }
    }

    private void OnCraft(RecipeLocation rl)
    {
        List<Location> locs = GetResourceLocationsInRecipe(rl);

        int matches = 0;
        foreach (Card card in ServiceLocator.player.GetCards())
        {
            if (card == this)
            {
                continue;
            }

            foreach (List<Location> otherLocs in card.GetRecipeResourceLocations())
            {
                bool foundMatch = false;
                foreach (Location otherLoc in otherLocs)
                {
                    foreach (Location loc in locs)
                    {
                        if (otherLoc.row == loc.row && otherLoc.col == loc.col)
                        {
                            foundMatch = true;
                            break;
                        }
                    }
                    if (foundMatch)
                    {
                        break;
                    }
                }

                if (foundMatch)
                {
                    ++matches;
                }
            }
        }
        DamageAllEnemies(matches * 2);
    }
}
