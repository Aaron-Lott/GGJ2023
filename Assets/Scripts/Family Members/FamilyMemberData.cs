using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FamilyMembers : int
{
    Mum,
    Dad,
    Son,
    Daughter,
}

[CreateAssetMenu(fileName = "FamilyMemberData", menuName = "FamilyMemberData")]
public class FamilyMemberData : ScriptableObject
{
    public FamilyMembers FamilyMemberType;

    public string FamilyMemberName;
    public Sprite Sprite;

    public int HappinessMax = 100;
}
