using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct InfluenceFamilyMemberInfo
{
    public FamilyMemberData TargetFamilyMember;
    [Range(-100, 100)] public int InfluenceAmount;
}

[CreateAssetMenu(fileName = "StoryCard", menuName = "StoryDeck/StoryCardData")]
public class StoryCardData : ScriptableObject
{
    public Sprite sprite;
    public Color backgroundColour = Color.white;
    public string description = "Enter card description here";

    public string yesText = "Yes";
    public string noText = "No";
    public bool hasCustomYesNoColours = false;
    public Color yesColour = Color.green;
    public Color noColour = Color.red;

    [NonReorderable] public List<InfluenceFamilyMemberInfo> OnYesFamilyMemberInfluences = new List<InfluenceFamilyMemberInfo>();
    [NonReorderable] public List<InfluenceFamilyMemberInfo> OnNoFamilyMemberInfluences = new List<InfluenceFamilyMemberInfo>();

    public List<StoryCardPack> onYesPacksToUnlock = new List<StoryCardPack>();
    public List<StoryCardPack> onNoPacksToUnlock = new List<StoryCardPack>();

    public FamilyMember onYesUnlockFamilyMemberSecret;
    public FamilyMember onNoUnlockFamilyMemberSecret;
}
