using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect : MonoBehaviour
{
    public string id = "statusEffect";
    public int num = 1;

    public virtual int GetDamage(int amt)
    {
        return amt;
    }

    public virtual int GetNumAdditionalRandomBasics()
    {
        return 0;
    }

    public virtual int GetPlayerDamage(int amt)
    {
        return amt;
    }
}
