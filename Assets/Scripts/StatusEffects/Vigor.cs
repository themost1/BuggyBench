using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vigor : StatusEffect
{
    public override int GetNumAdditionalRandomBasics()
    {
        return num;
    }
}
