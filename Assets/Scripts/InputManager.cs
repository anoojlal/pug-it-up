using UnityEngine;
using System;

public class InputManager : MonoBehaviour {

    private static float[] pathPositions = new float[] { -3f, -1.5f, 0, 1.5f, 3f };

    private LineRenderer lineRenderer;
    public float closestPathPosition = pathPositions[0];

    void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update() {
        if (Input.touchCount > 0) {
            GetClosestPathPosition(Camera.main.ScreenToWorldPoint(Input.touches[0].position).x);

            lineRenderer.enabled = true;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, new Vector3(closestPathPosition, 0, 0));
            lineRenderer.SetPosition(1, new Vector3(closestPathPosition, 20, 0));
        }
    }

    private void GetClosestPathPosition(float touchPosition) {
        foreach (float pathPosition in pathPositions) {
            if (Math.Abs(touchPosition - pathPosition) < 0.75f) {
                closestPathPosition = pathPosition;

                break;
            }
        }
    }
}
