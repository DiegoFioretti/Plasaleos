using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroController : MonoBehaviour {

    private bool gyroEnabled;
    private bool accelEnabled;
    private Gyroscope gyro;

    public bool fixedGravity;

    // Use this for initialization
    void Start ()
    {
#if INPUT_MOBILE
        gyroEnabled = EnableGyro();
        //gyroEnabled = false;
        if(!gyroEnabled)
        {
            //accelEnabled = EnableAccel();
            accelEnabled = true;
        }
#else
        gyroEnabled = false;
        accelEnabled = false;
#endif
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            return true;
        }

        return false;
    }

    private bool EnableAccel()
    {
        if (SystemInfo.supportsAccelerometer)
        {
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gyroEnabled)
        {
            //transform.up = new Vector3(gyro.gravity.x, gyro.gravity.y, 0) * -1;
            transform.up = -gyro.gravity;
        }
        else if(accelEnabled)
        {
            transform.up = -Input.acceleration;
        }

#if INPUT_MOBILE
        if (fixedGravity)
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.z);
#endif
    }
}
