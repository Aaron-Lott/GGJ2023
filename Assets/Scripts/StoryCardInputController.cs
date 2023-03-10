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

    private bool InputDisabled => FindObjectOfType<InputTutorial>()? .IsPlaying ?? false;

    /// <summary>
    /// Move the card image while dragging.
    /// </summary>
    /// <param name="eventData">Touch data for the interaction.</param>
    public void OnDrag(PointerEventData eventData)
    {
        if (InputDisabled)
        {
            return;
        }

        storyCard.currentFlingable.X = (eventData.position + offset).x;

        UpdateCardProperties();
    }

    /// <summary>
    /// Select the current element when pressed or focus on element in play mode.
    /// </summary>
    /// <param name="eventData">Touch data for the interaction.</param>
    public void OnPointerDown(PointerEventData eventData)
    {
        if (InputDisabled)
        {
            return;
        }

        offset = (Vector2)transform.localPosition - eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (InputDisabled)
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

            AudioManager.Instance?.swipe?.Play();
        }
        else
        {
            StartCoroutine(storyCard.currentFlingable.ReturnToInitialPosition());
        }
    }

    public void UpdateCardProperties()
    {
        // Update image background colour and text based of swipe status.
        if (storyCard.Data.hasCustomYesNoColours)
            storyCard.backgroundImage.color = storyCard.currentFlingable.submitInfo.isYes ? storyCard.Data.yesColour : storyCard.Data.noColour;
        else
            storyCard.backgroundImage.color = storyCard.currentFlingable.submitInfo.isYes ? Color.cyan : Color.cyan;

        storyCard.yesText.gameObject.SetActive(storyCard.currentFlingable.submitInfo.isYes);
        storyCard.noText.gameObject.SetActive(!storyCard.currentFlingable.submitInfo.isYes);
    }
}
