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
        cardImage.sprite = storyCardData.sprite;
        backgroundImage.color = storyCardData.backgroundColour;
    }

    private void OnYes()
    {
        StoryDeckManager.Instance.AddUnlockablePacksToDeck(Data.onYesPacksToUnlock);
    }

    private void OnNo()
    {
        StoryDeckManager.Instance.AddUnlockablePacksToDeck(Data.onNoPacksToUnlock);
    }
}
