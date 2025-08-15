using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int numDraftsDone = 0;
    public int craftTurn = 0;

    private bool won = false,
        lost = false;

    void Awake()
    {
        ServiceLocator.gameManager = this;
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && (won || lost))
        {
            Restart();
        }
    }

    private void Restart()
    {
        numDraftsDone = 0;
        craftTurn = 0;
        ServiceLocator.inventory.ClearAllResources();
        ServiceLocator.player.Reset();
        won = false;
        lost = false;
        SceneManager.LoadScene("DraftScene");
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
        ServiceLocator.player.MarkStatusEffectsNotNew();
        int additionalBasics = ServiceLocator.player.GetNumAdditionalRandomBasics();
        ServiceLocator.inventory.GenerateRandomBasics(craftTurn + 5 + additionalBasics);
    }

    public void PostCraft()
    {
        if (ServiceLocator.player.health <= 0)
        {
            lost = true;
        }
        else if (ServiceLocator.craftingTable.HasNoEnemies() && InFinalFight())
        {
            won = true;
        }
    }

    public bool Lost()
    {
        return lost;
    }

    public bool Won()
    {
        return won;
    }
}
