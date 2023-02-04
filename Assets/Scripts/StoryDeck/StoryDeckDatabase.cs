using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StoryDeckDatabase", menuName = "StoryDeck/StoryDeckDatabase")]
public class StoryDeckDatabase : ScriptableObject
{
    public StoryCardPack BasePack;

    public List<StoryCardPack> UnlockablePacks = new List<StoryCardPack>();
}
