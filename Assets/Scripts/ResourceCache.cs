using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCache : MonoBehaviour
{
    public List<GameObject> resources;
    public Dictionary<string, GameObject> resourceMap = new Dictionary<string, GameObject>();

    void Start()
    {
        ServiceLocator.resourceCache = this;
        foreach (GameObject resource in resources)
        {
            resourceMap[resource.GetComponent<Resource>().id] = resource;
            Debug.Log(resource.GetComponent<Resource>().id);
        }
    }

    public GameObject GetResource(string resourceId)
    {
        GameObject prefab = resourceMap[resourceId];
        return Instantiate(prefab);
    }
}
