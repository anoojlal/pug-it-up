using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UIElements;

public class TileGenerator : MonoBehaviour {

    public GameObject tilePrefab;

    // TODO: Keep track of List of bool arrays instead of GameObjects since RenderManager will deal with GameObjects
    public List<GameObject> level;

    public void Awake() {
        // TODO:
        // - Keep track of entire level's tiles in List
        // - Work with RenderManager to render finite number of tiles

        level = new List<GameObject>();
        GenerateRandomSingleWidth(1000);
    }

    public void GenerateRandomSingleWidth(int length) {
        bool[] firstTiles = { false, false, false, false, false, false };
        int previousTile = 2;
        firstTiles[previousTile] = true;
        level.Add(InstantiateTileRow(firstTiles));

        for (int i = 1; i < length; i++) {
            bool[] tiles = { false, false, false, false, false, false };
            System.Random random = new System.Random();
            bool goLeft = random.Next(2) == 1;
            previousTile = previousTile == 5 || (goLeft && previousTile != 0) ? previousTile - 1 : previousTile + 1;
            tiles[previousTile] = true;
            level.Add(InstantiateTileRow(tiles));
        }
    }

    public GameObject InstantiateTileRow(bool[] tiles) {
        // TODO: Let RenderManager do the GameObject stuff

        int index = level.Count;

        GameObject tileRow = new GameObject("TileRow" + index);
        tileRow.transform.SetParent(this.transform);

        for (int tileIndex = 0; tileIndex <= 5; tileIndex++) {
            if (tiles[tileIndex]) {
                GameObject tile = PrefabUtility.InstantiatePrefab(tilePrefab) as GameObject;

                tile.transform.localPosition = new Vector3(-3.75f + (tileIndex * 1.5f), index, index);
                tile.transform.SetParent(tileRow.transform);
            }
        }

        return tileRow;
    }
}
