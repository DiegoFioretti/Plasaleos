using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroController : MonoBehaviour {

    private bool gyroEnabled;
    private bool accelEnabled;
    private Gyroscope gyro;
    float angle;

    public bool dragGravity = false;
    [SerializeField] private float dragMagnitude = 3;

    private Vector2 startPos;
    private Vector2 endPos;
    private bool directionChosen;

    // Use this for initialization
    void Start() {
#if INPUT_MOBILE
        gyroEnabled = EnableGyro();
        //gyroEnabled = false;
        if (!gyroEnabled) {
            //accelEnabled = EnableAccel();
            accelEnabled = true;
        }
#else
        gyroEnabled = false;
        accelEnabled = false;
#endif

        dragGravity = GameManager.instance.isDragGravity;
    }

    private bool EnableGyro() {
        if (SystemInfo.supportsGyroscope) {
            gyro = Input.gyro;
            gyro.enabled = true;
            return true;
        }

        return false;
    }

    private bool EnableAccel() {
        if (SystemInfo.supportsAccelerometer) {
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (dragGravity) {
            Vector2 gravityDir;
            if (Input.touchCount > 0) {
                Touch touch = Input.GetTouch(0);

                // Handle finger movements based on touch phase.
                switch (touch.phase) {
                    // Record initial touch position.
                    case TouchPhase.Began:
                        startPos = touch.position;
                        directionChosen = false;
                        break;

                        // Report that a direction has been chosen when the finger is lifted.
                    case TouchPhase.Moved:
                        endPos = touch.position;
                        gravityDir = (endPos - startPos);
                        if (gravityDir.magnitude > dragMagnitude)
                            directionChosen = true;
                        break;
                }
            }
            if (directionChosen) {
                directionChosen = false;
                gravityDir = (endPos - startPos);
                transform.up = -gravityDir;
            }
        } else {
            if (gyroEnabled) {
                Vector3 down = -gyro.gravity;
                down.z = 0;
                transform.up = -gyro.gravity;
            } else if (accelEnabled) {
                transform.up = -Input.acceleration;
            }
        }
    }
}