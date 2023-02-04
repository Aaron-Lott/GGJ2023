using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StoryDeckDatabase", menuName = "StoryDeck/StoryDeckDatabase")]
public class StoryDeckDatabase : ScriptableObject
{
    public List<StoryCardData> Database = new List<StoryCardData>();
}
