using UnityEditor;
using UnityEngine;

public class RenderManager : MonoBehaviour {

    private static int RENDER_LIMIT = 35;
    private int lastRenderedTileRow;

    public GameObject tilePrefab;

    void Start() {
        lastRenderedTileRow = 0;

        while (lastRenderedTileRow < TileGenerator.level.Count && lastRenderedTileRow < RENDER_LIMIT) {
            RenderTileRow(lastRenderedTileRow, TileGenerator.level[lastRenderedTileRow]);
            lastRenderedTileRow++;
        }
    }

    void Update() {
        if (transform.childCount > 0) {
            GameObject bottomTileRow = transform.GetChild(0).gameObject;

            if (bottomTileRow.transform.GetChild(0).transform.position.y < -5) {
                // TODO: Destroy all the other tiles in tile row too
                Destroy(bottomTileRow.transform.GetChild(0).gameObject);
                Destroy(bottomTileRow);

                // TODO: Make this async
                if (lastRenderedTileRow < TileGenerator.level.Count) {
                    GameObject topTileRow = transform.GetChild(transform.childCount - 1).gameObject;
                    RenderTileRow(topTileRow.transform.GetChild(0).position.y + 1, TileGenerator.level[lastRenderedTileRow]);
                    lastRenderedTileRow++;
                }
            }
        }
    }

    private void RenderTileRow(float y, int[] tiles) {
        Debug.Log($"Rendering tile at y: {y}");
        GameObject tileRow = new GameObject("TileRow" + lastRenderedTileRow);
        tileRow.transform.SetParent(transform);

        foreach (int tileIndex in tiles) {
            GameObject tile = PrefabUtility.InstantiatePrefab(tilePrefab) as GameObject;

            tile.transform.localPosition = new Vector3(-3.75f + (tileIndex * 1.5f), y, lastRenderedTileRow);
            tile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1 / -GameManager.TICK_FREQUENCY);
            tile.transform.SetParent(tileRow.transform);
        }
    }
}
