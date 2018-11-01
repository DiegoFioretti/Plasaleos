using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColorByCompletion : MonoBehaviour {

    [SerializeField] private int level;

    [SerializeField] private Color completed;
    [SerializeField] private Color finished;

    // Use this for initialization
    void Start () {
        int aliens = GameManager.instance.GetAlienSavedInLevel(level);
        if(aliens >= 4)
        {
            GetComponent<Image>().color = completed;
        }
        else if(aliens > 0)
        {
            GetComponent<Image>().color = finished;
        }
	}
}
