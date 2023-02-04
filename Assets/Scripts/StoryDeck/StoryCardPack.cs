using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryCardPack : ScriptableObject
{
    public List<StoryCardData> UnlockRequirements = new List<StoryCardData>();

    public List<StoryCardData> Cards = new List<StoryCardData>();
}
