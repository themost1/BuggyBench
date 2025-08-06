using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private string[][] recipe;

    [SerializeField]
    private string id;

    [SerializeField]
    private string title;

    [SerializeField]
    private string description;

    void Start()
    {
        recipe = GetRecipe();
        GenerateImage();
    }

    public virtual string[][] GetRecipe()
    {
        return new string[][]
        {
            new string[] { "", "", "", "" },
            new string[] { "", "", "", "" },
            new string[] { "", "", "", "" },
            new string[] { "", "", "", "" }
        };
    }

    public void GenerateImage()
    {
        int row = 0,
            col = 0;
        Vector3 topLeft = new Vector3(
            0.12f + recipe[0].Length / 2.0f * -0.25f,
            0.3f + 0.25f * recipe.Length / 2.0f,
            0f
        );
        foreach (string[] resourceRow in recipe)
        {
            foreach (string resource in resourceRow)
            {
                GameObject resourceObj = ServiceLocator.resourceCache.GetResource(resource);
                resourceObj.transform.SetParent(transform);
                resourceObj.transform.position = transform.position;
                resourceObj.GetComponent<Renderer>().sortingLayerName = "Card";
                resourceObj.GetComponent<Renderer>().sortingOrder = 1;
                resourceObj.transform.position +=
                    topLeft + new Vector3(0.25f * col, -0.25f * row, 0f);
                resourceObj.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                col++;
            }
            row++;
            col = 0;
        }
    }

    public string GetId()
    {
        return id;
    }

    public string GetTitle()
    {
        return title;
    }

    public string GetDescription()
    {
        return description;
    }
}
