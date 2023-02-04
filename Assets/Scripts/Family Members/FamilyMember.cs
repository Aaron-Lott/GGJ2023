using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FamilyMember
{
    public FamilyMemberData Data { get; private set; }

    public int Happiness { get; private set; }

    public FamilyMember (FamilyMemberData familyMemberData)
    {
        Data = familyMemberData;

        ResetHappiness();
    }

    public void InfluenceHappiness(int changeInHappiness)
    {
        Happiness += changeInHappiness;

        if (Happiness < 0) Happiness = 0;
        if (Happiness > Data.HappinessMax) Happiness = Data.HappinessMax;
    }

    public void ResetHappiness()
    {
        Happiness = Data.HappinessMax;
    }
}
