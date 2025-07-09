using System;
using UnityEngine;

public class Arcball : MonoBehaviour {
    public Transform target;
    public float radius = 1.0f;
    private Vector3? lastVec;
    private Vector3 targetToCamera;

    void Start() {
        if (target != null) {
            targetToCamera = transform.position - target.position;
        }
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            lastVec = ProjectToSphere(Input.mousePosition);
        } else if (Input.GetMouseButton(0)) {
            Vector3 currentVec = ProjectToSphere(Input.mousePosition);
            Quaternion q = Quaternion.FromToRotation(lastVec.Value, currentVec);
            transform.rotation = q * transform.rotation;
            lastVec = currentVec;
        } else if (Input.GetMouseButtonUp(0)) {
            lastVec = null;
        }
    }

    Vector3 ProjectToSphere(Vector3 mousePos) {
        Vector2 screen = new Vector2(Screen.width, Screen.height);
        Vector2 p = (new Vector2(mousePos.x, mousePos.y) - screen / 2f) / (radius * screen.y / 2f);
        float mag = p.sqrMagnitude;
        float z = mag < 1.0f ? Mathf.Sqrt(1.0f - mag) : 0.0f;
        return new Vector3(p.x, p.y, z).normalized;
    }
}
