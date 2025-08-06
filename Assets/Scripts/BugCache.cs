using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BugCache : MonoBehaviour
{
    public List<GameObject> bugs;
    public Dictionary<string, GameObject> bugMap = new Dictionary<string, GameObject>();

    void Awake()
    {
        ServiceLocator.bugCache = this;
        foreach (GameObject bug in bugs)
        {
            bugMap[bug.GetComponent<Bug>().GetId()] = bug;
        }
    }

    public GameObject CreateBug(string id)
    {
        GameObject prefab = bugMap[id];
        GameObject instance = Instantiate(prefab);
        return instance;
    }

    public GameObject CreateRandomBug()
    {
        GameObject prefab = bugMap.ElementAt(Random.Range(0, bugMap.Count)).Value;
        return Instantiate(prefab);
    }
}
