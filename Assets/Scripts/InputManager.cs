using UnityEngine;
using System;

public class InputManager : MonoBehaviour {

    private static float[] pathPositions = new float[] { -3f, -1.5f, 0, 1.5f, 3f };

    private LineRenderer lineRenderer;
    public float closestPathPosition = pathPositions[0];

    void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = true;
        lineRenderer.positionCount = 2;
    }

    void Update() {
        if (Input.touchCount > 0) {
            if (UpdateClosestPathPosition(Camera.main.ScreenToWorldPoint(Input.touches[0].position).x)) {
                lineRenderer.SetPosition(0, new Vector3(closestPathPosition, 0, 0));
                lineRenderer.SetPosition(1, new Vector3(closestPathPosition, 20, 0));
            }
        }
    }

    private bool UpdateClosestPathPosition(float touchPosition) {
        float newClosestPathPosition = pathPositions[0];

        if (Math.Abs(touchPosition) > 2.25) {
            newClosestPathPosition = pathPositions[touchPosition < 0 ? 0 : pathPositions.Length - 1];
        } else {
            for (int i = 1; i < pathPositions.Length - 1; i++) {
                float pathPosition = pathPositions[i];

                if (Math.Abs(pathPosition - touchPosition) < 0.75f) {
                    newClosestPathPosition = pathPosition;

                    break;
                }
            }
        }

        if (newClosestPathPosition != closestPathPosition) {
            closestPathPosition = newClosestPathPosition;
            Debug.Log("Update closestPathPosition");

            return true;
        }

        return false;
    }
}
