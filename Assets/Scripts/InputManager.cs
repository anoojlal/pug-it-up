using UnityEngine;
using System;

public class InputManager : MonoBehaviour {

    // TODO:
    // - Accept mouse input for testing

    private static float[] PATHS = new float[] { -3f, -1.5f, 0, 1.5f, 3f };
    private LineRenderer lineRenderer;

    public static float path;

    void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = true;
        lineRenderer.positionCount = 2;
        UpdatePath(PATHS[0]);
    }

    void Update() {
        if (Input.touchCount > 0) {
            UpdatePath(Camera.main.ScreenToWorldPoint(Input.touches[0].position).x);
        }
    }

    private void UpdatePath(float touchPosition) {
        float newPath = PATHS[0];

        if (Math.Abs(touchPosition) >= 2.25) {
            newPath = PATHS[touchPosition < 0 ? 0 : PATHS.Length - 1];
        } else {
            for (int i = 1; i < PATHS.Length - 1; i++) {
                float path = PATHS[i];

                if (Math.Abs(path - touchPosition) <= 0.75f) {
                    newPath = path;

                    break;
                }
            }
        }

        if (newPath != path) {
            path = newPath;
            lineRenderer.SetPosition(0, new Vector3(path, 0, 0));
            lineRenderer.SetPosition(1, new Vector3(path, 20, 0));
        }
    }
}
