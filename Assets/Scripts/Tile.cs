using UnityEngine;

public class Tile : MonoBehaviour {

    private Rigidbody2D rb;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start() {
        // TODO: Let GameManager handle this

        rb.velocity = new Vector2(0, 1 / -GameManager.TICK_FREQUENCY);
    }
}
