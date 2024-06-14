using UnityEditor;
using UnityEngine;

public class RenderManager : MonoBehaviour {

    // TODO:
    // - Render next tile row when one is deleted
    // - Error checking when level runs out

    private static int RENDER_LIMIT = 10;
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
        GameObject bottomTileRow = this.transform.GetChild(0).gameObject;

        if (bottomTileRow.transform.GetChild(0).transform.position.y < -5) {
            Destroy(bottomTileRow.transform.GetChild(0).gameObject); // TODO: Destroy all the other tiles in tile row too
            Destroy(bottomTileRow);
            Debug.Log("DESTROYED!!!! ..?");
        }
    }

    private void RenderTileRow(int index, int[] tiles) {
        GameObject tileRow = new GameObject("TileRow" + index);
        tileRow.transform.SetParent(this.transform);

        foreach (int tileIndex in tiles) {
            GameObject tile = PrefabUtility.InstantiatePrefab(tilePrefab) as GameObject;

            tile.transform.localPosition = new Vector3(-3.75f + (tileIndex * 1.5f), index, index);
            tile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1 / -GameManager.TICK_FREQUENCY);
            tile.transform.SetParent(tileRow.transform);
        }
    }
}
