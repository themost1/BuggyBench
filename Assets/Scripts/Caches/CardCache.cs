using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        int metalNum = 0,
            rockNum = 0,
            woodNum = 0,
            coalNum = 0;
        foreach (GameObject card in cards)
        {
            List<string> basics = card.GetComponent<Card>().GetRecipeBasics();
            if (basics.Contains("metal"))
            {
                metalNum++;
            }
            if (basics.Contains("rock"))
            {
                rockNum++;
            }
            if (basics.Contains("wood"))
            {
                woodNum++;
            }
            if (basics.Contains("coal"))
            {
                coalNum++;
            }
        }
        Debug.Log(
            "Metal: " + metalNum + ", Rock: " + rockNum + ", Wood: " + woodNum + ", Coal:" + coalNum
        );
    }

    public GameObject CreateCard(string id)
    {
        GameObject prefab = cardMap[id];
        GameObject instance = Instantiate(prefab);
        return instance;
    }

    public List<GameObject> CreateRandomCards(int num)
    {
        List<string> keys = cardMap.Keys.ToList();
        keys = keys.OrderBy(arg => Guid.NewGuid()).Take(num).ToList();
        List<GameObject> cards = new List<GameObject>();
        for (int i = 0; i < num; i++)
        {
            int index = i % keys.Count;
            cards.Add(CreateCard(keys[index]));
        }
        return cards;
    }

    public List<GameObject> CreateRandomNonHeldCards(int num)
    {
        List<string> keys = cardMap.Keys.ToList();
        keys = keys.OrderBy(arg => Guid.NewGuid()).Take(keys.Count).ToList();
        List<GameObject> cards = new List<GameObject>();
        int i = 0;
        while (cards.Count < num)
        {
            int index = i % keys.Count;
            string id = keys[index];
            if (ServiceLocator.player.HasCard(id))
            {
                i++;
                continue;
            }
            cards.Add(CreateCard(keys[index]));
            i++;
        }
        return cards;
    }
}
