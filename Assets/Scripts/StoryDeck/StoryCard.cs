using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class StoryCard : MonoBehaviour
{
    [Header("Statics")]
    [SerializeField] private RectTransform storyCardFlingablePrefab;
    [SerializeField] private RectTransform storyCardFlingableSpawnPoint;

    [Header("Fields")]
    public TMP_Text descriptionText;
    public Image backgroundImage;
    public TMP_Text yesText;
    public TMP_Text noText;

    [HideInInspector] public UnityEvent OnYesChosen;
    [HideInInspector] public UnityEvent OnNoChosen;

    public StoryCardData Data { get; private set; }

    public Transform currentFlingable;
    public Transform previousFlingable;
    public Rigidbody2D currentFlingableImageRigidbody2D;
    public Image currentFlingableImage;

    private void OnEnable()
    {
        OnYesChosen.AddListener(OnYes);
        OnNoChosen.AddListener(OnNo);
    }

    private void OnDisable()
    {
        OnYesChosen.RemoveListener(OnYes);
        OnNoChosen.RemoveListener(OnNo);
    }

    public void SetupCard(StoryCardData storyCardData)
    {
        Data = storyCardData;
        backgroundImage.color = Data.backgroundColour;
        descriptionText.text = Data.description;
        yesText.text = Data.yesText;
        noText.text = Data.noText;

        StartCoroutine(CreateFlingable());
    }

    private IEnumerator CreateFlingable()
    {
        previousFlingable = currentFlingable;

        currentFlingable = Instantiate(storyCardFlingablePrefab, storyCardFlingableSpawnPoint);
        Debug.Log(currentFlingable);

        currentFlingableImage = currentFlingable.GetComponent<Image>();
        currentFlingableImage.sprite = Data.sprite;
        currentFlingableImageRigidbody2D = currentFlingable.GetComponent<Rigidbody2D>();

        yield return new WaitForSeconds(1);

        if (previousFlingable)
        Destroy(previousFlingable.gameObject);
    }

    private void OnYes()
    {
        StoryDeckManager.Instance.AddUnlockablePacksToDeck(Data.onYesPacksToUnlock);
        StoryDeckManager.Instance.GenerateNewCard();
    }

    private void OnNo()
    {
        StoryDeckManager.Instance.AddUnlockablePacksToDeck(Data.onNoPacksToUnlock);
        StoryDeckManager.Instance.GenerateNewCard();
    }
}
