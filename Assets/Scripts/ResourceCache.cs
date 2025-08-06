using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCache : MonoBehaviour
{
    public List<GameObject> resources;
    public Dictionary<string, GameObject> resourceMap = new Dictionary<string, GameObject>();

    void Awake()
    {
        ServiceLocator.resourceCache = this;
        foreach (GameObject resource in resources)
        {
            resourceMap[resource.GetComponent<Resource>().id] = resource;
        }
    }

    public GameObject GetResource(string resourceId)
    {
        GameObject prefab = resourceMap[resourceId];
        return Instantiate(prefab);
    }
}
