using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecretsMenu : MonoBehaviour
{
    public LayoutGroup grid;
    public Secret secretPrefab;

    private void Start() 
    {
        PopulateSecrets();
    }

    private void PopulateSecrets()
    {
        foreach (Transform child in grid.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var familyMember in FamilyManager.Instance.FamilyMembers)
        {
            if (!familyMember.Value.Data.hasSecret)
            {
                continue;
            }

            Secret secret = Instantiate(secretPrefab, grid.transform);
            secret.image.sprite = familyMember.Value.Data.secretSprite;
            secret.secretText.text = familyMember.Value.Data.secret;
            secret.SetUnlocked(familyMember.Value.IsSecretUnlocked, familyMember.Value.Data);
        }
    }

    public void ClosePopup()
    {
        StopAllCoroutines();
        StartCoroutine(ClosePopupRoutine());
    }

    private IEnumerator ClosePopupRoutine()
    {
        float elapsedTime = 0;
        float waitTime = 0.2f;

        transform.localScale = Vector3.one;
        Vector3 currentScale = transform.localScale;

        while (elapsedTime < waitTime)
        {
            transform.localScale = Vector3.Lerp(currentScale, Vector3.zero, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;
        
            yield return null;
        }  

        transform.localScale = Vector3.zero;
        gameObject.SetActive(false);
    }

    public void OpenPopup()
    {
        StopAllCoroutines();
        PopulateSecrets();

        gameObject.SetActive(true);
        StartCoroutine(OpenPopupRoutine());
    }

    private IEnumerator OpenPopupRoutine()
    {
        float elapsedTime = 0;
        float waitTime = 0.2f;

        transform.localScale = Vector3.zero;
        Vector3 currentScale = transform.localScale;

        while (elapsedTime < waitTime)
        {
            transform.localScale = Vector3.Lerp(currentScale, Vector3.one, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;
        
            yield return null;
        }  

        transform.localScale = Vector3.one;
    }
}
