using UnityEngine;
using System.IO;

[RequireComponent(typeof(Mine))]
public class MineSave : MonoBehaviour
{
    private Mine mine;

    private void Awake()
    {
        mine = GetComponent<Mine>();
    }

    private void Start()
    {
        if (SaveManager.Instance != null)
            SaveManager.Instance.AllMineSave += SaveMine;
        else
            Debug.LogError("SaveManager not ready when MineSave enabled!");
    }
    
    private void OnEnable()
    {
        if (SaveManager.Instance != null)
            SaveManager.Instance.AllMineSave += SaveMine;
        else
            Debug.LogError("SaveManager not ready when MineSave enabled!");
    }

    private void OnDisable()
    {
        if (SaveManager.Instance != null)
            SaveManager.Instance.AllMineSave -= SaveMine;
    }

    public void SaveMine()
    {
        if (mine == null) return;

        MineSaveData data = new MineSaveData(mine);
        string json = JsonUtility.ToJson(data, true);

        string path = Path.Combine(Application.persistentDataPath, $"Mine_{transform.position}.json");
        File.WriteAllText(path, json);

        Debug.Log($"Mine saved: {path}");
    }
}
