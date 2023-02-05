using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoryCardFlingable : MonoBehaviour
{
    public struct SubmitInfo
    {
        public bool canSubmit;
        public bool isYes;
    }

    public SubmitInfo submitInfo;

    public Image familyMemberImage;
    public TMP_Text familyMemberName;
    public new Rigidbody2D rigidbody2D;

    [HideInInspector] public float startXPosition;
    private float normalisedXPosition;
    private Transform container;

    public float maxRotation = 20f;
    public float submitForce = 1600f;

    private void Start() 
    {
        startXPosition = transform.localPosition.x;
        container = StoryDeckManager.Instance.storyCard.transform;
    }

    public float X
    {
        get => transform.localPosition.x;
        set
        {
            float minX = (container as RectTransform).rect.xMin;
            float maxX = (container as RectTransform).rect.xMax;

            float x = Mathf.Round(value.ToRange(minX, maxX));
            transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);

            normalisedXPosition = (x - startXPosition) / maxX;

            submitInfo.canSubmit = x <= minX || x >= maxX;
            submitInfo.isYes = normalisedXPosition > 0 ? true : false;

            // Handle card rotation.
            float z = ((container.position.x - transform.position.x) / maxRotation).ToRange(-maxRotation, maxRotation);
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, z);
        }
    }

    public IEnumerator ReturnToInitialPosition()
    {
        float elapsedTime = 0;
        float waitTime = 0.1f;

        float currentXPosition = X;

        while (elapsedTime < waitTime)
        {
            X = Mathf.Lerp(currentXPosition, startXPosition, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;
        
            yield return null;
        }  

        X = startXPosition;
    }

    public void ApplyPhysics()
    {
        rigidbody2D.isKinematic = false;
        rigidbody2D.AddForce(new Vector2(normalisedXPosition * submitForce, Mathf.Abs(normalisedXPosition) * submitForce), ForceMode2D.Impulse);
        rigidbody2D.AddTorque(-normalisedXPosition * submitForce / 5);
    }
}
