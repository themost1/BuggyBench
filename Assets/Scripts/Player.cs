using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private List<Card> cards = new List<Card>();

    public int health;
    private int initialHealth = 50;

    void Awake()
    {
        ServiceLocator.player = this;
        health = initialHealth;
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
            card.OnCraft();
        }

        foreach (var row in ServiceLocator.craftingTable.GetSlots())
        {
            foreach (var slot in row)
            {
                slot.ClearResource();
            }
        }
    }

    public void LoseHealth(int amt)
    {
        health = Mathf.Max(0, health - amt);
    }

    public void Reset()
    {
        foreach (Card card in cards)
        {
            Destroy(card.gameObject);
        }
        cards.Clear();
        health = initialHealth;
    }
}
