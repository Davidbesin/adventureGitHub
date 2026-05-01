using UnityEngine;

public interface ITileSense
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    HexTile GetNearTile(HexTileSensor sensor);

}
