using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasScaler))]
[ExecuteInEditMode]
public class CleverCanvasScaler : MonoBehaviour
{
    [SerializeField] private bool runInEditor = true;
    [SerializeField] private Vector2 targetResolution = new Vector2(1080, 1920);
    [SerializeField] private Vector2 targetAspectRatio = new Vector2(9, 16);

    private bool screenIsPortrait = true;

    private void Start()
    {
        if (Screen.width > Screen.height)
        {
            screenIsPortrait = false;
            GetComponent<CanvasScaler>().matchWidthOrHeight = 1;
        }
        else
        {
            screenIsPortrait = true;
            GetComponent<CanvasScaler>().matchWidthOrHeight = 0;
        }
    }

    void Update()
    {
        if (runInEditor || Application.isPlaying)
        {
            CanvasScaler canvasScaler = GetComponent<CanvasScaler>();
            if (canvasScaler.referenceResolution != targetResolution)
            {
                if (targetResolution.x > 0 && targetResolution.y > 0)
                {
                    canvasScaler.referenceResolution = targetResolution;
                }
                else
                {
                    Debug.Log("Please set target resolution values above 0");
                    return;
                }
            }
            if (targetAspectRatio.x <= 0 || targetAspectRatio.y <= 0)
            {
                Debug.Log("Please set target aspect ratio values above 0");
                return;
            }

            UpdateCanvasScaler();
        }
    }

    public void UpdateCanvasScaler()
    {
        float currentIdealWidth = (Screen.height / targetAspectRatio.y) * targetAspectRatio.x;
        if (screenIsPortrait && Screen.width > currentIdealWidth)
        {
            screenIsPortrait = false;
            GetComponent<CanvasScaler>().matchWidthOrHeight = 1;
        }
        else if (Screen.width <= currentIdealWidth)
        {
            screenIsPortrait = true;
            GetComponent<CanvasScaler>().matchWidthOrHeight = 0;
        }
    }
}