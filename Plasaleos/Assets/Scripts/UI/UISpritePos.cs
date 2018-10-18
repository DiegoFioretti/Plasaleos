using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISpritePos : MonoBehaviour {

    private RectTransform canvasRectT;
    private RectTransform healthBar;
    private Transform objectToFollow;

    private GameObject gyro;

    private void Start()
    {
        healthBar = gameObject.GetComponent<RectTransform>();
        gyro = GameObject.FindGameObjectWithTag("Gyroscope");
        canvasRectT = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<RectTransform>();
        objectToFollow = GameObject.FindGameObjectWithTag("AlienCounterPoint").transform;
    }

    void Update()
    {
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, objectToFollow.position);
        healthBar.anchoredPosition = screenPoint - canvasRectT.sizeDelta / 2f;
        if(!gyro.GetComponent<GyroController>().dragGravity)
        {
            GetComponent<RectTransform>().up = gyro.transform.up;
            GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, GetComponent<RectTransform>().rotation.eulerAngles.z);
        }
    }
}
