using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button quitButton;
    public GameObject introductionPanel;

    public void Start()
    {
#if UNITY_IOS || UNITY_ANDROID || UNITY_WEBGL
        quitButton.gameObject.SetActive(false);
#endif
    }

    public void StartIntroAnimation()
    {
        if (!Global.SeenIntroSequnece)
        {
            introductionPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
            introductionPanel.GetComponent<Animator>().SetTrigger("trigger");
        }

        StartCoroutine(LoadGame());
    }

    private IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(Global.SeenIntroSequnece ? 0 : 20);

        Global.SeenIntroSequnece = true;
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
