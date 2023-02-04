using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FamilyStatusBar : MonoBehaviour
{
    public FamilyMemberPortrait familyMemberPortrait;

    public void Start()
    {
        int index = 0;

        foreach (var familyMember in FamilyManager.Instance.FamilyMembers)
        {
            // Add new family member to status bar.
            FamilyMemberPortrait portrait = Instantiate(familyMemberPortrait.gameObject, transform).GetComponent<FamilyMemberPortrait>();
            
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

            portrait.labelText.text = familyMember.Value.Data.FamilyMemberName;
    
            index ++;
        }
    }
}
