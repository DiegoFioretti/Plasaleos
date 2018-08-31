using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    static public LevelManager instance;

    private int maxAliens = -1;

    private int rescuedAliens = 0;

    public int RescuedAliens
    {
        get
        {
            return rescuedAliens;
        }

        set
        {
            rescuedAliens = value;
        }
    }

    // Use this for initialization
    void Start () {
        if (!instance)
        {
            instance = this;
            maxAliens = GameObject.FindGameObjectsWithTag("Alien").Length;
        }
        else
        {
            Destroy(this);
        }
	}

    private void Update() {
        if(rescuedAliens >= maxAliens)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
