using UnityEngine;
using TMPro;
using System.Collections;

public class TextMeshProUpdater : MonoBehaviour
{
    private TextMeshProUGUI  textMesh;
    public BaseResource holderOfTheMysteriousString;
    public string stingToUpdate => holderOfTheMysteriousString.NumberAsString;

    void OnEnable()
    {
        textMesh = GetComponent<TextMeshProUGUI >();

        if (textMesh == null)
        {
            Debug.LogError("No TextMeshPro component found on this GameObject!");
            return;
        }

        // Start the coroutine when enabled
        StartCoroutine(UpdateTextCoroutine());
    }

    void OnDisable()
    {
        // Stop coroutine when disabled
        StopAllCoroutines();
    }

    IEnumerator UpdateTextCoroutine()
    {
        while (true)
        {
            // Example: set text to current time each second
            textMesh.text = stingToUpdate;

            // Wait one second before updating again
            yield return new WaitForSeconds(1f);
        }
    }
}
