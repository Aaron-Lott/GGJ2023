using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int minimumStableTrustValue = 15;
    [SerializeField] private int minimumUntrustingFamilyMembersToLose = 2;

    #region singleton
    public static GameManager Instance { get => instance; }

    private static GameManager instance;
    #endregion

    public delegate void OnGameStateChangedDelegate();
    public static event OnGameStateChangedDelegate OnGameWon;
    public static event OnGameStateChangedDelegate OnGameDraw;
    public static event OnGameStateChangedDelegate OnGameLost;

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
    }

    private void Start() 
    {
        StoryDeckManager.Instance.GenerateNewCard();
    }

    /// <returns>Whether checking the game state resulted in a game-end scenario</returns>
    public bool CheckGameState()
    {
        if (CheckGameLose()) return true;

        if (CheckGameDraw()) return true;

        return false;
    }

    private bool CheckGameLose()
    {
        // Total family members with their turst below the minimum stable trust value
        int totalUntrustingFamilyMembers = 0;
        foreach (var keyValuePair in FamilyManager.Instance.FamilyMembers)
        {
            if (keyValuePair.Value.Trust < minimumStableTrustValue)
            {
                totalUntrustingFamilyMembers++;
            }
        }

        // Check if total exceeds minimum to lose game
        if (totalUntrustingFamilyMembers >= minimumUntrustingFamilyMembersToLose)
        {
            OnGameLost?.Invoke();
            return true;
        }
        return false;
    }

    private bool CheckGameDraw()
    {
        if (StoryDeckManager.Instance.CurrentDeck.Count <= 0)
        {
            OnGameDraw?.Invoke();
            return true;
        }
        return false;
    }

    public void TriggerWin(FamilyMemberData winningFamilyMember)
    {
        FamilyMember familyMember = FamilyManager.Instance.TryGetFamilyMember(winningFamilyMember);
        if (familyMember != null)
            familyMember.IsSecretUnlocked = true;

        OnGameWon?.Invoke();
    }
    
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
