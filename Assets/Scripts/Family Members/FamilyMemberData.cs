using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FamilyMembers : int
{
    Mum,
    Dad,
    Brother,
    Sister,
    Dog,
    Cat,
    Grandad,

    Uncle
}

[CreateAssetMenu(fileName = "FamilyMemberData", menuName = "FamilyMemberData")]
public class FamilyMemberData : ScriptableObject
{
    public FamilyMembers FamilyMemberType;
    
    public bool hasSecret;
    public string FamilyMemberName;
    public Sprite Sprite;
    public Sprite secretSprite;
    public string secret;

    public int HappinessMax = 100;
}
