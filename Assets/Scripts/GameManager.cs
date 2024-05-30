using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static float TICK_FREQUENCY = 0.5f;
    public static float[] X_POSITIONS = new float[] { -3.75f, -2.25f, -0.75f, 0.75f, 2.25f, 3.75f };

    public static TileGenerator tileGenerator;
    public static InputManager inputManager;
    public static GameObject player;

    private LinkedListNode<GameObject> currentTileRow;
    public int currentPosition;

    public void Awake() {
        tileGenerator = GameObject.Find("TileGenerator").GetComponent<TileGenerator>();
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        player = GameObject.Find("Player");
    }

    public void Start() {
        //currentTileRow = tileGenerator.level.First;
        //currentPosition = currentTileRow.Value.GetComponent<TileRow>().tiles[0].GetComponent<Tile>().position;
        //bool onTrack = false;

        //foreach (GameObject tile in currentTileRow.Value.GetComponent<TileRow>().tiles) {
        //    if (tile.GetComponent<Tile>().position == currentPosition) {
        //        onTrack = true;

        //        break;
        //    }
        //}

        //Debug.Log("currentPosition: " + currentPosition + " onTrack: " + onTrack);
        InvokeRepeating("Tick", 0f, TICK_FREQUENCY);
    }

    private void Tick() {
        bool goLeft = inputManager.path < player.transform.position.x;
        //int shifted = Convert.ToInt32(currentTileRow.Value.GetComponent<TileRow>().shifted);

        //currentTileRow = currentTileRow.Next;
        //Debug.Log("goLeft: " + goLeft + " shifted: " + shifted + " currentPosition: " + currentPosition);
        //currentPosition = goLeft ? TileGenerator.NEXT_LEFT[shifted, currentPosition] : TileGenerator.NEXT_RIGHT[shifted, currentPosition];
        //bool onTrack = false;

        //foreach (GameObject tile in currentTileRow.Value.GetComponent<TileRow>().tiles) {
        //    if (tile.GetComponent<Tile>().position == currentPosition) {
        //        onTrack = true;

        //        break;
        //    }
        //}

        //Debug.Log("currentPosition: " + currentPosition + " onTrack: " + onTrack);

        StartCoroutine(MovePlayer(goLeft));
    }

    IEnumerator MovePlayer(bool goLeft) {
        // TODO: correct player x value in case of glitch

        float timeElapsed = 0;
        float duration = TICK_FREQUENCY / 2;
        float startX = player.transform.position.x;
        float startY = 0;
        float endX = goLeft ? startX - 1.5f : startX + 1.5f;
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
