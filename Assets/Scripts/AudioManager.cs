using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource music;
    public AudioSource swipe;
    public AudioSource button;

    #region singleton
    public static AudioManager Instance { get => instance; }

    private static AudioManager instance;
    #endregion

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
            DontDestroyOnLoad(gameObject);
        }
        #endregion
    }

    public void ButtonClick()
    {
        if (button.isPlaying) return;

        button.Play();
    }
}
