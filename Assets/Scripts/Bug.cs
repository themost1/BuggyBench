using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{
    [SerializeField]
    private string id;

    public string GetId()
    {
        return id;
    }
}
