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

[Serializable]
public struct TrustRequirementInfo
{
    public enum TrustRequirementFilterType
    {
        GreaterThanMinimumOnly,
        LessThanMaximumOnly,
        WithinMinimumAndMaximum,
    }

    public FamilyMemberData TargetFamilyMember;
    public TrustRequirementFilterType FilterType;
    public int MinimumTrustRequirement;
    public int MaximumTrustRequirement;
}

[CreateAssetMenu(fileName = "StoryCard", menuName = "StoryDeck/StoryCardData")]
public class StoryCardData : ScriptableObject
{
    public List<FamilyMemberData> FamilyMembersRequired = new List<FamilyMemberData>();
    [NonReorderable] public List<TrustRequirementInfo> CardDrawAvailabilityTrustRequirements = new List<TrustRequirementInfo>();

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

    public List<StoryCardPack> OnYesPacksToUnlock = new List<StoryCardPack>();
    public List<StoryCardPack> OnNoPacksToUnlock = new List<StoryCardPack>();

    public FamilyMemberData OnYesTargetFamilyMemberToUnlockSecret;
    public FamilyMemberData OnNoTargetFamilyMemberToUnlockSecret;
}
