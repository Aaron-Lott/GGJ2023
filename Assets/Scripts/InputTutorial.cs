using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTutorial : MonoBehaviour
{
    [SerializeField] private Transform finger;
    [SerializeField] private StoryCardInputController inputController;

    public bool IsPlaying { get; private set; }

    private float fingerStartXPosition;

    private const float swipeDistance = 500f;
    private const float waitDuration = 1.5f;

    private void Start()
    {
        StartCoroutine(TutorialRoutine());
    }


    public IEnumerator TutorialRoutine()
    {
        fingerStartXPosition = finger.transform.localPosition.x;
        finger.gameObject.SetActive(true);

        IsPlaying = true;

        yield return new WaitForSeconds(waitDuration);

        StoryCardFlingable storyCardFlingable = FindObjectOfType<StoryCardFlingable>();

        // Simulate swipe left.
        float elapsedTime = 0;
        float waitTime = 0.1f;

        float currentCardXPosition = storyCardFlingable.X;
        float endCardXPosition = storyCardFlingable.startXPosition - swipeDistance;

        float curentFingerXPosition = finger.transform.localPosition.x;
        float endFingerXPosition = fingerStartXPosition - swipeDistance;

        while (elapsedTime < waitTime)
        {
            storyCardFlingable.X = Mathf.Lerp(currentCardXPosition, endCardXPosition, (elapsedTime / waitTime));
            finger.transform.localPosition = new Vector2(Mathf.Lerp(curentFingerXPosition, endFingerXPosition, (elapsedTime / waitTime)), finger.transform.localPosition.y);
            inputController.UpdateCardProperties();
            elapsedTime += Time.deltaTime;
        
            yield return null;
        }  

        storyCardFlingable.X = endCardXPosition;
        finger.transform.localPosition = new Vector3(endFingerXPosition, finger.transform.localPosition.y);
        
        yield return new WaitForSeconds(waitDuration);
        elapsedTime = 0;

        // Simulate swipe to center.
        currentCardXPosition = storyCardFlingable.X;
        endCardXPosition = storyCardFlingable.startXPosition;

        curentFingerXPosition = finger.transform.localPosition.x;
        endFingerXPosition = fingerStartXPosition;

        while (elapsedTime < waitTime)
        {
            storyCardFlingable.X = Mathf.Lerp(currentCardXPosition, endCardXPosition, (elapsedTime / waitTime));
            finger.transform.localPosition = new Vector2(Mathf.Lerp(curentFingerXPosition, endFingerXPosition, (elapsedTime / waitTime)), finger.transform.localPosition.y);
            inputController.UpdateCardProperties();
            elapsedTime += Time.deltaTime;
        
            yield return null;
        }  

        storyCardFlingable.X = endCardXPosition;
        finger.transform.localPosition = new Vector3(endFingerXPosition, finger.transform.localPosition.y);

        yield return new WaitForSeconds(waitDuration);
        elapsedTime = 0;

        // Simulate swipe right.
        currentCardXPosition = storyCardFlingable.X;
        endCardXPosition = storyCardFlingable.startXPosition + swipeDistance;

        curentFingerXPosition = finger.transform.localPosition.x;
        endFingerXPosition = fingerStartXPosition + swipeDistance;

        while (elapsedTime < waitTime)
        {
            storyCardFlingable.X = Mathf.Lerp(-currentCardXPosition, endCardXPosition, (elapsedTime / waitTime));
            finger.transform.localPosition = new Vector2(Mathf.Lerp(curentFingerXPosition, endFingerXPosition, (elapsedTime / waitTime)), finger.transform.localPosition.y);
            inputController.UpdateCardProperties();
            elapsedTime += Time.deltaTime;
        
            yield return null;
        }  

        storyCardFlingable.X = endCardXPosition;
        finger.transform.localPosition = new Vector3(endFingerXPosition, finger.transform.localPosition.y);

        yield return new WaitForSeconds(waitDuration);
        elapsedTime = 0;

        // Simulate swipe to center.
        currentCardXPosition = storyCardFlingable.X;
        endCardXPosition = storyCardFlingable.startXPosition;

        curentFingerXPosition = finger.transform.localPosition.x;
        endFingerXPosition = fingerStartXPosition;

        while (elapsedTime < waitTime)
        {
            storyCardFlingable.X = Mathf.Lerp(currentCardXPosition, endCardXPosition, (elapsedTime / waitTime));
            finger.transform.localPosition = new Vector2(Mathf.Lerp(curentFingerXPosition, endFingerXPosition, (elapsedTime / waitTime)), finger.transform.localPosition.y);
            inputController.UpdateCardProperties();
            elapsedTime += Time.deltaTime;
        
            yield return null;
        }  

        storyCardFlingable.X = endCardXPosition;
        finger.transform.localPosition = new Vector3(endFingerXPosition, finger.transform.localPosition.y);

        yield return new WaitForSeconds(waitDuration);
        elapsedTime = 0;

        finger.gameObject.SetActive(false);
        IsPlaying = false;
    }
}
