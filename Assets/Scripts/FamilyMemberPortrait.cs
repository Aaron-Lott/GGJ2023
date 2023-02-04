using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class FamilyMemberPortrait : MonoBehaviour
{
    public Image happinessImage;
    public Image portraitImage;
    public TMP_Text labelNameText;
    public TMP_Text labelTypeText;
    
    [HideInInspector] public FamilyMembers familyMemberType;
}
