using UnityEngine;
using System;
using System.IO;

public class LoadManager : MonoBehaviour
{
    public static LoadManager Instance;

    public event Action AllMineLoad;
    public event Action AllTowerLoad;
    public event Action AllHexLoad;

    private void Awake()
    {
        Instance = this;
    }

    public void LoadAllMines()
    {
        AllMineLoad?.Invoke();
        LoadAll();
        Debug.Log("All mines loaded.");
    }

    public void LoadAllTowers()
    {
        AllTowerLoad?.Invoke();
        Debug.Log("All towers loaded.");
    }

    public void LoadAllHexTiles()
    {
        AllHexLoad?.Invoke();
        Debug.Log("All hex tiles loaded.");
    }

    public void LoadAll()
    {
        string[] files = Directory.GetFiles(Application.persistentDataPath, "*.json");
        foreach (string path in files)
        {
            string json = File.ReadAllText(path);

            if (json.Contains("\"type\": \"mine\""))
            {
                MineSaveData mineData = JsonUtility.FromJson<MineSaveData>(json);
                SpawnMine(mineData);
            }
            else if (json.Contains("\"type\": \"tower\"") || json.Contains("\"type\": \"attackingTower\""))
            {
                TowerSaveData towerData = JsonUtility.FromJson<TowerSaveData>(json);
                SpawnTower(towerData);
            }
            else if (json.Contains("\"type\": \"Planet\""))
            {
                HexSaveData hexData = JsonUtility.FromJson<HexSaveData>(json);
                SpawnHexGrid(hexData);
            }
        }

        Debug.Log("All mines, towers, and hex tiles loaded.");
    }

    private void SpawnMine(MineSaveData data)
    {
        if (!PrefabRegistry.Instance.prefabsDict.TryGetValue("SampleMine", out GameObject prefab))
        {
            Debug.LogError("SpawnMine: Prefab key 'SampleMine' not found!");
            return;
        }

        GameObject instance = Instantiate(prefab, data.position, data.rotation);
        instance.transform.localScale = data.scale;

        Mine mine = instance.GetComponent<Mine>();
        if (mine == null)
        {
            Debug.LogError("SpawnMine: Mine component missing on prefab!");
            return;
        }

        mine.AssignResource(ResourceRegistry.Instance.GetResource(data.resourceKey));

        // Ensure upgrade interfaces exist
        if (mine.Upgradeablehealth == null ||
            mine.UpgradeableRegenRate == null ||
            mine.UpgradeableMaxAmount == null ||
            mine.UpgradeableRegenhealth == null)
        {
            Debug.LogWarning("SpawnMine: Upgradeable stats are null, creating interfaces...");
            mine.SendMessage("AddUpgradeInterFace");
        }

        // Restore upgrade levels
        SafeAssignLevel(mine.Upgradeablehealth, data.healthLevel, "Mine Health");
        SafeAssignLevel(mine.UpgradeableRegenRate, data.regenRateLevel, "Mine RegenRate");
        SafeAssignLevel(mine.UpgradeableMaxAmount, data.maxAmountLevel, "Mine MaxAmount");
        SafeAssignLevel(mine.UpgradeableRegenhealth, data.regenHealthLevel, "Mine RegenHealth");

        // Apply stats
        mine.ApplyStatsToHealth();
        mine.ApplyStatsToRegenRate();
        mine.ApplyStatsToMaxAmount();
        mine.ApplyStatsToRegenHealth();

        Debug.Log($"SpawnMine: Mine loaded at {data.position} with resourceKey={data.resourceKey}");
    }

    private void SpawnTower(TowerSaveData data)
    {
        if (!PrefabRegistry.Instance.prefabsDict.TryGetValue("AttackTowerBundle", out GameObject prefab))
        {
            Debug.LogError("SpawnTower: Prefab key 'AttackTowerBundle' not found!");
            return;
        }

        GameObject instance = Instantiate(prefab, data.position, data.rotation);
        instance.transform.localScale = data.scale;

        BaseDefensiveTower tower = instance.GetComponent<BaseDefensiveTower>();
        if (tower == null)
        {
            Debug.LogError("SpawnTower: BaseDefensiveTower component missing on prefab!");
            return;
        }

        // Ensure upgrade interfaces exist
        if (tower.UpgradeableHealth == null ||
            tower.UpgradeableRegenHealth == null ||
            tower.UpgradeableRange == null ||
            tower.UpgradeableTowerLevel == null)
        {
            Debug.LogWarning("SpawnTower: Upgradeable stats are null, creating interfaces...");
            tower.SendMessage("AddUpgradeInterfaces");
        }

        // Restore upgrade levels
        SafeAssignLevel(tower.UpgradeableHealth, data.healthLevel, "Tower Health");
        SafeAssignLevel(tower.UpgradeableRegenHealth, data.regenHealthLevel, "Tower Regen Health");
        SafeAssignLevel(tower.UpgradeableRange, data.rangeLevel, "Tower Range");
        SafeAssignLevel(tower.UpgradeableTowerLevel, data.towerLevelUpgrade, "Tower Level");

        // Apply stats
        tower.ApplyStatsToMaxHealth();
        tower.ApplyStatsToRegenHealth();
        tower.ApplyStatsToRange();
        tower.ApplyStatsToTowerLevel();

        Debug.Log($"SpawnTower: Tower loaded at {data.position} with upgrade levels applied.");
    }


    private void SpawnHexGrid(HexSaveData data)
    {
        if (!PrefabRegistry.Instance.prefabsDict.TryGetValue("LoveDPlanet", out GameObject prefab))
        {
            Debug.LogError("SpawnHexGrid: Prefab key 'LoveDPlanet' not found!");
            return;
        }

        GameObject instance = Instantiate(prefab);

        HexManager manager = instance.GetComponent<HexManager>();
        if (manager == null)
        {
            Debug.LogError("SpawnHexGrid: HexManager component missing on prefab!");
            return;
        }

        manager.ApplyOwnershipFromIDs(data.ownedTileIDs, data.unownedTileIDs);
        Debug.Log($"SpawnHexGrid: Hex grid loaded with {data.ownedTileIDs.Count} owned and {data.unownedTileIDs.Count} unowned tiles.");
    }

    // Helper for safe upgrade assignment
    private void SafeAssignLevel(UpgradeableStatInterface stat, int level, string name)
    {
        if (stat != null)
        {
            stat.AssignLevel(level);
        }
        else
        {
            Debug.LogWarning($"SafeAssignLevel: {name} stat interface is null!");
        }
    }
}
