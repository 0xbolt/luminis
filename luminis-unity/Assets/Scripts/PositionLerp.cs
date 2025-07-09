using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionLerp : MonoBehaviour {
    public float lerpTime = 1f;
    Vector3 prevPosition;
    Vector3 targetPosition;
    private float timeElapsed = 0f;

    void Start() {
    }

    void Update() {
        var t = timeElapsed / lerpTime;
        while (t <= 1) {
            transform.position = Vector3.Lerp(prevPosition, targetPosition, t);
            timeElapsed += Time.deltaTime;
        }
    }

    public void SetTargetPosition(Vector3 position) {
        prevPosition = transform.position;
        targetPosition = position;
        timeElapsed = 0;
    }
}
