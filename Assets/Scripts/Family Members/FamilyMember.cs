using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FamilyMember
{
    public FamilyMemberData Data { get; private set; }

    public int Trust { get; private set; }

    public bool IsSecretKnow { get; private set; }

    public enum TrustLevels : int
    {
        Low,
        Medium,
        High
    }

    public TrustLevels TrustLevel 
    { 
        get
        {
            if (Trust < Data.TrustMax * 0.3f)
            {
                return TrustLevels.Low;
            }
            else if (Trust < Data.TrustMax * 0.6f)
            {
                return TrustLevels.Medium;
            }

            return TrustLevels.High;
        }
    }

    public FamilyMember (FamilyMemberData familyMemberData)
    {
        Data = familyMemberData;

        ResetTrust();
    }

    public void InfluenceTrust(int changeInTrust)
    {
        Trust += changeInTrust;

        if (Trust < 0) Trust = 0;
        if (Trust > Data.TrustMax) Trust = Data.TrustMax;
    }

    public void ResetTrust()
    {
        Trust = Data.TrustMax / 2;
    }
}
