using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCanvas : MonoBehaviour {
    [SerializeField] private GameObject[] canvases;
    GyroController gyroController;
    bool isUp;

    private void Awake() {
        isUp = true;
        gyroController = GameObject.FindGameObjectWithTag("Gyroscope").GetComponent<GyroController>();
    }

    void Update() {
        if (!isUp && (Physics2D.gravity.y < -9.8f / 3 || gyroController.dragGravity)) {
            foreach (GameObject canvas in canvases) {
                Vector3 rotation = canvas.transform.eulerAngles;
                rotation.z = 0f;
                canvas.transform.eulerAngles = rotation;
            }
            isUp = true;
        } else if (isUp && Physics2D.gravity.y > 9.8f / 3) {
            foreach (GameObject canvas in canvases) {
                Vector3 rotation = canvas.transform.eulerAngles;
                rotation.z = 180f;
                canvas.transform.eulerAngles = rotation;
            }
            isUp = false;
        }
    }
}