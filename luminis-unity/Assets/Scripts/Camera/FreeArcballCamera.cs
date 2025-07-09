using UnityEngine;

public class FreeArcballCamera : MonoBehaviour {
    public Transform target;
    public float distance = 5f;
    public float zoomSpeed = 2f;
    public float minDistance = 1f, maxDistance = 10f;
    public float rotationSpeed = 1.2f;

    Quaternion rotation = Quaternion.identity;
    Vector3? lastPos = null;

    void Update() {
        // Rotation
        if (Input.GetMouseButton(0)) {
            Vector3 cur = ProjectToSphere(Input.mousePosition);
            if (lastPos.HasValue) {
                Vector3 prev = lastPos.Value;
                Vector3 axis = Vector3.Cross(prev, cur);
                float angle = Vector3.Angle(prev, cur) * rotationSpeed;
                Quaternion delta = Quaternion.AngleAxis(angle, axis.normalized);
                rotation = delta * rotation;
            }
            lastPos = cur;
        } else {
            lastPos = null;
        }

        // Zoom
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance = Mathf.Clamp(distance - scroll * zoomSpeed, minDistance, maxDistance);

        // Apply
        Vector3 offset = rotation * new Vector3(0, 0, -distance);
        transform.position = target.position + offset;
        transform.LookAt(target.position);
    }

    Vector3 ProjectToSphere(Vector3 mouse) {
        Vector2 view = new Vector2(Screen.width, Screen.height);
        Vector2 p = (new Vector2(mouse.x, mouse.y) / view) * 2f - Vector2.one;
        p.y = -p.y;

        float mag = p.sqrMagnitude;
        float z = mag < 1f ? Mathf.Sqrt(1f - mag) : 0f;
        return new Vector3(p.x, p.y, z).normalized;
    }
}
