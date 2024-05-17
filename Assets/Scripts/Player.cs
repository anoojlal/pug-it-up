using UnityEngine;

public class Player : MonoBehaviour {

    void Awake() {
        this.transform.position = new Vector3(-0.75f, 0, 0);
    }

    void Start() {
        
    }

    void Update() {
        // TODO:
        // - Sprite faces left or right depending on selectedPath
    }

    public void Move(bool left) {
        float translation = left ? -1.5f : 1.5f;
        this.transform.position = new Vector3(this.transform.position.x + translation, this.transform.position.y, this.transform.position.z);
    }
}
