using UnityEngine;

public class Player : MonoBehaviour {

    private SpriteRenderer spriteRenderer;

    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.transform.position = new Vector3(-0.75f, 0, -1);
    }

    void Update() {
        if (this.transform.position.x < GameManager.inputManager.path) {
            spriteRenderer.flipX = true;
        } else {
            spriteRenderer.flipX = false;
        }
    }
}
