using UnityEngine;

public class GameManager : MonoBehaviour {

    public float tickSpeed;

    public void Start() {
        // TODO:
        // InvokeRepeating does this every tick:
        // 1. Update current TileRow to next in LinkedList
        // 2. Based on selectedPath and NEXT_LEFT/RIGHT,
        //    move Player either left or right
        //      - Movement takes half tick
        //      - If Player is moving to existing Tile,
        //        render Player as green, else red
    }

    public void Update() {
        // TODO:
        // 
    }
}
