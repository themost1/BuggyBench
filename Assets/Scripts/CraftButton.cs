using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftButton : MonoBehaviour
{
    [SerializeField]
    private GameObject proceedButton;

    [SerializeField]
    private GameObject winBanner,
        loseBanner;

    void OnMouseDown()
    {
        ServiceLocator.player.Craft();
        ServiceLocator.craftingTable.AttackBugs();
        ServiceLocator.craftingTable.MoveBugs();
        ServiceLocator.gameManager.OnCraftEnd();
        DisplayUiAfterCraft();
    }

    private void DisplayUiAfterCraft()
    {
        if (ServiceLocator.player.health <= 0)
        {
            loseBanner.GetComponent<Banner>().Display();
        }
        if (ServiceLocator.craftingTable.HasNoEnemies())
        {
            if (ServiceLocator.gameManager.InFinalFight())
            {
                winBanner.GetComponent<Banner>().Display();
            }
            proceedButton.SetActive(true);
        }
    }
}
