using UnityEngine;
using System.Collections;

public class PlayerBagToVault : MonoBehaviour
{
    [Header("Player Inventories")]
    [SerializeField] private PlayersInventory playerBag;
    [SerializeField] private PlayersInventory playerVault;

    [Header("Drain Settings")] 
    [SerializeField] private float drainIntervalSeconds = 0.5f;
    [SerializeField] private int drainAmountPerStep = 5;

    private Coroutine drainCoroutine;

    public IEnumerator DrainResourcesSlowly()
    {
        foreach (var resource in playerBag.PlayersResources)
        {
            while (resource.Amount > 0)
            {
                int amountToDrain = Mathf.Min(drainAmountPerStep, resource.Amount);

                resource.SubtractAmount(amountToDrain);
                playerVault.AddResources(resource.GetType(), amountToDrain);

                Debug.Log($"Drained {amountToDrain} of {resource.GetType().Name} to {playerVault.name}");

                yield return new WaitForSeconds(drainIntervalSeconds);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider is a Player
        if (other.GetComponent<Vault>() != null)
        {
            Debug.Log("Player entered vault zone, starting drain...");
            drainCoroutine = StartCoroutine(DrainResourcesSlowly());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Vault>() != null)
        {
            Debug.Log("Player exited vault zone, stopping drain...");
            if (drainCoroutine != null)
            {
                StopCoroutine(drainCoroutine);
                drainCoroutine = null;
            }
        }
    }
}
