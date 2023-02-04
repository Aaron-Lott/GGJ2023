using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using TMPro;

public class CardController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public RectTransform image;
    public Image imageBackground;
    public Rigidbody2D imageRigidbody2D;
    public TMP_Text yesText;
    public TMP_Text noText;

    private Vector2 offset;
    private bool inputDisabled;
    private bool submitCard;
    private float imageStartXPosition;

    private float imageNormalXPosition;

    private const float maxRotation = 20f;
    private const float submitForce = 1600f;

    private void Start() 
    {
        imageRigidbody2D.isKinematic = true;
    }

    public float imageX
    {
        get => -image.localPosition.x;
        set
        {
            float minX = (transform as RectTransform).rect.xMin;
            float maxX = (transform as RectTransform).rect.xMax;

            float x = Mathf.Round(value.ToRange(minX, maxX));
            image.localPosition = new Vector3(x, image.localPosition.y, transform.localPosition.z);

            submitCard = x <= minX || x >= maxX;
            imageNormalXPosition = (x - imageStartXPosition) / maxX;

            // Handle card rotation.
            float z = ((transform.position.x - image.transform.position.x) / maxRotation).ToRange(-maxRotation, maxRotation);
            image.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, z);
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
        imageBackground.color = imageNormalXPosition > 0 ? Color.green : Color.red;
        yesText.gameObject.SetActive(imageNormalXPosition > 0);
        noText.gameObject.SetActive(imageNormalXPosition < 0);
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

        if (submitCard)
        {
            // Apply physics.
            imageRigidbody2D.isKinematic = false;

            imageRigidbody2D.AddForce(new Vector2(imageNormalXPosition * submitForce, imageNormalXPosition * submitForce), ForceMode2D.Impulse);
            imageRigidbody2D.AddTorque(-imageNormalXPosition * submitForce / 5);
            inputDisabled = true;
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
