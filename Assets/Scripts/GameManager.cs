using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static float TICK_FREQUENCY = 0.5f;

    public static TileGenerator tileGenerator;
    public static InputManager inputManager;
    public static GameObject player;

    private LinkedListNode<GameObject> currentTileRow;

    public void Awake() {
        tileGenerator = GameObject.Find("TileGenerator").GetComponent<TileGenerator>();
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        player = GameObject.Find("Player");
    }

    public void Start() {
        // TODO:
        // InvokeRepeating does this every tick:
        // 1. Update current TileRow to next in LinkedList
        // 2. Based on selectedPath and NEXT_LEFT/RIGHT,
        //    move Player either left or right
        //      - Movement takes half tick
        //      - If Player is moving to existing Tile,
        //        render Player as green, else red

        currentTileRow = tileGenerator.level.First;
        InvokeRepeating("Tick", 0f, TICK_FREQUENCY);
    }

    public void Update() {
        // TODO:
        // 
    }

    private void Tick() {
        currentTileRow = currentTileRow.Next;
        StartCoroutine(MovePlayer(inputManager.path < player.transform.position.x));
    }

    IEnumerator MovePlayer(bool left) {
        float timeElapsed = 0;
        float duration = TICK_FREQUENCY / 2;
        float startX = player.transform.position.x;
        float startY = 0;
        float endX = left ? startX - 1.5f : startX + 1.5f;
        float endY = 0.5f;

        player.transform.position = new Vector3(startX, startY, player.transform.position.z);
        Debug.Log("start: " + player.transform.position);
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        while (timeElapsed < duration) {
            float t = timeElapsed / duration;
            player.transform.position = new Vector3(Mathf.Lerp(startX, endX, t), Mathf.Lerp(startY, endY, t), player.transform.position.z);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        player.transform.position = new Vector3(endX, endY, player.transform.position.z);
        Debug.Log("end: " + player.transform.position);
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1 / -TICK_FREQUENCY);
    }
}
