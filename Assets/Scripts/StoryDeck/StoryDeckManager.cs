using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryDeckManager : MonoBehaviour
{
    [SerializeField] private StoryDeckDatabase storyDeckDatabase;

    [Header("Card Instantiation")]
    public StoryCard storyCard;

    #region singleton
    public static StoryDeckManager Instance { get => instance; }

    private static StoryDeckManager instance;
    #endregion

    public List<StoryCardData> CurrentDeck { get; private set; } = new List<StoryCardData>();

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
        #endregion
    }

    private void Start()
    {
        ResetDeck();
    }

    private void ResetDeck()
    {
        CurrentDeck.Clear();

        AddCardFromPack(storyDeckDatabase.BasePack);

        foreach (StoryCardPack cardPack in storyDeckDatabase.UnlockablePacks)
        {
            if (cardPack.isInitiallyUnlocked)
            {
                AddCardFromPack(cardPack);
            }
        }
    }

    public void AddUnlockablePacksToDeck(List<StoryCardPack> cardPacks)
    {
        foreach (StoryCardPack cardPack in cardPacks)
        {
            AddCardFromPack(cardPack);
        }
    }

    private void AddCardFromPack(StoryCardPack cardPack)
    {
        foreach (StoryCardData cardData in cardPack.Cards)
        {
            if (cardData != null)
            {
                bool allRequiredMemberArePresent = true;
                foreach (FamilyMemberData familyMember in cardData.FamilyMembersRequired)
                {
                    if (FamilyManager.Instance.TryGetFamilyMember(familyMember) == null)
                        allRequiredMemberArePresent = false;

                }

                if (allRequiredMemberArePresent)
                    CurrentDeck.Add(cardData);
            }
        }
    }

    public void GenerateNewCard()
    {
        if (CurrentDeck.Count <= 0)
        {
            return;
        }

        List<StoryCardData> drawableCards = new List<StoryCardData>();
        foreach (StoryCardData card in CurrentDeck)
        {
            if (card.CardDrawAvailabilityTrustRequirements.Count > 0)
            {
                bool anyTrustRequirementsNotMet = false;
                foreach(TrustRequirementInfo trustRequirementInfo in card.CardDrawAvailabilityTrustRequirements)
                {
                    FamilyMember familyMember = FamilyManager.Instance.TryGetFamilyMember(trustRequirementInfo.TargetFamilyMember);
                    if (familyMember != null)
                    {
                        switch (trustRequirementInfo.FilterType)
                        {
                            case TrustRequirementInfo.TrustRequirementFilterType.GreaterThanMinimumOnly:
                                if (familyMember.Trust >= trustRequirementInfo.MinimumTrustRequirement)
                                {
                                    anyTrustRequirementsNotMet = true;
                                }
                                break;
                            case TrustRequirementInfo.TrustRequirementFilterType.LessThanMaximumOnly:
                                if (familyMember.Trust <= trustRequirementInfo.MaximumTrustRequirement)
                                {
                                    anyTrustRequirementsNotMet = true;
                                }
                                break;
                            case TrustRequirementInfo.TrustRequirementFilterType.WithinMinimumAndMaximum:
                                if (familyMember.Trust >= trustRequirementInfo.MinimumTrustRequirement
                                    && familyMember.Trust <= trustRequirementInfo.MaximumTrustRequirement)
                                {
                                    anyTrustRequirementsNotMet = true;
                                }
                                break;
                        }
                    }
                }

                // Only add drawable cards that meet all trust requirements
                if (!anyTrustRequirementsNotMet)
                    drawableCards.Add(card);
            }
            else
            {
                drawableCards.Add(card);
            }
        }

        int randomCardIndex = Random.Range(0, drawableCards.Count);

        storyCard.SetupCard(drawableCards[randomCardIndex]);
        
        CurrentDeck.Remove(CurrentDeck[randomCardIndex]);
    }
}
