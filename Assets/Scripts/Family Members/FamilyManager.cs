using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FamilyManager : MonoBehaviour
{
    [SerializeField] private List<FamilyMemberData> familyMembersToCreate;

    public delegate void OnGameLoseDelegate();
    public static event OnGameLoseDelegate OnGameLose;

    private Dictionary<FamilyMembers, FamilyMember> FamilyMembers = new Dictionary<FamilyMembers, FamilyMember>();

    private bool reportedGameLose;

    private void Awake()
    {
        reportedGameLose = false;
    }

    void Start()
    {
        foreach (FamilyMemberData familyMemberData in familyMembersToCreate)
        {
            FamilyMember newFamilyMember = new FamilyMember(familyMemberData);

            FamilyMembers.Add(newFamilyMember.Info.FamilyMemberType, newFamilyMember);
        }
    }

    public void ResetFamilyMembers()
    {
        foreach (var keyValuePair in FamilyMembers)
        {
            keyValuePair.Value.ResetHappiness();
        }

        reportedGameLose = false;
    }

    void Update()
    {
        DebugFamilyMembersCreated();
    }

    private void CheckGameLose()
    {
        if (!reportedGameLose)
        {
            foreach (var keyValuePair in FamilyMembers)
            {
                if (keyValuePair.Value.Happiness == 0)
                {
                    OnGameLose?.Invoke();
                    reportedGameLose = true;
                }
            }
        }
    }

    public FamilyMember TryGetFamilyMember(FamilyMembers familyMemberType)
    {
        if (FamilyMembers.ContainsKey(familyMemberType))
            return FamilyMembers[familyMemberType];
        else
            return null;
    }

    public void InfluenceFamilyMember(FamilyMembers familyMemberType, int changeInHappiness)
    {
        FamilyMember familyMember = TryGetFamilyMember(familyMemberType);

        if (familyMember != null)
        {
            familyMember.InfluenceHappiness(changeInHappiness);
        }

        CheckGameLose();
    }


    bool doOnceDebugFamilyMembersCreated = true;

    private void DebugFamilyMembersCreated()
    {
        if (doOnceDebugFamilyMembersCreated)
        {
            foreach (var fm in FamilyMembers)
            {
                Debug.Log(fm.Value.Info.FamilyMemberName);
            }

            doOnceDebugFamilyMembersCreated = false;
        }
    }
}
