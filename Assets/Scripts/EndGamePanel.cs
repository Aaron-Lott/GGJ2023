using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EndGamePanel : MonoBehaviour
{
    public TMP_Text firstMessage;
    public TMP_Text secondMessage;

    public Animator animator;

    private void Start()
    {     
        GameManager.OnGameLost += ShowPanelLose;
        GameManager.OnGameWon += ShowPanelWin;
        GameManager.OnGameDraw += ShowPanelDraw;
    }

    public void ShowPanelLose(FamilyMemberData winningFamilyMember, FamilyMemberData losingFamilyMember)
    {
        animator.SetTrigger("trigger");
        firstMessage.text = "Seems you didn't quite fit in with the Wilotts this time around. After a trail period, they decided that maybe another family would be a better fit for you.";
        secondMessage.text = $"You especially didn't get along with {losingFamilyMember}. Try to ensure you don't lower multiple family member's trust too low next time, and perhaps you'll settle in better...";
        StartCoroutine(ReturnToMainMenu());
    }

    public void ShowPanelWin(FamilyMemberData winningFamilyMember, FamilyMemberData losingFamilyMember)
    {
        animator.SetTrigger("trigger");
        firstMessage.text = $"After living with the Wilotts for some time, you've managed to build a strong relationship with <winningFamilyMemberName>! Turns out {winningFamilyMember.secret}";
        secondMessage.text = $"Seems you didn't get along with {losingFamilyMember.FamilyMemberName}, so you may need to change your approach in future. Congrats on building a string bond with {winningFamilyMember.FamilyMemberName}!";
        StartCoroutine(ReturnToMainMenu());
    }

    public void ShowPanelDraw(FamilyMemberData winningFamilyMember, FamilyMemberData losingFamilyMember)
    {
        animator.SetTrigger("trigger");
        firstMessage.text = "After striking a good balance with the family, you continue to make yourself at home with the Wilotts.";
        secondMessage.text = "You didn't manage to uncover any of the family member's secrets this time around, so you may need to change your approach in future.";
        StartCoroutine(ReturnToMainMenu());
    }

    public IEnumerator ReturnToMainMenu()
    {
        yield return new WaitForSeconds(20);
        GameManager.Instance.ReturnToMainMenu();
    }
}
