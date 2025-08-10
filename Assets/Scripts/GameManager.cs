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
        string nextScene = GetNextSceneAfterDraft();
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

    public int GetNumBugs()
    {
        if (numDraftsDone == 3)
        {
            return 2;
        }
        else if (numDraftsDone == 4)
        {
            return 3;
        }
        return 4;
    }
}
