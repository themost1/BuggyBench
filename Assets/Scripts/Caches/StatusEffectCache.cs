using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectCache : MonoBehaviour
{
    public List<GameObject> effects;
    public Dictionary<string, GameObject> effectMap = new Dictionary<string, GameObject>();

    void Awake()
    {
        ServiceLocator.statusEffectCache = this;
        foreach (GameObject effect in effects)
        {
            effectMap[effect.GetComponent<StatusEffect>().id] = effect;
        }
    }

    public GameObject GetStatusEffect(string id)
    {
        GameObject prefab = effectMap[id];
        return Instantiate(prefab);
    }
}
