using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safety : StatusEffect
{
    public override int GetPlayerDamage(int amt)
    {
        if (amt >= num)
        {
            num = 0;
            amt -= num;
            return amt;
        }

        num -= amt;
        amt = 0;
        return amt;
    }
}
