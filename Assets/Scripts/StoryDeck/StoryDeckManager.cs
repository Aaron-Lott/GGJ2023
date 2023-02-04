using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryDeckManager : MonoBehaviour
{
    [SerializeField] private StoryDeckDatabase storyDeckDatabase;

    [Header("Card Instantiation")]
    [SerializeField] private StoryCard cardPrefab;
    [SerializeField] private Transform cardSpawnPoint;

    #region singleton
    public static StoryDeckManager Instance { get => instance; }

    private static StoryDeckManager instance;
    #endregion

    private List<StoryCardData> currentDeck = new List<StoryCardData>();

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
        currentDeck.Clear();

        foreach (StoryCardData cardData in storyDeckDatabase.BasePack.Cards)
        {
            currentDeck.Add(cardData);
        }

        foreach (StoryCardPack cardPack in storyDeckDatabase.UnlockablePacks)
        {
            if (cardPack.isInitiallyUnlocked)
            {
                foreach (StoryCardData cardData in cardPack.Cards)
                {
                    currentDeck.Add(cardData);
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
                currentDeck.Add(cardData);
            }
        }
    }

    public StoryCard GenerateNewCard()
    {
        StoryCard newCard = Instantiate(cardPrefab, cardSpawnPoint.position, cardSpawnPoint.rotation);

        int randomCardIndex = Random.Range(0, currentDeck.Count);
        newCard.SetupCard(currentDeck[randomCardIndex]);
        currentDeck.Remove(currentDeck[randomCardIndex]);

        return newCard;
    }
}
