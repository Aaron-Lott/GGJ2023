using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResetSecretsButton : MonoBehaviour
{
    public event Action ButtonPress;

    public void ResetSecrets()
    {
        foreach (var familyMember in FamilyManager.Instance.AllFamilyMembers)
        {
            if (!familyMember.Value.Data.hasSecret)
            {
                continue;
            }

            familyMember.Value.IsSecretUnlocked = false;
        }

        ButtonPress?.Invoke();
    }
}
