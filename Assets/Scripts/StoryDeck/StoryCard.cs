using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryCard : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Image backgroundImage;

    public StoryCardData Data { get; private set; }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetupCard(StoryCardData storyCardData)
    {
        image.sprite = storyCardData.sprite;
        backgroundImage.color = storyCardData.backgroundColour;
    }
}
