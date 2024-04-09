using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TileGenerator : MonoBehaviour {

    public GameObject tilePrefab;

    Queue<bool[]> tiles;

    public void Start() {
        InitializeTiles();

        if (ValidateTiles()) {
            SpawnTiles();
        }
    }

    public void InitializeTiles() {
        tiles = new Queue<bool[]>();

        for (int i = 0; i < 100; i++) {
            bool[] row_1 = { false, true, false, false };
            bool[] row_2 = { true, true, false, false };
            bool[] row_3 = { false, false, true, false };
            bool[] row_4 = { true, false, true, false };
            bool[] row_5 = { false, false, false, true };
            bool[] row_6 = { true, false, false, true };
            bool[] row_7 = { false, false, false, true };
            bool[] row_8 = { true, false, true, false };
            bool[] row_9 = { false, false, true, false };
            bool[] row_10 = { true, true, false, false };

            tiles.Enqueue(row_1);
            tiles.Enqueue(row_2);
            tiles.Enqueue(row_3);
            tiles.Enqueue(row_4);
            tiles.Enqueue(row_5);
            tiles.Enqueue(row_6);
            tiles.Enqueue(row_7);
            tiles.Enqueue(row_8);
            tiles.Enqueue(row_9);
            tiles.Enqueue(row_10);
        }
    }

    public bool ValidateTiles() {
        // TODO:
        // - check if rows alternate between true and false
        // - check if path to end is possible

        return true;
    }

    public void SpawnTiles() {
        int y = 0;

        while (tiles.Count > 0) {
            bool[] currentRow = tiles.Dequeue();
            float firstTileX = currentRow[0] ? -2.25f : -3.75f;

            for (int tile = 1; tile < currentRow.Length; tile++) {
                if (currentRow[tile]) {
                    var newTile = PrefabUtility.InstantiatePrefab(tilePrefab) as GameObject;
                    newTile.transform.SetParent(this.transform);
                    float x = firstTileX + (3 * (tile - 1));
                    newTile.transform.localPosition = new Vector3(x, y, y);
                }
            }

            y++;
        }
    }
}
