using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceedButton : MonoBehaviour
{
    void OnMouseDown()
    {
        ServiceLocator.gameManager.LoadNextSceneAfterFight();
    }
}
