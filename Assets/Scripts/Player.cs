using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour {

    // TODO:
    // - Green when on a tile, red when off the tiles (game over)

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        transform.position = new Vector3(-0.75f, 0, -1);
    }

    void Update() {
        if (transform.position.x < GameManager.inputManager.path) {
            spriteRenderer.flipX = true;
        } else {
            spriteRenderer.flipX = false;
        }
    }

    public void MovePlayer(bool goLeft) {
        StartCoroutine(MovePlayerCoroutine(goLeft));
    }

    IEnumerator MovePlayerCoroutine(bool goLeft) {
        // TODO: correct player x value in case of glitch

        float timeElapsed = 0;
        float duration = GameManager.TICK_FREQUENCY / 2;
        float startX = transform.position.x;
        float startY = 0;
        float endX = goLeft ? startX - 1.5f : startX + 1.5f;
        float endY = 0.5f;

        transform.position = new Vector3(startX, startY, transform.position.z);
        //Debug.Log("start: " + player.transform.position);
        rb.velocity = new Vector2(0, 0);

        while (timeElapsed < duration) {
            float t = timeElapsed / duration;
            transform.position = new Vector3(Mathf.Lerp(startX, endX, t), Mathf.Lerp(startY, endY, t), transform.position.z);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = new Vector3(endX, endY, transform.position.z);
        //Debug.Log("end: " + player.transform.position);
        rb.velocity = new Vector2(0, 1 / -GameManager.TICK_FREQUENCY);
    }
}
