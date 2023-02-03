using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class CardController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public RectTransform image;

    private Vector2 offset;
    private float imageStartXPosition;
    private bool isAnimating;

    private const float maxRotation = 15;

    private void Start() 
    {
        imageStartXPosition = image.transform.localPosition.x;

    }

    private void Update() 
    {
        if (InputManager.SwipeLeft())
        {

        }
        else if (InputManager.SwipeRight())
        {

        }
    }

    public float X
    {
        get => -image.localPosition.x;
        set
        {
            float minX = (transform as RectTransform).rect.xMin;
            float maxX = (transform as RectTransform).rect.xMax;

            float x = Mathf.Round(value.ToRange(minX, maxX));
            image.localPosition = new Vector3(x, image.localPosition.y, transform.localPosition.z);

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
        if (isAnimating)
        {
            return;
        }

        X = (eventData.position + offset).x;
    }

    /// <summary>
    /// Select the current element when pressed or focus on element in play mode.
    /// </summary>
    /// <param name="eventData">Touch data for the interaction.</param>
    public void OnPointerDown(PointerEventData eventData)
    {
        if (isAnimating)
        {
            return;
        }

        offset = (Vector2)transform.localPosition - eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isAnimating)
        {
            return;
        }

        StartCoroutine(ReturnToInitialPosition());
    }

    private IEnumerator ReturnToInitialPosition()
    {
        isAnimating = true;

        float elapsedTime = 0;
        float waitTime = 0.1f;

        float currentXPosition = X;

        while (elapsedTime < waitTime)
        {
            X = Mathf.Lerp(-currentXPosition, imageStartXPosition, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;
        
            yield return null;
        }  

        X = imageStartXPosition;

        isAnimating = false;
    }
}
