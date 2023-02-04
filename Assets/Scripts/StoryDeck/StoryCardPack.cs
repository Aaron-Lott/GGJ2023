using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardPack", menuName = "StoryDeck/StoryCardPack")]
public class StoryCardPack : ScriptableObject
{
    public bool isInitiallyUnlocked;

    public List<StoryCardData> Cards = new List<StoryCardData>();
}
