using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSlot : MonoBehaviour
{
    private GameObject resource = null;

    private GameObject bug = null;

    void OnMouseDown()
    {
        if (bug != null)
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
}
