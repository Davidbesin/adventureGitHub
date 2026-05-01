using UnityEngine;
using System.Collections;
using System;

public class PlayerToMine : MonoBehaviour
{
    public int resourceGatheringTime = 1; // seconds per tick
    [SerializeField] private PlayersInventory playersInventory;
   
    //ThE UI AND Mine. Could do this shit in another script..
    Mine currentMineGlobalField; 
    public int CollectedUI => collectedAmount;
    public float RegenRate => currentMineGlobalField.RegenRate;
    public int Health => currentMineGlobalField.Health;
    public int MineMaxAmount => currentMineGlobalField.MineMaxAmount;
    public float HealthRegen => currentMineGlobalField.RegenRateHealth;
    
    

    private Coroutine collectingCoroutine;


    private void OnTriggerEnter(Collider other)
    {
        Mine currentMine = other.GetComponent<Mine>();
        if (currentMine != null)
        {
            Debug.Log("MineTriggerWorks");
            currentMineGlobalField = currentMine;
            if (collectingCoroutine != null)
                StopCoroutine(collectingCoroutine);

            collectingCoroutine = StartCoroutine(CollectFromMine(currentMine));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Mine currentMine = other.GetComponent<Mine>();
        if (currentMine != null && collectingCoroutine != null)
        {
            currentMineGlobalField = null;
            StopCoroutine(collectingCoroutine);
            collectingCoroutine = null;
        }
    }

    private int GetCollectAmount(Type resourceType)
    {
        if (resourceType == typeof(Copper)) return 10 * CopperStat.staticlevel;
        if (resourceType == typeof(Diamond)) return 10 * DiamondStat.staticlevel;
        if (resourceType == typeof(Gold)) return 10 * GoldStat.staticlevel;
        if (resourceType == typeof(Iron)) return 10 * IronStat.staticlevel;
        if (resourceType == typeof(ManaGem)) return 10 * GemStat.staticlevel;
        if (resourceType == typeof(RubiesResource)) return 10 * RubyStat.staticlevel;
        if (resourceType == typeof(SilverResource)) return 10 * SilverStat.staticlevel;
        if (resourceType == typeof(StoneResource)) return 10 * StoneStat.staticlevel;
        if (resourceType == typeof(Wood)) return 10 * WoodStat.staticlevel;

        Debug.LogWarning($"No specific collect stat set for {resourceType.Name}. Defaulting to 1.");
        return 1;
    }
    int collectedAmount;
    private IEnumerator CollectFromMine(Mine mine)
    {
        while (true)
        {
            yield return new WaitForSeconds(resourceGatheringTime);
            Debug.Log("Collect");

            collectedAmount = mine.Collect(GetCollectAmount(mine.ResourceType.GetType()));

            if (collectedAmount > 0)
            {
                Debug.Log($"Collected {collectedAmount} from {mine.ResourceType.GetType().Name}. Mine now has {mine.MineAmount} left.");
                playersInventory.AddResources(mine.ResourceType.GetType(), collectedAmount);
            }
        }
    }
}
