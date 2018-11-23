using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColorByCompletion : MonoBehaviour {

    [SerializeField] private int level;
    [SerializeField] private int piecesInLevel;

    [SerializeField] private Color completed;
    [SerializeField] private Color finished;

    // Use this for initialization
    void Update () {
        int aliens = GameManager.instance.GetAlienSavedInLevel(level);
        int pieces = GameManager.instance.GetPiecesSavedInLevel(level);
        if (aliens >= 4 && pieces >= piecesInLevel)
        {
            GetComponent<Image>().color = completed;
        }
        else if(aliens > 0)
        {
            GetComponent<Image>().color = finished;
        }
	}
}
