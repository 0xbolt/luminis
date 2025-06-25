using UnityEngine;

public class ArcballCamera : MonoBehaviour {
    public Transform target;
    public float distance = 10.0f;
    public float zoomSpeed = 2.0f;
    public float minDistance = 2.0f;
    public float maxDistance = 50.0f;
    public float orbitSpeed = 5.0f;
    public float panSpeed = 0.5f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    // Start is called before the first frame update
    void Start() {
        if (target != null)
        {
            Vector3 angles = transform.eulerAngles;
            yaw = angles.y;
            pitch = angles.x;
        }
    }

    // Update is called once per frame
    void Update() {
        if (target == null) return;

        // Orbit
        if (Input.GetMouseButton(1)) // Right mouse
        {
            yaw += Input.GetAxis("Mouse X") * orbitSpeed;
            pitch -= Input.GetAxis("Mouse Y") * orbitSpeed;
            pitch = Mathf.Clamp(pitch, -89f, 89f);
        }

        // Zoom
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance -= scroll * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        // Pan
        if (Input.GetMouseButton(2)) // Middle mouse
        {
            float panX = -Input.GetAxis("Mouse X") * panSpeed;
            float panY = -Input.GetAxis("Mouse Y") * panSpeed;
            Vector3 pan = transform.right * panX + transform.up * panY;
            target.position += pan;
        }

        // Calculate new position
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 direction = rotation * Vector3.back;
        transform.position = target.position + direction * distance;
        transform.rotation = rotation;
    }
}
