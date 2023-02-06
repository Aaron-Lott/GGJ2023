using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System.Linq;

public class StoryCard : MonoBehaviour
{
    [Header("Statics")]
    [SerializeField] private StoryCardFlingable storyCardFlingablePrefab;
    [SerializeField] private FamilyStatusBar familyStatusBar;
    [SerializeField] private Transform storyCardFlingableSpawnPosition;

    [Header("Fields")]
    public TMP_Text descriptionText;
    public Image backgroundImage;
    public TMP_Text yesText;
    public TMP_Text noText;

    [HideInInspector] public UnityEvent OnYesChosen;
    [HideInInspector] public UnityEvent OnNoChosen;

    public StoryCardData Data { get; private set; }

    [HideInInspector] public StoryCardFlingable currentFlingable;
    [HideInInspector] public StoryCardFlingable previousFlingable;

    private void OnEnable()
    {
        OnYesChosen.AddListener(OnYes);
        OnNoChosen.AddListener(OnNo);
    }

    private void OnDisable()
    {
        OnYesChosen.RemoveListener(OnYes);
        OnNoChosen.RemoveListener(OnNo);
    }

    public void SetupCard(StoryCardData storyCardData)
    {
        Data = storyCardData;
        //backgroundImage.color = Data.backgroundColour;
        descriptionText.text = Data.description;
        yesText.text = Data.yesText;
        noText.text = Data.noText;

        CreateFlingable();
    }

    private void CreateFlingable()
    {
        if (currentFlingable)
            Destroy(currentFlingable.gameObject, 1);

        currentFlingable = Instantiate(storyCardFlingablePrefab, storyCardFlingableSpawnPosition);
        currentFlingable.familyMemberImage.sprite = Data.sprite;
    }

    private void OnYes()
    {
        InfluenceFamilyMembers(Data.OnYesFamilyMemberInfluences);

        if (Data.OnYesTargetFamilyMemberToUnlockSecret != null)
        {
            GameManager.Instance.TriggerWin(Data.OnYesTargetFamilyMemberToUnlockSecret);
        }
        else if (GameManager.Instance.CheckGameState())
        {
            return;
        }

        StoryDeckManager.Instance.AddUnlockablePacksToDeck(Data.OnYesPacksToUnlock);
        StoryDeckManager.Instance.GenerateNewCard();
    }

    private void OnNo()
    {
        InfluenceFamilyMembers(Data.OnNoFamilyMemberInfluences);

        if (Data.OnNoTargetFamilyMemberToUnlockSecret != null)
        {
            GameManager.Instance.TriggerWin(Data.OnNoTargetFamilyMemberToUnlockSecret);
        }
        else if (GameManager.Instance.CheckGameState())
        {
            return;
        }

        StoryDeckManager.Instance.AddUnlockablePacksToDeck(Data.OnNoPacksToUnlock);
        StoryDeckManager.Instance.GenerateNewCard();
    }

    private void InfluenceFamilyMembers(List<InfluenceFamilyMemberInfo> influenceFamilyMemberInfo)
    {
        foreach (var keyValuePair in FamilyManager.Instance.FamilyMembers)
        {
            foreach (InfluenceFamilyMemberInfo info in influenceFamilyMemberInfo)
            {
                if (keyValuePair.Key == info.TargetFamilyMember)
                {
                    keyValuePair.Value.InfluenceTrust(info.InfluenceAmount);
                    familyStatusBar.UpdateFamilyMemberHappinesUI(keyValuePair.Value);
                }
            }
        }
    }
}
