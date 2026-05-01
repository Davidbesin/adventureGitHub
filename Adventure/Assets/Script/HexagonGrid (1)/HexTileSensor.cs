using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class HexTileSensor : MonoBehaviour
{
    [Header("Detection Settings")]
    public float sensorRadius = 1.1f; 
    public LayerMask tileLayer;      

    [Header("Sensed Neighbors")]
    public List<GameObject> neighbors = new List<GameObject>();

    [Header("Manual Edit")]
    public GameObject tileToRemove; // The field to hold the GO you want to delete

    [ContextMenu("Sense Neighbors Now")]
    public void SenseNeighbors()
    {
        neighbors.Clear();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, sensorRadius, tileLayer);

        foreach (var hit in hitColliders)
        {
            if (hit.gameObject != this.gameObject)
            {
                neighbors.Add(hit.gameObject);
            }
        }
        Debug.Log($"{name} found {neighbors.Count} neighbors.");
    }

    [ContextMenu("Remove Specific Neighbor")]
    public void RemoveNeighbor()
    {
        if (tileToRemove != null && neighbors.Contains(tileToRemove))
        {
            neighbors.Remove(tileToRemove);
            Debug.Log($"{tileToRemove.name} removed from {name}'s neighbor list.");
            tileToRemove = null; // Clear the slot after removing
        }
        else
        {
            Debug.LogWarning("Tile not found in list or field is empty!");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, sensorRadius);
    }
}
