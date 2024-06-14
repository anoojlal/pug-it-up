using UnityEngine;

public class GameManager : MonoBehaviour {

    // TODO:
    // - Refactor everything to be static

    public static float TICK_FREQUENCY = 0.5f;

    public static TileGenerator tileGenerator;
    public static InputManager inputManager;
    public static RenderManager renderManager;

    public static Player player;
    public int currentPosition;

    public void Awake() {
        tileGenerator = GameObject.Find("TileGenerator").GetComponent<TileGenerator>();
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        renderManager = GameObject.Find("InputManager").GetComponent<RenderManager>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void Start() {
        InvokeRepeating("Tick", 0f, TICK_FREQUENCY);
    }

    private void Tick() {
        bool goLeft = inputManager.path < player.transform.position.x;

        player.MovePlayer(goLeft);
    }
}
