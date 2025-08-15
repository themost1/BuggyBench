using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banner : MonoBehaviour
{
    [SerializeField]
    private GameObject textObject;

    public void Display()
    {
        gameObject.SetActive(true);
        textObject.SetActive(true);
    }
}
