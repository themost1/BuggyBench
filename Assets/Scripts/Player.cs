using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private List<Card> cards = new List<Card>();

    public int health;
    private int initialHealth = 50;
    private List<StatusEffect> statusEffects = new List<StatusEffect>();

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

    public bool HasCard(string id)
    {
        foreach (Card card in cards)
        {
            if (card.GetId() == id)
            {
                return true;
            }
        }

        return false;
    }

    public int GetDamage(int amt)
    {
        foreach (StatusEffect effect in statusEffects)
        {
            if (effect.isNew)
            {
                continue;
            }
            amt = effect.GetDamage(amt);
        }
        return amt;
    }

    public void MarkStatusEffectsNotNew()
    {
        foreach (StatusEffect effect in statusEffects)
        {
            effect.isNew = false;
        }
    }

    public int GetNumAdditionalRandomBasics()
    {
        int amt = 0;
        foreach (StatusEffect effect in statusEffects)
        {
            if (effect.isNew)
            {
                continue;
            }
            amt = effect.GetDamage(amt);
        }
        return amt;
    }

    public void AddStatusEffect(string id) { }
}
