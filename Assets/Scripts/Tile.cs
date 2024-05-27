using UnityEngine;

public class Tile : MonoBehaviour {

    public int position;

    Rigidbody2D rb;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start() {
        rb.velocity = new Vector2(0, 1 / -GameManager.TICK_FREQUENCY);
    }
}
