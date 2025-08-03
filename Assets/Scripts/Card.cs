using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private string[][] recipe;

    void Start()
    {
        SetRecipe();
        GenerateImage();
    }

    public virtual void SetRecipe() {}

    public void GenerateImage() {}
}
