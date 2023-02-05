using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    #region singleton
    public static GameManager Instance { get => instance; }

    private static GameManager instance;
    #endregion

    public delegate void OnGameLostDelegate();
    public static event OnGameLostDelegate OnGameLost;
    public static UnityEvent OnGameWon;

    private bool reportedGameLose;

    private void Awake()
    {
        #region singleton awake
        if (instance != this && instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
        #endregion

        reportedGameLose = false;
    }

    private void Start() 
    {
        StoryDeckManager.Instance.GenerateNewCard();
    }

    public void CheckGameState(StoryCardData lastSubmittedStoryCardData)
    {
        CheckGameWin(lastSubmittedStoryCardData);

        CheckGameLose();
    }

    private void CheckGameWin(StoryCardData lastSubmittedStoryCardData)
    {
        foreach (var keyValuePair in FamilyManager.Instance.FamilyMembers)
            {
                if (keyValuePair.Value.Trust == keyValuePair.Value.Data.TrustMax)
                {
                    OnGameWon?.Invoke();
                    reportedGameLose = true;
                }
            }
    }

    private void CheckGameLose()
    {
        if (!reportedGameLose)
        {
            // If a single family member's trust reaches 0, game has been lost
            foreach (var keyValuePair in FamilyManager.Instance.FamilyMembers)
            {
                if (keyValuePair.Value.Trust == 0)
                {
                    OnGameLost?.Invoke();
                    reportedGameLose = true;
                }
            }
        }
    }

    private void OnWin()
    {

    }
    
}
