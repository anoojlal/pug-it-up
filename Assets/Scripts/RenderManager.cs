using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using System;

public class RenderManager : MonoBehaviour {

    public GameObject bottomRow;
    public int queueIndex;

    void Awake() {
        queueIndex = 0;
    }

    void Start() {
        InitializeRows();
        bottomRow = GetFirstActiveTile(0);
    }

    void Update() {
        if (bottomRow != null && bottomRow.transform.position.y <= -3) {
            CycleRows();
        }
    }

    void InitializeRows() {
        for (int row = 0; row < 30; row++) {
            if (row < TileGenerator.queue.Count) {
                ActivateRow(row, TileGenerator.queue[queueIndex++]);
            } else {
                DisableRow(row);
            }
        }
    }

    void ActivateRow(int row, int[] tiles) {
        for (int tileIndex = row % 2; tileIndex < 6; tileIndex += 2) {
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

    private GameObject GetFirstActiveTile(int row) {
        for (int tileIndex = row % 2; tileIndex < 6; tileIndex += 2) {
            GameObject tile = transform.Find($"Tile_{row}_{tileIndex}").gameObject;

            if (tile.activeSelf) {
                return tile;
            }
        }

        return null;
    }

    private void CycleRows() {
        int bottomRowIndex = Int32.Parse(bottomRow.name.Split('_')[1]);

        if (queueIndex < TileGenerator.queue.Count) {
            int topRowIndex = bottomRowIndex == 0 ? 29 : bottomRowIndex - 1;
            GameObject topRow = GetFirstActiveTile(topRowIndex);
            ActivateRow(bottomRowIndex, TileGenerator.queue[queueIndex++]);

            for (int tile = bottomRowIndex % 2; tile < 6; tile += 2) {
                GameObject bottomRow = transform.Find($"Tile_{bottomRowIndex}_{tile}").gameObject;
                bottomRow.transform.position = new Vector3(bottomRow.transform.position.x, topRow.transform.position.y + 1, topRow.transform.position.z + 1);
            }
        } else {
            Debug.Log("ran out of queue!!!!!!!"); // TODO: advance to next level with buffer in-between levels
            DisableRow(bottomRowIndex);
        }

        bottomRow = GetFirstActiveTile((bottomRowIndex + 1) % 30);
    }
}
