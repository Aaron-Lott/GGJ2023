using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public delegate void OnGameStateChangedDelegate(FamilyMemberData winningFamilyMemberData, FamilyMemberData losingFamilyMemberData);
    public static event OnGameStateChangedDelegate OnGameWon;
    public static event OnGameStateChangedDelegate OnGameDraw;
    public static event OnGameStateChangedDelegate OnGameLost;

    private void Awake()
    {
        #region singleton awake
        if (instance != this && instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        #endregion
    }

    private void Start() 
    {
        bool beganGameWithValidFirstCard = StoryDeckManager.Instance.GenerateNewCard();

        if (!beganGameWithValidFirstCard)
        {
            Debug.LogError("No cards able to be generated on first turn. Please check deck setup!");
            OnGameDraw?.Invoke(null, null);
        }
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
        int currentyLowestTrust = int.MaxValue;
        FamilyMemberData lowestFamilyMember = null;
        foreach (var keyValuePair in FamilyManager.Instance.FamilyMembers)
        {
            if (keyValuePair.Value.Trust < minimumStableTrustValue)
            {
                totalUntrustingFamilyMembers++;
            }

            if (keyValuePair.Value.Trust < currentyLowestTrust)
            {
                lowestFamilyMember = keyValuePair.Key;
                currentyLowestTrust = keyValuePair.Value.Trust;
            }
        }

        // Check if total exceeds minimum to lose game
        if (totalUntrustingFamilyMembers >= minimumUntrustingFamilyMembersToLose)
        {
            OnGameLost?.Invoke(null, lowestFamilyMember);
            return true;
        }
        return false;
    }

    private bool CheckGameDraw()
    {
        if (StoryDeckManager.Instance.GetDrawableCards().Count <= 0 && !StoryDeckManager.Instance.GetNextCardIsDirectCard())
        {
            OnGameDraw?.Invoke(null, null);
            return true;
        }
        return false;
    }

    public void TriggerWin(FamilyMemberData winningFamilyMember)
    {
        FamilyMember familyMember = FamilyManager.Instance.TryGetFamilyMember(winningFamilyMember);
        if (familyMember != null)
        {
            familyMember.IsSecretUnlocked = true;

            int currentyLowestTrust = int.MaxValue;
            FamilyMemberData lowestFamilyMember = null;
            foreach (var keyValuePair in FamilyManager.Instance.FamilyMembers)
            {
                if (keyValuePair.Key == winningFamilyMember)
                    continue;

                if (keyValuePair.Value.Trust < currentyLowestTrust)
                {
                    lowestFamilyMember = keyValuePair.Key;
                    currentyLowestTrust = keyValuePair.Value.Trust;
                }
            }

            OnGameWon?.Invoke(winningFamilyMember, lowestFamilyMember);
        }
    }
    
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
