using UnityEngine;
using System;

public class InputManager : MonoBehaviour {

    private static float[] PATHS = new float[] { -3f, -1.5f, 0, 1.5f, 3f };

    private LineRenderer lineRenderer;
    public float selectedPath = PATHS[0];

    void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = true;
        lineRenderer.positionCount = 2;
    }

    void Update() {
        if (Input.touchCount > 0) {
            UpdateSelectedPath(Camera.main.ScreenToWorldPoint(Input.touches[0].position).x);
        }
    }

    private void UpdateSelectedPath(float touchPosition) {
        float newPath = PATHS[0];

        if (Math.Abs(touchPosition) > 2.25) {
            newPath = PATHS[touchPosition < 0 ? 0 : PATHS.Length - 1];
        } else {
            for (int i = 1; i < PATHS.Length - 1; i++) {
                float path = PATHS[i];

                if (Math.Abs(path - touchPosition) < 0.75f) {
                    newPath = path;

                    break;
                }
            }
        }

        if (newPath != selectedPath) {
            selectedPath = newPath;
            lineRenderer.SetPosition(0, new Vector3(selectedPath, 0, 0));
            lineRenderer.SetPosition(1, new Vector3(selectedPath, 20, 0));
        }
    }
}
