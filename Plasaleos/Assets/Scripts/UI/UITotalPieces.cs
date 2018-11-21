using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITotalPieces : MonoBehaviour {

    private int totalPieces = -1;

    [SerializeField] private Text text;

    // Use this for initialization
    void Start()
    {
        totalPieces = GameObject.FindGameObjectsWithTag("Alien").Length;
    }

    // Update is called once per frame
    void Update()
    {
        totalPieces = GameManager.instance.PieceCount;
        text.text = totalPieces.ToString();
    }
}
