using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoryCard : MonoBehaviour
{
    public RectTransform cardImageTransform;
    public Rigidbody2D cardImageRigidbody2D;
    public Image cardImage;

    public Image backgroundImage;
    public TMP_Text yesText;
    public TMP_Text noText;

    public StoryCardData Data { get; private set; }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetupCard(StoryCardData storyCardData)
    {
        cardImage.sprite = storyCardData.sprite;
        backgroundImage.color = storyCardData.backgroundColour;
    }
}
