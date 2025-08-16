using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private List<Card> cards = new List<Card>();

    public int health;
    private int initialHealth = 50;
    private List<StatusEffect> statusEffects = new List<StatusEffect>();
    private List<string> effectsToAdd = new List<string>();

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

        for (int i = 0; i < statusEffects.Count; ++i)
        {
            statusEffects[i].transform.position = new Vector3(-3.3f - 0.8f * i, -3f, 0f);
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
            amt = effect.GetDamage(amt);
        }
        return amt;
    }

    public void OnTurnStart()
    {
        foreach (string effect in effectsToAdd)
        {
            ApplyStatusEffect(effect);
        }
        effectsToAdd.Clear();
    }

    public int GetNumAdditionalRandomBasics()
    {
        int amt = 0;
        foreach (StatusEffect effect in statusEffects)
        {
            amt += effect.GetNumAdditionalRandomBasics();
        }
        return amt;
    }

    public void AddStatusEffect(string id)
    {
        effectsToAdd.Add(id);
    }

    private void ApplyStatusEffect(string id)
    {
        foreach (StatusEffect effect in statusEffects)
        {
            if (effect.id == id)
            {
                effect.num++;
                return;
            }
        }

        GameObject effectObj = ServiceLocator.statusEffectCache.GetStatusEffect(id);
        statusEffects.Add(effectObj.GetComponent<StatusEffect>());
        // effectObj.transform.position = new Vector3(-3f - 0.8f * statusEffects.Count, -3f, 0f);
    }

    public void ClearStatusEffects()
    {
        foreach (StatusEffect effect in statusEffects)
        {
            Destroy(effect.gameObject);
        }
        statusEffects.Clear();
    }

    public List<string> GetAllRecipeBasics()
    {
        List<string> ret = new List<string>();
        foreach (Card card in cards)
        {
            List<string> cardBasics = card.GetRecipeBasics();
            foreach (string basic in cardBasics)
            {
                ret.Add(basic);
            }
        }

        return ret;
    }
}
