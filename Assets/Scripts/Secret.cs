using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class Secret : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public Image image;
    public TMP_Text secretText;
    public bool unlocked;

    public void SetUnlocked(bool unlocked, FamilyMemberData data)
    {
        canvasGroup.alpha = unlocked ? 1 : 0.5f;
        secretText.text =  unlocked ? data.secret : "SECRET LOCKED";
    }
}
