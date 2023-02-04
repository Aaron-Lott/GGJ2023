using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class StoryCard : MonoBehaviour
{
    public RectTransform cardImageTransform;
    public Rigidbody2D cardImageRigidbody2D;
    public Image cardImage;

    public Image backgroundImage;
    public TMP_Text yesText;
    public TMP_Text noText;
    public TMP_Text storyText;

    public UnityEvent OnYesChosen;
    public UnityEvent OnNoChosen;

    public StoryCardData Data { get; private set; }

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
        cardImage.sprite = storyCardData.sprite;
        backgroundImage.color = storyCardData.backgroundColour;
        storyText.text = storyCardData.description;
        yesText.text = storyCardData.yesText;
        noText.text = storyCardData.noText;
    }

    private void OnYes()
    {
        StoryDeckManager.Instance.AddUnlockablePacksToDeck(Data.onYesPacksToUnlock);
        StoryDeckManager.Instance.GenerateNewCard();
    }

    private void OnNo()
    {
        StoryDeckManager.Instance.AddUnlockablePacksToDeck(Data.onNoPacksToUnlock);
        StoryDeckManager.Instance.GenerateNewCard();
    }
}
