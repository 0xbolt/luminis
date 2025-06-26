using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteAlways]
public class OrbitCamera : MonoBehaviour {
    public Transform target;
    [Range(1f, 20f)]
    public float distance = 5f;
    [Range(1f, 10f)]
    public float speed = 2f;
    public bool lockDistance = true;

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
        // Calculate rotation
        var angles = transform.eulerAngles;
        if (Input.GetMouseButton(0)) {
            angles.y += Input.GetAxis("Mouse X") * speed;
            angles.x -= Input.GetAxis("Mouse Y") * speed;
            angles.x = Mathf.Clamp(angles.x, -89f, 89f);
        }
        // Update position and rotation
        Quaternion rotation = Quaternion.Euler(angles);
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
