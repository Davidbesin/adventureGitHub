using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HexSaveData
{
    public string prefabKey;
    public string type;
    public List<int> ownedTileIDs;
    public List<int> unownedTileIDs;

    public HexSaveData(HexManager manager, string prefabKey)
    {
        type = "Planet";
        this.prefabKey = prefabKey;
        ownedTileIDs = new List<int>();
        unownedTileIDs = new List<int>();

        foreach (var tile in manager.OwnedTile)
            ownedTileIDs.Add(tile.ID);

        foreach (var tile in manager.UnownedTile)
            unownedTileIDs.Add(tile.ID);
    }
}
