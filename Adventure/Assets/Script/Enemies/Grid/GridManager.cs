using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static List<GridTile> AllTiles = new List<GridTile>();

    private void Awake()
    {
        GridTile[] tiles = FindObjectsOfType<GridTile>();

        AllTiles.Clear();

        for (int i = 0; i < tiles.Length; i++)
        {
            AllTiles.Add(tiles[i]);
        }
    }
}