using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour {

    // TODO:
    // - Button to keep generating more level

    public static List<int[]> level;

    public void Awake() {
        level = new List<int[]>();
        GenerateRandomSingleWidth(2, 10000);
    }

    public void GenerateRandomSingleWidth(int startingTile, int length) {
        int[] firstTiles = { startingTile };
        int currentTile = startingTile;
        level.Add(firstTiles);

        for (int i = 1; i < length; i++) {
            System.Random random = new System.Random();
            bool goLeft = random.Next(2) == 1;
            currentTile = currentTile == 5 || (goLeft && currentTile != 0) ? currentTile - 1 : currentTile + 1;
            int[] tiles = { currentTile };
            level.Add(tiles);
        }
    }
}
