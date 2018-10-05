using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroController : MonoBehaviour {

    private bool gyroEnabled;
    private bool accelEnabled;
    private Gyroscope gyro;
    float angle;

    public bool fixedGravity;

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
        if (gyroEnabled) {
            angle = Vector2.SignedAngle(gyro.gravity, -transform.up);
        } else if (accelEnabled) {
            angle = Vector2.SignedAngle(Input.acceleration, -transform.up);
        }
        if (Mathf.Abs(angle) >= 75f) {
            transform.up = transform.right * Mathf.Sign(angle);
        }

#if INPUT_MOBILE
        if (fixedGravity)
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.z);
#endif
    }
}