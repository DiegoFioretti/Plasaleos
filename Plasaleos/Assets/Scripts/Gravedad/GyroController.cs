using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroController : MonoBehaviour {
    [SerializeField] float rotAcceleration;
    [SerializeField] float threshold;
    private bool gyroEnabled;
    private bool accelEnabled;
    private Gyroscope gyro;
    Vector3 initalRot;
    float angle;

    public bool fixedGravity;

    // Use this for initialization
    void Start() {
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = false;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
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
            transform.up = Vector2.up;
            initalRot = gyro.attitude.eulerAngles;
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
        Vector3 rotation = transform.eulerAngles;
        if (gyroEnabled) {
            Vector3 diff = gyro.attitude.eulerAngles - initalRot;
            if (Mathf.Abs(diff.z) > threshold) {
                rotation.z -= diff.z * Time.deltaTime * rotAcceleration;
            }
            // transform.up = -gyro.gravity;
        } else if (accelEnabled) {
            rotation.z -= Input.acceleration.z;
            // transform.up = -Input.acceleration;
        }
        transform.eulerAngles = rotation;
    }
}