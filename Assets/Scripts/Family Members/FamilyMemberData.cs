using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FamilyMemberData", menuName = "FamilyMemberData")]
public class FamilyMemberData : ScriptableObject
{
    public string FamilyMemberName;
    public string FamilyMemberRelation;
    public Sprite Sprite;
    public Sprite secretSprite;
    public string secret;
    public bool hasSecret;

    public int HappinessMax = 100;
}
