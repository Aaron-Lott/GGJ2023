using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FamilyManager : MonoBehaviour
{
    [SerializeField] private bool buildFamily;
    public List<FamilyMemberData> allFamilyMembers;

    #region singleton
    public static FamilyManager Instance { get => instance; }

    private static FamilyManager instance;
    #endregion

    public Dictionary<FamilyMemberData, FamilyMember> FamilyMembers { get; private set; } = new Dictionary<FamilyMemberData, FamilyMember>();
    public Dictionary<FamilyMemberData, FamilyMember> AllFamilyMembers { get; private set; } = new Dictionary<FamilyMemberData, FamilyMember>();

    public const int familyMemberAmount = 4;


    private void Awake()
    {
        #region singleton awake
        if (instance != this && instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        #endregion
        
        PopulateAllFamily();
        
        if (buildFamily) BuildFamily();
    }

    private void BuildFamily()
    {
        List<FamilyMemberData> familyMembersToCreate = new List<FamilyMemberData>();
        List<FamilyMemberData> familyMembersAvailable = new List<FamilyMemberData>();
        familyMembersAvailable.AddRange(allFamilyMembers);

        // Get random family members.
        for (int i = 0; i < familyMemberAmount; i++)
        {
            int index = Random.Range(0, familyMembersAvailable.Count);
            familyMembersToCreate.Add(familyMembersAvailable[index]);
            familyMembersAvailable.RemoveAt(index);
        }

        // Build active family.
        foreach (FamilyMemberData familyMemberData in familyMembersToCreate)
        {
            FamilyMember newFamilyMember = new FamilyMember(familyMemberData);

            FamilyMembers.Add(newFamilyMember.Data, newFamilyMember);
        }
    }

    public void PopulateAllFamily()
    {
        // Build all family.
        foreach (FamilyMemberData familyMemberData in allFamilyMembers)
        {
            FamilyMember newFamilyMember = new FamilyMember(familyMemberData);

            AllFamilyMembers.Add(newFamilyMember.Data, newFamilyMember);
        }
    }

    public void ResetFamilyMembers()
    {
        foreach (var keyValuePair in FamilyMembers)
        {
            keyValuePair.Value.ResetTrust();
        }
    }

    void Update()
    {
        //DebugFamilyMembersCreated();
    }

    public FamilyMember TryGetFamilyMember(FamilyMemberData familyMemberType)
    {
        if (FamilyMembers.ContainsKey(familyMemberType))
            return FamilyMembers[familyMemberType];
        else
            return null;
    }


    bool doOnceDebugFamilyMembersCreated = true;

    private void DebugFamilyMembersCreated()
    {
        if (doOnceDebugFamilyMembersCreated)
        {
            foreach (var fm in FamilyMembers)
            {
                Debug.Log(fm.Value.Data.FamilyMemberName);
            }

            doOnceDebugFamilyMembersCreated = false;
        }
    }
}
