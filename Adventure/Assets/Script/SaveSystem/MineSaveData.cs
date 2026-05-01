using System;
using UnityEngine;

[System.Serializable]
public class MineSaveData
{
    public string type;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;

    public int resourceKey; // key for resource type

    public int healthLevel;
    public int regenRateLevel;
    public int maxAmountLevel;
    public int regenHealthLevel;

    public MineSaveData(Mine mine)
    {
        type = "mine";
        position = mine.transform.position;
        rotation = mine.transform.rotation;
        scale = mine.transform.localScale;

        // Use the resource type to get the key
        resourceKey = ResourceRegistry.Instance.GetKey(mine.ResourceType);

        // Safe null checks in case upgradeables aren’t initialized
        healthLevel = mine.Upgradeablehealth != null ? mine.Upgradeablehealth.level : 0;
        regenRateLevel = mine.UpgradeableRegenRate != null ? mine.UpgradeableRegenRate.level : 0;
        maxAmountLevel = mine.UpgradeableMaxAmount != null ? mine.UpgradeableMaxAmount.level : 0;
        regenHealthLevel = mine.UpgradeableRegenhealth != null ? mine.UpgradeableRegenhealth.level : 0; 
    }
}
