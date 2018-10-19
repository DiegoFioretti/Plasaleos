using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCanvas : MonoBehaviour {

    [SerializeField] private GameObject canvasUp;
    [SerializeField] private GameObject canvasDown;

    // Update is called once per frame
    void Update()
    {
        Debug.LogError(Physics.gravity.y);
        if (Physics2D.gravity.y < 0 || GameObject.FindGameObjectWithTag("Gyroscope").GetComponent<GyroController>().dragGravity)
        {
            canvasUp.SetActive(true);
            canvasDown.SetActive(false);
        }
        else
        {
            canvasUp.SetActive(false);
            canvasDown.SetActive(true);
        }
    }
}
