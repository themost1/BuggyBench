using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusEffectUi : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;

    void Update()
    {
        int num = GetComponent<StatusEffect>().num;
        text.text = num.ToString();
    }
}
