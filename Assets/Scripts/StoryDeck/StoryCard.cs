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
        backgroundImage.color = Data.backgroundColour;
        descriptionText.text = Data.description;
        yesText.text = Data.yesText;
        noText.text = Data.noText;

        StartCoroutine(CreateFlingable());
    }

    private IEnumerator CreateFlingable()
    {
        previousFlingable = currentFlingable;

        currentFlingable = StoryCardFlingable.Instantiate(storyCardFlingablePrefab, storyCardFlingableSpawnPosition);
        currentFlingable.familyMemberImage.sprite = Data.sprite;

        // Yield before destroying previous flingable.
        yield return new WaitForSeconds(1);

        if (previousFlingable)
        {
            Destroy(previousFlingable.gameObject);
        }
    }

    private void OnYes()
    {
        StoryDeckManager.Instance.AddUnlockablePacksToDeck(Data.onYesPacksToUnlock);
        StoryDeckManager.Instance.GenerateNewCard();

        // Update family member happiness.
        foreach (var familyMember in FamilyManager.Instance.FamilyMembers)
        {
            if (Data.familyMembersInFavour.Contains(familyMember.Key))
            {
                familyMember.Value.InfluenceHappiness(Data.positiveInfluence);
                familyStatusBar.UpdateFamilyMemberHappinesUI(familyMember.Value);
            }
            else if (Data.familyMembersAgainst.Contains(familyMember.Key))
            {
                familyMember.Value.InfluenceHappiness(-Data.negativeInfluence);
                familyStatusBar.UpdateFamilyMemberHappinesUI(familyMember.Value);
            }
        }
    }

    private void OnNo()
    {
        StoryDeckManager.Instance.AddUnlockablePacksToDeck(Data.onNoPacksToUnlock);
        StoryDeckManager.Instance.GenerateNewCard();

        // Update family member happiness.
        foreach (var familyMember in FamilyManager.Instance.FamilyMembers)
        {
            if (Data.familyMembersInFavour.Contains(familyMember.Key))
            {
                familyMember.Value.InfluenceHappiness(-Data.negativeInfluence);
                familyStatusBar.UpdateFamilyMemberHappinesUI(familyMember.Value);
            }
            else if (Data.familyMembersAgainst.Contains(familyMember.Key))
            {
                familyMember.Value.InfluenceHappiness(Data.positiveInfluence);
                familyStatusBar.UpdateFamilyMemberHappinesUI(familyMember.Value);
            }
        }
    }
}
