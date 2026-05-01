using UnityEngine;
using System.IO;

[RequireComponent(typeof(HexManager))]
public class HexSave : MonoBehaviour
{
    private HexManager manager;

    private void Awake()
    {
        manager = GetComponent<HexManager>();
    }

    public void SaveHex()
    {
        // You’ll need to decide which prefabKey to use here.
        // For example, if the prefab has a PrefabID component:
        string prefabKey = GetComponent<PrefabID>()?.PlanetPrefabID ?? "Unknown";

        HexSaveData data = new HexSaveData(manager, prefabKey);
        string json = JsonUtility.ToJson(data, true);

        string path = Path.Combine(Application.persistentDataPath, $"Hex_{prefabKey}.json");
        File.WriteAllText(path, json);

        Debug.Log($"Hex grid saved: {path}");
    }

    private void Start()
    {
        if (SaveManager.Instance != null)
            SaveManager.Instance.AllHexSave += SaveHex; // subscribe to hex event
            else
            Debug.Log("SaveManager not ready when hex enabled!");
    }
    private void OnEnable()
    {
        if (SaveManager.Instance != null)
            SaveManager.Instance.AllHexSave += SaveHex; // subscribe to hex event

            else
            Debug.Log("SaveManager not ready when hex enabled!");
    }

    private void OnDisable()
    {
        if (SaveManager.Instance != null)
            SaveManager.Instance.AllHexSave -= SaveHex;
    }
}
