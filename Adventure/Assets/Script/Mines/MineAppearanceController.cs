using UnityEngine;

public class MineAppearanceController : MonoBehaviour
{
    private HexTile tile;
    private Mine mineScript;
    private MeshRenderer meshRenderer;
    private Collider mineCollider;

    private void Awake()
    {
        // Cache components
        mineScript = GetComponent<Mine>();
        meshRenderer = GetComponent<MeshRenderer>();
        mineCollider = GetComponent<Collider>();

        // Find the tile via sensor
        var sensor = GetComponent<HexTileSensor>();
        if (sensor != null && sensor.neighbors.Count > 0)
        {
            tile = sensor.neighbors[0].GetComponent<HexTile>();
        }
        else
        {
            Debug.LogWarning($"Mine {name} has no tile under its feet!");
        }
    }

    private void OnEnable()
    {
        if (tile != null)
        {
            tile.OnChangeOwned += SyncAppearance;
            SyncAppearance(); // immediate sync
        }
    }

    private void OnDisable()
    {
        if (tile != null)
            tile.OnChangeOwned -= SyncAppearance;
    }

    private void SyncAppearance()
    {
        bool owned = tile.Owned;
        Debug.Log($"Mine {name} appearance update: Tile owned={owned}");

        if (mineScript != null) mineScript.enabled = owned;
        if (meshRenderer != null) meshRenderer.enabled = owned;
        if (mineCollider != null) mineCollider.enabled = owned;
    }
}
