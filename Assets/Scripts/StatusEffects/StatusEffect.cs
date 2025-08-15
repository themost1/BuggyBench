using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect : MonoBehaviour
{
    public bool isNew = true;
    public string id = "statusEffect";
    public int num = 1;

    public virtual int GetDamage(int amt)
    {
        return amt;
    }
}
