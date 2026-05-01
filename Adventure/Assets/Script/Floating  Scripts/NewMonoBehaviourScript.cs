using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SaveMeshGroupRuntime : MonoBehaviour
{
    public bool SaveOnPlay = true;

    void Start()
    {
        if (SaveOnPlay)
        {
            SaveMeshes();
        }
    }

    [ContextMenu("Save Mesh Group")]
    public void SaveMeshes()
    {
#if UNITY_EDITOR

        string folderPath = "Assets/GeneratedMeshes";

        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            AssetDatabase.CreateFolder("Assets", "GeneratedMeshes");
        }

        GameObject parent = new GameObject("GeneratedMeshGroup");

        MeshFilter[] filters = GetComponentsInChildren<MeshFilter>();

        for (int i = 0; i < filters.Length; i++)
        {
            if (filters[i].sharedMesh == null)
                continue;

            Mesh newMesh = Instantiate(filters[i].sharedMesh);

            string meshPath = folderPath + "/Mesh_" + i + ".asset";
            AssetDatabase.CreateAsset(newMesh, meshPath);

            GameObject child = new GameObject("Mesh_" + i);
            child.transform.parent = parent.transform;

            MeshFilter mf = child.AddComponent<MeshFilter>();
            mf.sharedMesh = newMesh;

            MeshRenderer mr = child.AddComponent<MeshRenderer>();

            if (filters[i].GetComponent<MeshRenderer>())
            {
                mr.sharedMaterial =
                filters[i].GetComponent<MeshRenderer>().sharedMaterial;
            }

            child.transform.position = filters[i].transform.position;
            child.transform.rotation = filters[i].transform.rotation;
            child.transform.localScale = filters[i].transform.localScale;
        }

        string prefabPath = folderPath + "/GeneratedMeshGroup.prefab";

        PrefabUtility.SaveAsPrefabAsset(parent, prefabPath);

        DestroyImmediate(parent);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Saved Mesh Group Prefab!");

#endif
    }
}