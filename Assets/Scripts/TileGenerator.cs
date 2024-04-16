using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class TileGenerator : MonoBehaviour {

    private int[,] NEXT_LEFT = new int[2, 3] {
        { -1, 0, 1 },
        { 0, 1, 2 }
    };

    private int[,] NEXT_RIGHT = new int[2, 3] {
        { 0, 1, 2 },
        { 1, 2, -1 }
    };

    public GameObject tilePrefab;
    public LinkedList<GameObject> level;

    public void Awake() {
        InitiateLevel(500);
    }

    public void InitiateLevel(int length) {
        // TODO:
        // - linked list infinitely full
        // - renders finite number of rows

        level = new LinkedList<GameObject>();
        AddTileRow(0, true, new bool[] { true, false, false });

        for (int y = 1; y < length; y++) {
            TileRow lastTileRow = level.Last.Value.GetComponent<TileRow>();
            int lastTilePosition = lastTileRow.tiles[0].GetComponent<Tile>().position;
            System.Random random = new System.Random();
            bool goLeft = random.Next(2) == 1;
            bool[] positions = { false, false, false };

            if (NEXT_RIGHT[Convert.ToInt32(lastTileRow.shifted), lastTilePosition] == -1 || (NEXT_LEFT[Convert.ToInt32(lastTileRow.shifted), lastTilePosition] != -1 && goLeft)) {
                positions[NEXT_LEFT[Convert.ToInt32(lastTileRow.shifted), lastTilePosition]] = true;
            } else {
                positions[NEXT_RIGHT[Convert.ToInt32(lastTileRow.shifted), lastTilePosition]] = true;
            }

            AddTileRow(y, y % 2 == 0, positions);
        }
    }

    private void AddTileRow(int y, bool shifted, bool[] positions) {
        GameObject tileRow = new GameObject("TileRow" + y);
        float firstTileX = shifted ? -2.25f : -3.75f;

        level.AddLast(tileRow);
        tileRow.AddComponent<TileRow>();
        tileRow.GetComponent<TileRow>().shifted = shifted;
        tileRow.transform.SetParent(this.transform);

        for (int position = 0; position < positions.Length; position++) {
            if (positions[position]) {
                var tile = PrefabUtility.InstantiatePrefab(tilePrefab) as GameObject;
                float x = firstTileX + (position * 3);

                tile.transform.localPosition = new Vector3(x, y, y);
                tile.transform.SetParent(tileRow.transform);
                tile.GetComponent<Tile>().position = position;
                tileRow.GetComponent<TileRow>().tiles.Add(tile);
            }
        }
    }

    public bool ValidateLevel() {
        // TODO:
        // - check if rows alternate between true and false
        // - check if path to end is possible

        return true;
    }
}
