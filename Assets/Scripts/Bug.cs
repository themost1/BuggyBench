using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bug : MonoBehaviour
{
    [SerializeField]
    private string id;

    public Vector2 lastLocation;

    public int health = 10;
    public int attack = 3;

    [SerializeField]
    private TMP_Text text;

    public string GetId()
    {
        return id;
    }

    public void MoveTo(Vector3 pos, Vector2 gridLoc, Vector2 oldLoc)
    {
        transform.DOMove(pos, 0.3f).OnComplete(() => SetGridLoc(gridLoc, oldLoc));
    }

    private void SetGridLoc(Vector2 loc, Vector2 oldLoc)
    {
        ServiceLocator.craftingTable.GetSlots()[(int)oldLoc.x][(int)oldLoc.y].SetBug(null);
        ServiceLocator.craftingTable.GetSlots()[(int)loc.x][(int)loc.y].SetBug(gameObject);
    }

    void Update()
    {
        text.text = attack + "<sprite name=\"Attack\">  " + health + "<sprite name=\"Health\">";
    }

    public void TakeDamage(int amt)
    {
        health = Mathf.Max(0, health - amt);
        if (health == 0)
        {
            DestroyImmediate(this.gameObject);
        }
    }
}
