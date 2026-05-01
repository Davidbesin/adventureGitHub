using UnityEngine;

public class PrefabID : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]string planetPrefabID;
    public string PlanetPrefabID => planetPrefabID;
}
