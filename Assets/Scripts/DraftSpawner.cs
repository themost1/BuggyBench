using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraftSpawner : MonoBehaviour
{
    void Start()
    {
        List<GameObject> options = ServiceLocator.cardCache.CreateRandomCards(3);
        for (int i = 0; i < options.Count; i++)
        {
            options[i].transform.position = new Vector3(i * 2f - 2f, 0, 0);
            options[i].GetComponent<Card>().inDraft = true;
        }
    }
}
