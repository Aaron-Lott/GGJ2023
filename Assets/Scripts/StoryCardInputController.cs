using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using TMPro;

public class StoryCardInputController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private StoryCard storyCard;

    private Vector2 offset;
    private bool inputDisabled;

    /// <summary>
    /// Move the card image while dragging.
    /// </summary>
    /// <param name="eventData">Touch data for the interaction.</param>
    public void OnDrag(PointerEventData eventData)
    {
        if (inputDisabled)
        {
            return;
        }

        storyCard.currentFlingable.X = (eventData.position + offset).x;

        // Update image background colour and text based of swipe status.
        storyCard.backgroundImage.color = storyCard.currentFlingable.submitInfo.isYes ? Color.green : Color.red;
        storyCard.yesText.gameObject.SetActive(storyCard.currentFlingable.submitInfo.isYes);
        storyCard.noText.gameObject.SetActive(!storyCard.currentFlingable.submitInfo.isYes);
    }

    /// <summary>
    /// Select the current element when pressed or focus on element in play mode.
    /// </summary>
    /// <param name="eventData">Touch data for the interaction.</param>
    public void OnPointerDown(PointerEventData eventData)
    {
        if (inputDisabled)
        {
            return;
        }

        offset = (Vector2)transform.localPosition - eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (inputDisabled)
        {
            return;
        }

        if (storyCard.currentFlingable.submitInfo.canSubmit)
        {
            // Apply physics to flingable.
            storyCard.currentFlingable.ApplyPhysics();

            if (storyCard.currentFlingable.submitInfo.isYes)
                storyCard.OnYesChosen?.Invoke();
            else
                storyCard.OnNoChosen?.Invoke();
        }
        else
        {
            StartCoroutine(storyCard.currentFlingable.ReturnToInitialPosition());
        }
    }
}
