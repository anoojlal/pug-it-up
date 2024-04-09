using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public float speed = 2.5f;
    Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        
    }

    void FixedUpdate() {
        rb.velocity = new Vector2(0, -speed);
    }
}
