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

    private struct CanCardSubmitInfo
    {
        public bool canSubmit;
        public bool isYes;
    }

    private Vector2 offset;
    private bool inputDisabled;
    private CanCardSubmitInfo canSubmitCard;

    private float imageStartXPosition;
    private float imageNormalXPosition;

    private const float maxRotation = 20f;
    private const float submitForce = 1600f;

    private void Start() 
    {
        storyCard.cardImageRigidbody2D.isKinematic = true;
    }

    public float imageX
    {
        get => -storyCard.cardImageTransform.localPosition.x;
        set
        {
            float minX = (transform as RectTransform).rect.xMin;
            float maxX = (transform as RectTransform).rect.xMax;

            float x = Mathf.Round(value.ToRange(minX, maxX));
            storyCard.cardImageTransform.localPosition = new Vector3(x, storyCard.cardImageTransform.localPosition.y, transform.localPosition.z);

            imageNormalXPosition = (x - imageStartXPosition) / maxX;

            canSubmitCard.canSubmit = x <= minX || x >= maxX;
            canSubmitCard.isYes = imageNormalXPosition > 0 ? true : false;

            // Handle card rotation.
            float z = ((transform.position.x - storyCard.cardImageTransform.transform.position.x) / maxRotation).ToRange(-maxRotation, maxRotation);
            storyCard.cardImageTransform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, z);
        }
    }

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

        imageX = (eventData.position + offset).x;

        // Update image background colour and text based of swipe status.
        storyCard.backgroundImage.color = imageNormalXPosition > 0 ? Color.green : Color.red;
        storyCard.yesText.gameObject.SetActive(imageNormalXPosition > 0);
        storyCard.noText.gameObject.SetActive(imageNormalXPosition < 0);
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

        if (canSubmitCard.canSubmit)
        {
            // Apply physics.
            storyCard.cardImageRigidbody2D.isKinematic = false;

            storyCard.cardImageRigidbody2D.AddForce(new Vector2(imageNormalXPosition * submitForce, imageNormalXPosition * submitForce), ForceMode2D.Impulse);
            storyCard.cardImageRigidbody2D.AddTorque(-imageNormalXPosition * submitForce / 5);
            inputDisabled = true;

            if (canSubmitCard.isYes)
                storyCard.OnYesChosen?.Invoke();
            else
                storyCard.OnNoChosen?.Invoke();
        }
        else
        {
            StartCoroutine(ReturnToInitialPosition());
        }
    }

    private IEnumerator ReturnToInitialPosition()
    {
        inputDisabled = true;

        float elapsedTime = 0;
        float waitTime = 0.1f;

        float currentXPosition = imageX;

        while (elapsedTime < waitTime)
        {
            imageX = Mathf.Lerp(-currentXPosition, imageStartXPosition, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;
        
            yield return null;
        }  

        imageX = imageStartXPosition;

        inputDisabled = false;
    }
}
