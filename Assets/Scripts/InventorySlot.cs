using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public string id;

    public Sprite baseSprite,
        selectedSprite;

    public bool selected = false;

    public int num = 3;

    [SerializeField]
    private TMP_Text numText;

    void OnMouseDown()
    {
        if (selected)
        {
            Deselect();
        }
        else
        {
            Select();
        }
    }

    public void Select()
    {
        if (num <= 0)
        {
            return;
        }
        selected = true;
        GetComponent<SpriteRenderer>().sprite = selectedSprite;
        ServiceLocator.inventory.OnSlotSelect(this);
    }

    public void Deselect()
    {
        selected = false;
        GetComponent<SpriteRenderer>().sprite = baseSprite;
    }

    void Update()
    {
        numText.text = num.ToString();
    }
}
