using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteAlways]
public class UniverseCameraController : MonoBehaviour {
    public Transform target;
    public float distance = 5f;
    public float scrollSpeed = 0.1f;
    public float rotationSpeed = 4f;
    public bool lockDistance = true;
    public bool lockRoll = true;

    public enum CameraMode {
        Free,
        Orbit,
    }

    void OnValidate() {
        // Rotate camera
        transform.LookAt(target);

        var targetToCamera = transform.position - target.position;
        if (lockDistance) {
            // Translate camera
            transform.position = target.position + targetToCamera.normalized * distance;
        } else {
            // Update distance
            distance = targetToCamera.magnitude;
        }
    }

    void LateUpdate() {
        // Validate position
        OnValidate();
        var eulerAngles = transform.eulerAngles;
        // Calculate rotation
        if (Input.GetMouseButton(0)) {
            eulerAngles.y += Input.GetAxis("Mouse X") * rotationSpeed;
            eulerAngles.x -= Input.GetAxis("Mouse Y") * rotationSpeed;
            if (eulerAngles.x > 180) { eulerAngles.x -= 360; }
            eulerAngles.x = Mathf.Clamp(eulerAngles.x, -89f, 89f);
        }
        // Calculate distance
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance = distance - (1 + distance) * scroll * scrollSpeed;
        distance = distance > 0 ? distance : 0;
        // Update position and rotation
        Quaternion rotation = Quaternion.Euler(eulerAngles);
        transform.position = target.position - rotation * Vector3.forward * distance;
        transform.rotation = rotation;
    }

#if UNITY_EDITOR
    void OnDrawGizmos() {
        if (target != null) {
            Gizmos.DrawLine(transform.position, target.position);
        }
    }
#endif


}
