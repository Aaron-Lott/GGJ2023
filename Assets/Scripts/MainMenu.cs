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
        introductionPanel.gameObject.SetActive(true);

        StartCoroutine(LoadGame());
    }

    private IEnumerator LoadGame()
    {
        while (AnimatorIsPlaying())
        {
            yield return null;
        }

        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

      bool AnimatorIsPlaying()
      {
        return introductionPanel.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length >
                introductionPanel.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
}
