using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{
    [SerializeField]
    private string id;

    public Vector2 lastLocation;

    public string GetId()
    {
        return id;
    }

    public void MoveTo(Vector2 loc, Vector2 oldLoc)
    {
        transform.DOMove(
            transform.position + new Vector3(loc.x - oldLoc.x, loc.y - oldLoc.y, 0),
            1.2f
        );
    }
}
