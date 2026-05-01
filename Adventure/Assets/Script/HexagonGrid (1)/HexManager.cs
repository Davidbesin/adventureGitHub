using UnityEngine;
using System.Collections.Generic;

public class HexManager : MonoBehaviour
{
    public List<HexTile> OwnedTile = new List<HexTile>();
    public List<HexTile> UnownedTile = new List<HexTile>();

    // Remove from Awake if you don’t want auto‑assign
    // private void Awake() => AssignIDsToChildren();

    [ContextMenu("Assign IDs To Children")]
    private void AssignIDsToChildren()
    {
        HexTile[] tiles = GetComponentsInChildren<HexTile>();
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i].AssignID(i);

            if (tiles[i].Owned)
                MoveToOwned(tiles[i]);
            else
                MoveToUnowned(tiles[i]);
        }

        Debug.Log($"Assigned IDs to {tiles.Length} hex tiles.");
    }

    [ContextMenu("Clear Tile Lists")]
    private void ClearTileLists()
    {
        OwnedTile.Clear();
        UnownedTile.Clear();
        Debug.Log("Cleared owned/unowned tile lists.");
    }

    public void MoveToOwned(HexTile tile)
    {
        if (!OwnedTile.Contains(tile)) OwnedTile.Add(tile);
        UnownedTile.Remove(tile);
        ToggleTileComponents(tile, true);
    }

    public void MoveToUnowned(HexTile tile)
    {
        if (!UnownedTile.Contains(tile)) UnownedTile.Add(tile);
        OwnedTile.Remove(tile);
        ToggleTileComponents(tile, false);
    }

    private void ToggleTileComponents(HexTile tile, bool state)
    {
        if (tile.TryGetComponent(out MeshRenderer renderer))
            renderer.enabled = state;

        if (tile.TryGetComponent(out Collider collider))
            collider.enabled = state;
    }

    public void ApplyOwnershipFromIDs(List<int> ownedIDs, List<int> unownedIDs)
    {
        // Clear current lists first
        ClearTileLists();

        HexTile[] tiles = GetComponentsInChildren<HexTile>();
        foreach (var tile in tiles)
        {
            if (ownedIDs.Contains(tile.ID))
            {
                MoveToOwned(tile);
                tile.SetBool(true);
            }
            else if (unownedIDs.Contains(tile.ID))
            {
                MoveToUnowned(tile);
                tile.SetBool(false);
            }
        }

        Debug.Log($"Applied ownership from IDs. Owned={OwnedTile.Count}, Unowned={UnownedTile.Count}");
    }

}
