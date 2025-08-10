using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int numDraftsDone = 0;

    void Awake()
    {
        ServiceLocator.gameManager = this;
        DontDestroyOnLoad(this);
    }

    public void LoadNextSceneAfterDraft()
    {
        numDraftsDone++;
        SceneManager.LoadScene(GetNextSceneAfterDraft());
    }

    private string GetNextSceneAfterDraft()
    {
        if (numDraftsDone < 3)
        {
            return "DraftScene";
        }
        return "FightScene";
    }
}
