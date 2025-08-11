using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardUi : MonoBehaviour
{
    [SerializeField]
    private TMP_Text titleText,
        descriptionText,
        timesText;

    void Start()
    {
        titleText.text = GetComponent<Card>().GetTitle();
        descriptionText.text = GetComponent<Card>().GetDescription();
    }

    public void UpdateTimesText(int times)
    {
        if (times == 0)
        {
            timesText.text = "";
            return;
        }
        timesText.text = "x" + times.ToString();
    }
}
