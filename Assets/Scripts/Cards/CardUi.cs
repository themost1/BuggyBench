using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardUi : MonoBehaviour
{
    [SerializeField]
    private TMP_Text titleText,
        descriptionText;

    void Start()
    {
        titleText.text = GetComponent<Card>().GetTitle();
        descriptionText.text = GetComponent<Card>().GetDescription();
    }
}
