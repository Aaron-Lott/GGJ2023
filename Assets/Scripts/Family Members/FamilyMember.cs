using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FamilyMember
{
    public FamilyMemberData Info { get; private set; }

    public int Happiness { get; private set; }

    public FamilyMember (FamilyMemberData familyMemberData)
    {
        Info = familyMemberData;

        ResetHappiness();
    }

    public void InfluenceHappiness(int changeInHappiness)
    {
        Happiness += changeInHappiness;

        if (Happiness < 0) Happiness = 0;
        if (Happiness > Info.HappinessMax) Happiness = Info.HappinessMax;
    }

    public void ResetHappiness()
    {
        Happiness = Info.HappinessMax;
    }
}
