using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraftSpawner : MonoBehaviour
{
    private List<GameObject> options = new List<GameObject>();

    void Start()
    {
        options = ServiceLocator.cardCache.CreateRandomNonHeldCards(3);
        for (int i = 0; i < options.Count; i++)
        {
            options[i].transform.position = new Vector3(i * 2f - 2f, 0, 0);
            options[i].GetComponent<Card>().inDraft = true;
        }
    }

    void OnDestroy()
    {
        foreach (GameObject option in options)
        {
            Destroy(option);
        }
    }
}
