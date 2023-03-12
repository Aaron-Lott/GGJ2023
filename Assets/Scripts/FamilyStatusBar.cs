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
            portrait.happinessImage.fillAmount = ((float)familyMember.Value.Trust / (float)familyMember.Value.Data.TrustMax);

            Color happinessColor;
            switch (familyMember.Value.TrustLevel)
            {
                case FamilyMember.TrustLevels.Low:
                    happinessColor = Color.red;
                    break;
                case FamilyMember.TrustLevels.Medium:
                    happinessColor = Color.yellow;
                    break;
                case FamilyMember.TrustLevels.High:
                default:
                    happinessColor = Color.green;
                    break;
            }
            portrait.happinessImage.color = happinessColor;

            portrait.labelNameText.text = familyMember.Value.Data.FamilyMemberName;
            portrait.labelTypeText.text = "(" + familyMember.Value.Data.FamilyMemberRelation + ")";

            familyMemberPortraits.Add(portrait);

            index ++;
        }
    }

    public IEnumerator UpdateFamilyMemberHappinessUI(FamilyMember familyMember, int changeInTrust)
    {
        foreach (FamilyMemberPortrait portrait in familyMemberPortraits)
        {
            if (portrait.familyMemberType == familyMember.Data)
            {
                portrait.happinessImage.fillAmount = ((float)familyMember.Trust / (float)familyMember.Data.TrustMax);

                if (changeInTrust > 0) portrait.happinessImage.color = Color.green;
                if (changeInTrust < 0) portrait.happinessImage.color = Color.red;

                yield return new WaitForSecondsRealtime(0.8f);

                Color happinessColor;
                switch (familyMember.TrustLevel)
                {
                    case FamilyMember.TrustLevels.Low:
                        happinessColor = Color.red;
                        break;
                    case FamilyMember.TrustLevels.Medium:
                        happinessColor = Color.yellow;
                        break;
                    case FamilyMember.TrustLevels.High:
                    default:
                        happinessColor = Color.green;
                        break;
                }
                portrait.happinessImage.color = happinessColor;
            }
        }
    }

    public void SkipUpdateFamilyMemberHappinessUI(FamilyMember familyMember, int changeInTrust)
    {
        foreach (FamilyMemberPortrait portrait in familyMemberPortraits)
        {
            if (portrait.familyMemberType == familyMember.Data)
            {
                portrait.happinessImage.fillAmount = ((float)familyMember.Trust / (float)familyMember.Data.TrustMax);

                Color happinessColor;
                switch (familyMember.TrustLevel)
                {
                    case FamilyMember.TrustLevels.Low:
                        happinessColor = Color.red;
                        break;
                    case FamilyMember.TrustLevels.Medium:
                        happinessColor = Color.yellow;
                        break;
                    case FamilyMember.TrustLevels.High:
                    default:
                        happinessColor = Color.green;
                        break;
                }
                portrait.happinessImage.color = happinessColor;
            }
        }
    }
}
