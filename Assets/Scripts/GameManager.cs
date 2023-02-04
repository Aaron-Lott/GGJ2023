using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region singleton
    public static GameManager Instance { get => instance; }

    private static GameManager instance;
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
        }
        DontDestroyOnLoad(gameObject);
        #endregion
    }

    private void Start() 
    {
        StoryDeckManager.Instance.GenerateNewCard();
    }
}
