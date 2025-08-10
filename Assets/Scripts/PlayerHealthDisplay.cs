using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealthDisplay : MonoBehaviour
{
    [SerializeField]
    private TMP_Text healthText;

    void Update()
    {
        healthText.text = ServiceLocator.player.health.ToString();
    }
}
