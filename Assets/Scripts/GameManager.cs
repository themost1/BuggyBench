using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int numDraftsDone = 0;
    public int craftTurn = 0;

    void Awake()
    {
        ServiceLocator.gameManager = this;
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void LoadNextSceneAfterDraft()
    {
        numDraftsDone++;
        string nextScene = GetNextSceneAfterDraft();
        SceneManager.LoadScene(GetNextSceneAfterDraft());
    }

    public void LoadNextSceneAfterFight()
    {
        SceneManager.LoadScene("DraftScene");
    }

    private string GetNextSceneAfterDraft()
    {
        if (numDraftsDone < 3)
        {
            return "DraftScene";
        }
        return "FightScene";
    }

    public bool InFinalFight()
    {
        return numDraftsDone >= 5;
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

    // Performs actions after a scene loads. `sceneType` should be set before
    // this function is called.
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "FightScene")
        {
            craftTurn = 0;
            ServiceLocator.inventory.GenerateRandomBasics(craftTurn + 5);
            ServiceLocator.inventory.gameObject.SetActive(true);
        }
        else
        {
            ServiceLocator.inventory.gameObject.SetActive(false);
        }
    }

    public void OnCraftEnd()
    {
        craftTurn++;
        ServiceLocator.inventory.GenerateRandomBasics(craftTurn + 5);
    }
}
