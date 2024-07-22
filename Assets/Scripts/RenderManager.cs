using UnityEditor;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using System;
using UnityEngine.Windows;

public class RenderManager : MonoBehaviour {

    //private static int RENDER_LIMIT = 35;
    //private int lastRenderedTileRow;

    //public GameObject tilePrefab;

    public GameObject bottomRow;
    //public int nextRowToInit;
    public int queueIndex;

    void Awake() {
        //nextRowToInit = 0;
        queueIndex = 0;
    }

    void Start() {
        InitializeRows();
        bottomRow = GetFirstActiveTile(0);

        //lastRenderedTileRow = 0;

        //while (lastRenderedTileRow < TileGenerator.level.Count && lastRenderedTileRow < RENDER_LIMIT) {
        //    RenderTileRow(lastRenderedTileRow, TileGenerator.level[lastRenderedTileRow]);
        //    lastRenderedTileRow++;
        //}
    }

    void InitializeRows() {
        for (int row = 0; row < 30; row++) {
            if (row < TileGenerator.queue.Count) {
                ActivateRow(row, TileGenerator.queue[queueIndex++]);
                //nextRowToInit = (nextRowToInit + 1) % 30;
                //Debug.Log($"nextRowToInit: {nextRowToInit}");
            } else {
                DisableRow(row);
            }
        }
    }

    void ActivateRow(int row, int[] tiles) {
        for (int tileIndex = row % 2; tileIndex < 6; tileIndex += 2) {
            Debug.Log($"Setting Tile_{row}_{tileIndex} to {tiles.Contains(tileIndex)}");
            GameObject tile = transform.Find($"Tile_{row}_{tileIndex}").gameObject;

            if (tiles.Contains(tileIndex)) {
                tile.SetActive(true);
                tile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1 / GameManager.TICK_FREQUENCY);
            } else {
                tile.SetActive(false);
            }
        }
    }

    void DisableRow(int row) {
        for (int tile = row % 2; tile < 6; tile += 2) {
            transform.Find($"Tile_{row}_{tile}").gameObject.SetActive(false);
        }
    }

    void Update() {

        if (bottomRow.transform.position.y <= -3) {
            CycleRows();
        } else {
            Debug.Log($"bottomRow {bottomRow.name}, y: {bottomRow.transform.position.y}, queueIndex: {queueIndex}");
            TileGenerator.queue[queueIndex].ToList().ForEach(i => Debug.Log(i.ToString()));
        }




        //if (transform.childCount > 0) {
        //    GameObject bottomTileRow = transform.GetChild(0).gameObject;

        //    if (bottomTileRow.transform.GetChild(0).transform.position.y < -5) {
        //        // TODO: Destroy all the other tiles in tile row too
        //        Destroy(bottomTileRow.transform.GetChild(0).gameObject);
        //        Destroy(bottomTileRow);

        //        // TODO: Make this async
        //        if (lastRenderedTileRow < TileGenerator.level.Count) {
        //            GameObject topTileRow = transform.GetChild(transform.childCount - 1).gameObject;
        //            RenderTileRow(topTileRow.transform.GetChild(0).position.y + 1, TileGenerator.level[lastRenderedTileRow]);
        //            lastRenderedTileRow++;
        //        }
        //    }
        //}
    }

    private GameObject GetFirstActiveTile(int row) {
        for (int tileIndex = row % 2; tileIndex < 6; tileIndex += 2) {
            GameObject tile = transform.Find($"Tile_{row}_{tileIndex}").gameObject;

            if (tile.activeSelf) {
                return tile;
            }
        }

        Debug.Log($"GetFirstActiveTile({row}) returning null");

        return null;
    }

    private void CycleRows() {
        int bottomRowIndex = Int32.Parse(bottomRow.name.Split('_')[1]);
        int topRowIndex = bottomRowIndex == 0 ? 29 : bottomRowIndex - 1;

        //Debug.Log($"bottomRowIndex = {bottomRowIndex}");
        //Debug.Log($"topRowIndex = {topRowIndex}");
        GameObject topRow = GetFirstActiveTile(topRowIndex);

        if (queueIndex < TileGenerator.queue.Count) {
            ActivateRow(bottomRowIndex, TileGenerator.queue[queueIndex++]);

            for (int tile = bottomRowIndex % 2; tile < 6; tile += 2) {
                GameObject bottomRow = transform.Find($"Tile_{bottomRowIndex}_{tile}").gameObject;
                bottomRow.transform.position = new Vector3(bottomRow.transform.position.x, topRow.transform.position.y + 1, topRow.transform.position.z + 1);
            }

            bottomRow = GetFirstActiveTile((bottomRowIndex + 1) % 30);
        } else {
            Debug.Log("ran out of queue!!!!!!!");
        }
    }

    void SetSpeed(float speed) {
        for (int row = 0; row < 30; row++) {
            for (int tile = row % 2; tile < 6; tile += 2) {
                transform.Find($"Tile_{row}_{tile}").GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
            }
        }
    }

    //private void RenderTileRow(float y, int[] tiles) {
    //    Debug.Log($"Rendering tile at y: {y}");
    //    GameObject tileRow = new GameObject("TileRow" + lastRenderedTileRow);
    //    tileRow.transform.SetParent(transform);

    //    foreach (int tileIndex in tiles) {
    //        GameObject tile = PrefabUtility.InstantiatePrefab(tilePrefab) as GameObject;

    //        tile.transform.localPosition = new Vector3(-3.75f + (tileIndex * 1.5f), y, lastRenderedTileRow);
    //        tile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1 / -GameManager.TICK_FREQUENCY);
    //        tile.transform.SetParent(tileRow.transform);
    //    }
    //}
}
