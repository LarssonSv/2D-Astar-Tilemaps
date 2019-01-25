using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    void FixedUpdate() {
        if (target) {

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        }
        else {
            try {
                target = GameObject.FindGameObjectWithTag("Player").transform;
            }
            catch  {
                Debug.Log("Waiting for player in scene");
            }
        }

    }
}
