using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TileGenerator : MonoBehaviour {

    public GameObject tilePrefab;

    public bool[,] tiles = {
        {true, true, true, true},
        {false, true, true, true},
        {true, true, true, true},
        {false, true, true, true},
        {true, false, true, true},
        {false, false, true, true},
        {true, true, true, false},
        {false, true, true, false}
    };

    public void Start() {
        if (ValidateTiles()) {
            SpawnTiles();
        }
    }

    public bool ValidateTiles() {
        // TODO:
        // - check if rows alternate between true and false
        // - check if path to end is possible

        return true;
    }

    public void SpawnTiles() {
        for (int y = 0; y < tiles.GetLength(0); y++) {
            float firstTileX = tiles[y, 0] ? -2.25f : -3.75f;

            for (int tile = 1; tile < tiles.GetLength(1); tile++) {
                if (tiles[y, tile]) {
                    var newTile = PrefabUtility.InstantiatePrefab(tilePrefab) as GameObject;
                    newTile.transform.SetParent(this.transform);
                    float x = firstTileX + (3 * (tile - 1));
                    newTile.transform.localPosition = new Vector3(x, y, y);
                }
            }
        }


        
    }
}
