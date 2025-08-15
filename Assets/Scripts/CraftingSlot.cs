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
        ServiceLocator.inventory.SpendResource();
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (resource == null)
            {
                return;
            }
            ServiceLocator.inventory.AddResource(resource.GetComponent<Resource>().id, 1);
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
        if (bugObj == null)
        {
            return;
        }
        bug.transform.SetParent(transform);
        bug.transform.position = transform.position;
    }

    public bool HasResource(string resourceId)
    {
        if (resourceId == "")
        {
            return true;
        }

        if (resource != null && resource.GetComponent<Resource>().id == resourceId)
        {
            return true;
        }

        if (resource != null && resource.GetComponent<Resource>().id == "diamond")
        {
            return true;
        }

        return false;
    }

    public bool HasAnyResource()
    {
        return resource != null;
    }

    public Bug GetBug()
    {
        return bug == null ? null : bug.GetComponent<Bug>();
    }

    public Bug GetEnemy()
    {
        return GetBug();
    }

    public void ClearResource()
    {
        DestroyImmediate(resource);
        resource = null;
    }
}
