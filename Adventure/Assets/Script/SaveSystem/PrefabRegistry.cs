using UnityEngine;
using System;
using System.Collections.Generic;
public class PrefabRegistry : MonoBehaviour
{
    
     public static PrefabRegistry Instance; 
     [SerializeField] private List<GameObject> prefabs = new();
    public Dictionary<string, GameObject> prefabsDict = new();

    private void Awake()
    {
        Instance ??= this;

        // Build dictionary: string → GameObject
        for (int i = 0; i < prefabs.Count; i++)
        {
            prefabsDict[prefabs[i].GetComponent<PrefabID>().PlanetPrefabID] = prefabs[i];
        }
    }

    public string GetKey(GameObject prefab)
    {
        var id = prefab.GetComponent<PrefabID>();
        return id != null ? id.PlanetPrefabID : null;
    }

}
