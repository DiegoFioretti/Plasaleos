using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITotalAlien : MonoBehaviour {

    private int totalAliens = -1;

    [SerializeField] private Text text;

    // Use this for initialization
    void Start()
    {
        totalAliens = GameObject.FindGameObjectsWithTag("Alien").Length;
    }

    // Update is called once per frame
    void Update()
    {
        totalAliens = GameManager.instance.AlienCount;
        text.text = totalAliens.ToString();
    }
}
