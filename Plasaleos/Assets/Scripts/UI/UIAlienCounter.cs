using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAlienCounter : MonoBehaviour {

    private int totalAliens = -1;
    private int rescuedAliens = -1;
    bool won;

    [SerializeField] private Text text;

    // Use this for initialization
    void Start() {
        totalAliens = GameObject.FindGameObjectsWithTag("Alien").Length;
        rescuedAliens = 0;
        won = false;
    }

    // Update is called once per frame
    void Update() {
        rescuedAliens = LevelManager.instance.RescuedAliens;
        text.text = rescuedAliens + "/" + totalAliens;
        if (rescuedAliens == totalAliens && won == false) {
            AkSoundEngine.PostEvent("Music_Victory", gameObject);
            won = true;
        }
    }
}