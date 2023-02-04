using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FamilyMember
{
    public FamilyMemberData Data { get; private set; }

    public int Happiness { get; private set; }

    public bool IsSecretKnow { get; private set; }

    public enum HappinessLevels : int
    {
        Low,
        Medium,
        High
    }

    public HappinessLevels HappinessLevel 
    { 
        get
        {
            if (Happiness < Data.HappinessMax * 0.3f)
            {
                return HappinessLevels.Low;
            }
            else if (Happiness < Data.HappinessMax * 0.6f)
            {
                return HappinessLevels.Medium;
            }

            return HappinessLevels.High;
        }
    }

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
        Happiness = Data.HappinessMax / 2;
    }
}
