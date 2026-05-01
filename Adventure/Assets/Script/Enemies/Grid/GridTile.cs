using UnityEngine;
using System.Collections.Generic;

[ExecuteAlways] // Show gizmos in editor
public class GridTile : MonoBehaviour
{
    [Header("Tile Settings")]
    public float neighborCheckRadius = 1.1f; // how far to check for neighbours
    public List<GridTile> neighbours = new(); 
    public bool walkable = true;
   // public int cost = 1;
    public int hCost;
    public int gCost;

    public GridTile parent;
    public int fCost => gCost + hCost;

    private void Update()
    {
        // Assign neighbours once at start
        Findneighbours();
    }

    private void Findneighbours()
    {
        neighbours.Clear();

        Collider[] hits = Physics.OverlapSphere(transform.position, neighborCheckRadius);

        foreach (Collider hit in hits)
        {
            GridTile tile = hit.GetComponent<GridTile>();
            if (tile != null && tile != this && !neighbours.Contains(tile))
            {
                neighbours.Add(tile);
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Draw tile
        Gizmos.color = walkable ? Color.green : Color.red;
        Gizmos.DrawSphere(transform.position, 0.1f);

        // Draw neighbor connections
        Gizmos.color = Color.yellow;
        foreach (GridTile neighbor in neighbours)
        {
            if (neighbor != null)
            {
                Gizmos.DrawLine(transform.position, neighbor.transform.position);
            }
        }
    }
}