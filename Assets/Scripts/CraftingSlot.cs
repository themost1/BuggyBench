using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSlot : MonoBehaviour
{
    private GameObject resource = null;

    void OnMouseDown()
    {   
        string selectedResource = ServiceLocator.inventory.GetSelectedResource();
        if (selectedResource == "")
        {
            return;
        }
        resource = ServiceLocator.resourceCache.GetResource(selectedResource);
        resource.transform.position = transform.position;
        resource.transform.SetParent(transform);
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(resource);
            return;
        }
    }
}
