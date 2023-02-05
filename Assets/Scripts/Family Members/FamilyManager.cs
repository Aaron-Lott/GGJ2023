using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FamilyManager : MonoBehaviour
{
    [SerializeField] private List<FamilyMemberData> allFamilyMembers;

    #region singleton
    public static FamilyManager Instance { get => instance; }

    private static FamilyManager instance;
    #endregion

    public Dictionary<FamilyMemberData, FamilyMember> FamilyMembers { get; private set; } = new Dictionary<FamilyMemberData, FamilyMember>();

    public const int familyMemberAmount = 4;


    private void Awake()
    {
        #region singleton awake
        if (instance != this && instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
        #endregion

        BuildFamily();
    }

    private void BuildFamily()
    {
        List<FamilyMemberData> familyMembersToCreate = new List<FamilyMemberData>();
        List<FamilyMemberData> familyMembers = new List<FamilyMemberData>();
        familyMembers.AddRange(allFamilyMembers);

        // Get random family members.
        for (int i = 0; i < familyMemberAmount; i++)
        {
            int index = Random.Range(0, familyMembers.Count);
            familyMembersToCreate.Add(familyMembers[index]);
            familyMembers.RemoveAt(index);
        }

        foreach (FamilyMemberData familyMemberData in familyMembersToCreate)
        {
            FamilyMember newFamilyMember = new FamilyMember(familyMemberData);

            FamilyMembers.Add(newFamilyMember.Data, newFamilyMember);
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
        DebugFamilyMembersCreated();
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
