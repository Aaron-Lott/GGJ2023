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
        DontDestroyOnLoad(gameObject);
        #endregion

        ResetDeck();
    }

    private void ResetDeck()
    {
        CurrentDeck.Clear();

        foreach (StoryCardData cardData in storyDeckDatabase.BasePack.Cards)
        {
            CurrentDeck.Add(cardData);
        }

        foreach (StoryCardPack cardPack in storyDeckDatabase.UnlockablePacks)
        {
            if (cardPack.isInitiallyUnlocked)
            {
                foreach (StoryCardData cardData in cardPack.Cards)
                {
                    CurrentDeck.Add(cardData);
                }
            }
        }
    }

    public void AddUnlockablePacksToDeck(List<StoryCardPack> cardPacks)
    {
        foreach (StoryCardPack cardPack in cardPacks)
        {
            foreach (StoryCardData cardData in cardPack.Cards)
            {
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

        int randomCardIndex = Random.Range(0, CurrentDeck.Count);

        storyCard.SetupCard(CurrentDeck[randomCardIndex]);
        
        CurrentDeck.Remove(CurrentDeck[randomCardIndex]);
    }
}
