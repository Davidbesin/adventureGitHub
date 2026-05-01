using UnityEngine;
using System.IO;

[RequireComponent(typeof(BaseDefensiveTower))]
public class TowerSave : MonoBehaviour
{
    private BaseDefensiveTower tower;

    private void Awake()
    {
        tower = GetComponent<BaseDefensiveTower>();
    }

    public void SaveTower()
    {
        if (tower == null) {Debug.Log("pR"); return;}

        TowerSaveData data = new TowerSaveData(tower);
        string json = JsonUtility.ToJson(data, true);

        string path = Path.Combine(Application.persistentDataPath, $"Tower_{transform.position}.json");
        File.WriteAllText(path, json);

        Debug.Log($"Tower saved: {path}");
    }

    private void Start()
    {
        if (SaveManager.Instance != null)
            SaveManager.Instance.AllTowerSave += SaveTower; // subscribe to tower event

            else
            Debug.LogError("SaveManager not ready when tower enabled!");
    }
    private void OnEnable()
    {
        if (SaveManager.Instance != null)
            SaveManager.Instance.AllTowerSave += SaveTower; // subscribe to tower event
            else
            Debug.LogError("SaveManager not ready when tower enabled!");
    }

    private void OnDisable()
    {
        if (SaveManager.Instance != null)
            SaveManager.Instance.AllTowerSave -= SaveTower;
    }
}
