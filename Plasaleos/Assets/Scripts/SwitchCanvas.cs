using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCanvas : MonoBehaviour {

    [SerializeField] private GameObject canvasUp;
    [SerializeField] private GameObject canvasDown;

    // Update is called once per frame
    void Update() {
        if (!(canvasUp == null || canvasDown == null)) {
            if (GameObject.FindGameObjectWithTag("Gyroscope") != null) {
                if (Physics2D.gravity.y < -9.8f / 3 || GameObject.FindGameObjectWithTag("Gyroscope").GetComponent<GyroController>().dragGravity) {
                    // canvasUp.SetActive(true);
                    // canvasDown.SetActive(false);
                    Vector3 rotation = canvasUp.transform.eulerAngles;
                    rotation.z = 0f;
                    canvasUp.transform.eulerAngles = rotation;
                } else if (Physics2D.gravity.y > 9.8f / 3) {
                    // canvasUp.SetActive(false);
                    // canvasDown.SetActive(true);
                    Vector3 rotation = canvasUp.transform.eulerAngles;
                    rotation.z = 180f;
                    canvasUp.transform.eulerAngles = rotation;
                }
            } else {
                canvasUp.SetActive(true);
                canvasDown.SetActive(false);
            }
        }
    }
}