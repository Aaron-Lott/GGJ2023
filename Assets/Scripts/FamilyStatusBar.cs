using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FamilyStatusBar : MonoBehaviour
{
    public FamilyMemberPortrait familyMemberPortraitPrefab;

    private List<FamilyMemberPortrait> familyMemberPortraits = new List<FamilyMemberPortrait>();

    public void Start()
    {
        InitialiseFamilyPortraits();
    }

    public void InitialiseFamilyPortraits()
    {
        int index = 0;

        foreach (var familyMember in FamilyManager.Instance.FamilyMembers)
        {
            // Add new family member to status bar.
            FamilyMemberPortrait portrait = Instantiate(familyMemberPortraitPrefab.gameObject, transform).GetComponent<FamilyMemberPortrait>();

            // Set the family member portrait type.
            portrait.familyMemberType = familyMember.Value.Data;

            // Set family member portrait image.
            portrait.portraitImage.sprite = familyMember.Value.Data.Sprite;

            // Set family member happiness.
            portrait.happinessImage.fillAmount = ((float)familyMember.Value.Happiness / (float)familyMember.Value.Data.HappinessMax);

            Color happinessColor;

            switch (familyMember.Value.HappinessLevel)
            {
                case FamilyMember.HappinessLevels.Low:
                    happinessColor = Color.red;
                    break;
                case FamilyMember.HappinessLevels.Medium:
                    happinessColor = Color.yellow;
                    break;
                case FamilyMember.HappinessLevels.High:
                default:
                    happinessColor = Color.green;
                    break;
            }

            portrait.happinessImage.color = happinessColor;

            portrait.labelNameText.text = familyMember.Value.Data.FamilyMemberName;
            portrait.labelTypeText.text = "(" + familyMember.Value.Data.FamilyMemberType + ")";

            familyMemberPortraits.Add(portrait);

            index ++;
        }
    }

    public void UpdateFamilyMemberHappinesUI(FamilyMember familyMember)
    {
        foreach (FamilyMemberPortrait portrait in familyMemberPortraits)
        {
            if (portrait.familyMemberType == familyMember.Data)
            {
                portrait.happinessImage.fillAmount = ((float)familyMember.Happiness / (float)familyMember.Data.HappinessMax);
            }
        }
    }
}
