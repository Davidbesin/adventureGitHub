using UnityEngine;
using System.Collections;

public class InventoryRegulator : MonoBehaviour
{
    [SerializeField] private PlayersInventory targetInventoy;
    [SerializeField] private int baseMaxAllowedSum = 100;   // threshold
    [SerializeField] private int maxAllowedSum;
    [SerializeField]int sum;
    [SerializeField] UpgradeableStatInterface upgradeableRegulatedAmount;
    private void Start()
    {
        // Start the coroutine when the game begins
        StartCoroutine(RegulateInventory());
    }

    private IEnumerator RegulateInventory()
    {
        // Loop forever
        while (true)
        {
            sum = 0;
            foreach (var resource in targetInventoy.PlayersResources)
            {
                sum += resource.Amount;
            }

            // Gate logic: flip boolean when sum exceeds threshold
            targetInventoy.canAddResources = sum < maxAllowedSum;

            // Wait ~0.33 seconds (3 times per second)
            yield return new WaitForSeconds(0.33f);
        }
    }

    public void ApplyStatsToMaxAmount()
    {
        maxAllowedSum = baseMaxAllowedSum * upgradeableRegulatedAmount.level;
    }     
}
