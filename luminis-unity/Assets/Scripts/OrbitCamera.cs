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
    public float speed = 4f;
    public bool lockDistance = true;

    private Vector3 eulerAngles;

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

    void Start() {
        eulerAngles = transform.eulerAngles;
    }

    void LateUpdate() {
        // Validate position
        OnValidate();
        // Calculate rotation
        if (Input.GetMouseButton(0)) {
            eulerAngles.y += Input.GetAxis("Mouse X") * speed;
            eulerAngles.x -= Input.GetAxis("Mouse Y") * speed;
            eulerAngles.x = Mathf.Clamp(eulerAngles.x, -89f, 89f);
        }
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
