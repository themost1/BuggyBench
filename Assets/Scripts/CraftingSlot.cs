using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSlot : MonoBehaviour
{
    private GameObject resource = null;

    void OnMouseDown()
    {
        string selectedResource = ServiceLocator.inventory.GetSelectedResource();
        Debug.Log(selectedResource);
        if (selectedResource == "")
        {
            return;
        }
        resource = ServiceLocator.resourceCache.GetResource(selectedResource);
        resource.transform.position = transform.position;
        resource.transform.SetParent(transform);
    }
}
