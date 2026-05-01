using UnityEngine;

[System.Serializable]
public class TowerSaveData
{
    public string type;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;


    // Upgrade levels
    public int healthLevel;
    public int regenHealthLevel;
    public int rangeLevel;
    public int towerLevelUpgrade;

    public TowerSaveData(BaseDefensiveTower tower)
    {
        type = "attackingTower";
        position = tower.transform.position;
        rotation = tower.transform.rotation;
        scale = tower.transform.localScale;

        // Safe null checks
        healthLevel = tower.UpgradeableHealth != null ? tower.UpgradeableHealth.level : 0;
        regenHealthLevel = tower.UpgradeableRegenHealth != null ? tower.UpgradeableRegenHealth.level : 0;
        rangeLevel = tower.UpgradeableRange != null ? tower.UpgradeableRange.level : 0;
        towerLevelUpgrade = tower.UpgradeableTowerLevel != null ? tower.UpgradeableTowerLevel.level : 0;
    }
}
