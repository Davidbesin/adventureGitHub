using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTest : MonoBehaviour {
    [SerializeField] private int width, height;
    private Dictionary<Vector3Int, GameObject> _tiles = new Dictionary<Vector3Int, GameObject>();

    [SerializeField] private GameObject tilePrefab;

    private Grid _grid;

    private void Awake() {
        _grid = GetComponent<Grid>();
    }

    private void Start() {
        StartCoroutine(GenerateGrid());
    }

    private IEnumerator GenerateGrid() {
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                Vector3Int cellPos = new Vector3Int(x, y, 0);

                Vector3 worldPos = _grid.CellToWorld(cellPos);

                GameObject tile = Instantiate(tilePrefab, worldPos, Quaternion.identity, transform);
                tile.name = $"Tile ({x},{y})";

                _tiles[cellPos] = tile;
            }
            yield return null;
        }
    }
}