using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StoryCard", menuName = "StoryDeck/StoryCardData")]
public class StoryCardData : ScriptableObject
{
    public Sprite sprite;
    public Color backgroundColour = Color.white;
    public string description;

    public bool hasCustomYesNoColours = false;
    public Color yesColour = Color.green;
    public Color noColour = Color.red;

    
}
