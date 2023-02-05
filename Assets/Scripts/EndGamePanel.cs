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

    public void ShowPanelLose()
    {
        animator.SetTrigger("trigger");
        firstMessage.text = "YOU LOSE";
        secondMessage.text = "UNLUCKY!";
        StartCoroutine(ReturnToMainMenu());
    }

    public void ShowPanelWin()
    {
        animator.SetTrigger("trigger");
        firstMessage.text = "YOU WIN";
        secondMessage.text = "WOOOHHOO!";
        StartCoroutine(ReturnToMainMenu());
    }

    public void ShowPanelDraw()
    {
        animator.SetTrigger("trigger");
        firstMessage.text = "YOU DRAW";
        secondMessage.text = "MEH...";
        StartCoroutine(ReturnToMainMenu());
    }

    public IEnumerator ReturnToMainMenu()
    {
        yield return new WaitForSeconds(20);
        GameManager.Instance.ReturnToMainMenu();
    }
}
