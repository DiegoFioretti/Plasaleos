using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISpritePos : MonoBehaviour {

    private RectTransform canvasRectT;
    private RectTransform healthBar;
    private Transform objectToFollow;

    private GameObject gyro;
    private RectTransform rectTransform;
    private GyroController gyroController;

    private void Start() {
        healthBar = gameObject.GetComponent<RectTransform>();
        gyro = GameObject.FindGameObjectWithTag("Gyroscope");
        gyroController = gyro.GetComponent<GyroController>();
        rectTransform = GetComponent<RectTransform>();
        canvasRectT = GameObject.FindGameObjectWithTag("AlienCounterCanvas").GetComponent<RectTransform>();
        objectToFollow = GameObject.FindGameObjectWithTag("AlienCounterPoint").transform;
    }

    void Update() {
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, objectToFollow.position);
        healthBar.anchoredPosition = screenPoint - canvasRectT.sizeDelta / 2f;
        if (gyro != null) {
            if (!gyroController.dragGravity) {
                rectTransform.up = gyro.transform.up;
                rectTransform.rotation = Quaternion.Euler(0, 0, rectTransform.rotation.eulerAngles.z);
            }
        }
    }
}