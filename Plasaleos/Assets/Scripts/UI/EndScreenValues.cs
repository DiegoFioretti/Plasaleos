using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenValues : MonoBehaviour {

    [SerializeField] private Text title;
    [SerializeField] private Text scores;

    private void Awake() {
        LevelManager.instance.LevelWon.AddListener(Activate);
    }

    private void OnEnable()
    {
        if(LevelManager.instance.RescuedAliens > 0)
        {
            title.text = "Victory";
        }
        else
        {
            title.text = "Game Over";
        }
        scores.text = "Saved: " + LevelManager.instance.RescuedAliens + "\n" + "Total: " + GameManager.instance.AlienCount;
    }

    void Activate() {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
}
