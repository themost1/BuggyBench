using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSlot : MonoBehaviour
{
    private GameObject resource = null;

    private GameObject bug = null;

    void OnMouseDown()
    {
        if (bug != null || resource != null)
        {
            return;
        }

        string selectedResource = ServiceLocator.inventory.GetSelectedResource();
        if (selectedResource == "")
        {
            return;
        }
        resource = ServiceLocator.resourceCache.GetResource(selectedResource);
        resource.transform.position = transform.position;
        resource.transform.SetParent(transform);
        resource.GetComponent<Renderer>().sortingLayerName = "CraftingTable";
        resource.GetComponent<Renderer>().sortingOrder = 1;
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(resource);
            return;
        }
    }

    public bool IsFull()
    {
        return bug != null || resource != null;
    }

    public void SetBug(GameObject bugObj)
    {
        this.bug = bugObj;
        bug.transform.SetParent(transform);
        bug.transform.position = transform.position;
    }

    public bool HasResource(string resourceId)
    {
        if (resource != null && resource.GetComponent<Resource>().id == resourceId)
        {
            return true;
        }

        return false;
    }
}
