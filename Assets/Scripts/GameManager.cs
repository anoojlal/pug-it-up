using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static float TICK_SPEED = 0.5f;

    private TileGenerator tileGenerator;
    private InputManager inputManager;
    private GameObject player;
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
        Debug.Log(currentTileRow.Value.name);

        InvokeRepeating("Tick", 0f, TICK_SPEED);
    }

    public void Update() {
        // TODO:
        // 
    }

    private void Tick() {
        currentTileRow = currentTileRow.Next;

        float translation = inputManager.path < player.transform.position.x ? -1.5f : 1.5f;
        player.transform.position = new Vector3(player.transform.position.x + translation, player.transform.position.y, player.transform.position.z);
        Debug.Log("player: " + player.transform.position + " currentTileRow: " + currentTileRow.Value.transform.position);
    }
}
