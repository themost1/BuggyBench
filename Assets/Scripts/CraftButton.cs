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
        ServiceLocator.craftingTable.ClearAllResources();
        ServiceLocator.craftingTable.MoveBugs();
        ServiceLocator.gameManager.OnCraftEnd();
        ServiceLocator.gameManager.PostCraft();
        DisplayUiAfterCraft();
    }

    private void DisplayUiAfterCraft()
    {
        if (ServiceLocator.gameManager.Lost())
        {
            loseBanner.GetComponent<Banner>().Display();
            return;
        }
        if (ServiceLocator.craftingTable.HasNoEnemies())
        {
            if (ServiceLocator.gameManager.Won())
            {
                winBanner.GetComponent<Banner>().Display();
            }
            proceedButton.SetActive(true);
        }
    }
}
