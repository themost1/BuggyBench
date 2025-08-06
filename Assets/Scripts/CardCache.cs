using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCache : MonoBehaviour
{
    public List<GameObject> cards;
    public Dictionary<string, GameObject> cardMap = new Dictionary<string, GameObject>();

    void Awake()
    {
        ServiceLocator.cardCache = this;
        foreach (GameObject card in cards)
        {
            cardMap[card.GetComponent<Card>().GetId()] = card;
        }
    }

    public GameObject CreateCard(string id)
    {
        GameObject prefab = cardMap[id];
        GameObject instance = Instantiate(prefab);
        return instance;
    }
}
