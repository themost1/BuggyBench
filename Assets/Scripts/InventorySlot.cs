using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public string id;

    public GameObject resource;

    public Sprite baseSprite, selectedSprite;

    public bool selected = false;

    private int num = 0;

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
        selected = true;
        GetComponent<SpriteRenderer>().sprite = selectedSprite;
        ServiceLocator.inventory.OnSlotSelect(this);
    }

    public void Deselect()
    {
        selected = false;
        GetComponent<SpriteRenderer>().sprite = baseSprite;
    }
}
