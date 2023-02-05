using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FamilyMemberData", menuName = "FamilyMemberData")]
public class FamilyMemberData : ScriptableObject
{
    public string FamilyMemberName;
    public string FamilyMemberRelation;
    public Sprite Sprite;

    public bool hasSecret;
    public Sprite secretSprite;
    public string secret;

    public int TrustMax = 100;
}
