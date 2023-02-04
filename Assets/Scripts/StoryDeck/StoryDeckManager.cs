using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryDeckManager : MonoBehaviour
{
    [SerializeField] private StoryDeckDatabase storyDeckDatabase;

    [Header("Card Instantiation")]
    [SerializeField] private StoryCard cardPrefab;
    [SerializeField] private Transform cardSpawnPoint;

    public StoryDeckManager Instance { get => instance; private set => instance = value; }

    private StoryDeckManager instance;

    private void Awake()
    {
        if (instance != this && instance != null)
        {
            Destroy(this);
        }
        else if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public StoryCard GenerateNewCard()
    {
        StoryCard newCard = Instantiate(cardPrefab, cardSpawnPoint.position, cardSpawnPoint.rotation);

        int randomCardIndex = Random.Range(0, storyDeckDatabase.Database.Count);
        newCard.SetupCard(storyDeckDatabase.Database[randomCardIndex]);

        return newCard;
    }
}
