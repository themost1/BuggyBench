using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftButton : MonoBehaviour
{
    void OnMouseDown()
    {
        ServiceLocator.craftingTable.MoveBugs();
    }
}
