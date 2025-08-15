using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strength : StatusEffect
{
    public override int GetDamage(int amt)
    {
        return amt + 1;
    }
}
