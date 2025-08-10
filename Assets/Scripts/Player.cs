using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private List<Card> cards = new List<Card>();

    public int health = 50;

    void Awake()
    {
        ServiceLocator.player = this;
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        for (int i = 0; i < cards.Count; ++i)
        {
            cards[i].transform.position = new Vector3(-7f + 2f * i, 3.4f, 0f);
        }
    }

    public void GainCard(string id)
    {
        GameObject card = ServiceLocator.cardCache.CreateCard(id);
        cards.Add(card.GetComponent<Card>());
    }

    public void Craft()
    {
        foreach (Card card in cards)
        {
            card.Craft();
        }
    }
}
