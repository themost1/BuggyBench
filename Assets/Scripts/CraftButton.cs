using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftButton : MonoBehaviour
{
    [SerializeField]
    private GameObject proceedButton;

    void OnMouseDown()
    {
        ServiceLocator.player.Craft();
        ServiceLocator.craftingTable.AttackBugs();
        ServiceLocator.craftingTable.MoveBugs();
        ServiceLocator.gameManager.OnCraftEnd();

        if (ServiceLocator.craftingTable.HasNoEnemies())
        {
            proceedButton.SetActive(true);
        }
    }
}
