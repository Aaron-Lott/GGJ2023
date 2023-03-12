using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EndGamePanel : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    public TMP_Text firstMessage;
    public TMP_Text secondMessage;

    private bool onPage1 = false;
    private Coroutine returnToHomeCoroutine;

    private void OnEnable()
    {     
        GameManager.OnGameLost += ShowPanelLose;
        GameManager.OnGameWon += ShowPanelWin;
        GameManager.OnGameDraw += ShowPanelDraw;
    }

    private void OnDisable()
    {
        GameManager.OnGameLost -= ShowPanelLose;
        GameManager.OnGameWon -= ShowPanelWin;
        GameManager.OnGameDraw -= ShowPanelDraw;
    }

    public void ShowPanelLose(FamilyMemberData winningFamilyMember, FamilyMemberData losingFamilyMember)
    {
        canvasGroup.blocksRaycasts = true;
        onPage1 = true;
        GetComponent<Animator>()?.SetTrigger("trigger");
        firstMessage.text = "Seems you didn't quite fit in with the Wilotts this time around.\n\n After your trail period, they decided that maybe another family would be a better fit for you.";
        secondMessage.text = $"You especially didn't get along with {losingFamilyMember.FamilyMemberName}.\n\n Try to ensure you don't lower multiple family member's trust too low next time, and perhaps you'll settle in better...";
        returnToHomeCoroutine = StartCoroutine(ReturnToMainMenu());
    }

    public void ShowPanelWin(FamilyMemberData winningFamilyMember, FamilyMemberData losingFamilyMember)
    {
        canvasGroup.blocksRaycasts = true;
        onPage1 = true;
        GetComponent<Animator>()?.SetTrigger("trigger");
        firstMessage.text = $"After living with the Wilotts for some time, you've managed to build a strong relationship with {winningFamilyMember.FamilyMemberName}!\n\n Turns out {winningFamilyMember.secret}";
        secondMessage.text = $"Seems you didn't get along with {losingFamilyMember.FamilyMemberName}, so you may need to change your approach in future.\n\n Congrats on building a strong bond with {winningFamilyMember.FamilyMemberName}!";
        returnToHomeCoroutine = StartCoroutine(ReturnToMainMenu());
    }

    public void ShowPanelDraw(FamilyMemberData winningFamilyMember, FamilyMemberData losingFamilyMember)
    {
        canvasGroup.blocksRaycasts = true;
        onPage1 = true;
        GetComponent<Animator>()?.SetTrigger("trigger");
        firstMessage.text = "After striking a good balance with the family, you continue to make yourself at home with the Wilotts.";
        secondMessage.text = "You didn't manage to uncover any of the family member's secrets this time around, so you may need to change your approach in future.";
        returnToHomeCoroutine = StartCoroutine(ReturnToMainMenu());
    }

    public IEnumerator ReturnToMainMenu()
    {
        yield return new WaitForSeconds(20);
        GameManager.Instance.ReturnToMainMenu();
    }

    public void SetPage2()
    {
        onPage1 = false;
    }

    public void SkipEndGamePanels()
    {
        if (onPage1)
        {
            GetComponent<Animator>()?.SetTrigger("page2trigger");
            onPage1 = false;
            return;
        }

        if (returnToHomeCoroutine != null)
        {
            StopCoroutine(returnToHomeCoroutine);
            returnToHomeCoroutine = null;
        }

        GameManager.Instance.ReturnToMainMenu();
    }
}
