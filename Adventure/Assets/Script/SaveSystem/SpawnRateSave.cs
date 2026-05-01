using UnityEngine;
using System.IO;

public class SpawnRateSave : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
     private void Start()
    {
        if (SaveManager.Instance != null)
            SaveManager.Instance.AllMineSave += SaveSpawnRate;
        else
            Debug.LogError("SaveManager not ready when MineSave enabled!");
    }
    
    private void OnEnable()
    {
        if (SaveManager.Instance != null)
            SaveManager.Instance.AllMineSave += SaveSpawnRate;
        else
            Debug.LogError("SaveManager not ready when MineSave enabled!");
    }

    private void OnDisable()
    {
        if (SaveManager.Instance != null)
            SaveManager.Instance.AllMineSave -= SaveSpawnRate;
    }

    // Update is called once per frame
    void SaveSpawnRate()
    {
        SpawnRateData spawnData = new();
        string json = JsonUtility.ToJson(spawnData, true);
        string path = Path.Combine(Application.persistentDataPath, $"SpawnData.json");
        File.WriteAllText(path, json);
        Debug.Log($"Mine saved: {path}");
    }
}
