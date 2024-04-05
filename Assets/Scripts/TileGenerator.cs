using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TileGenerator : MonoBehaviour {

    public GameObject tile;
    public Vector2 size;

    public void Start() {
        GenerateBoard();
    }

    public void GenerateBoard() {
        for (int col = 0; col < size.x; col++) {
            for (int row = 0; row < size.y; row++) {
                var newTile = PrefabUtility.InstantiatePrefab(tile) as GameObject;
                newTile.transform.SetParent(this.transform);
            }
        }
    }
}
