using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    static public LevelManager instance;

    private int maxAliens = -1;

    private int aliveAliens = -1;

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

    public int AliveAliens
    {
        get{
            return aliveAliens;
        }
        set{
            aliveAliens = value;
        }
    }

    // Use this for initialization
    void Start () {
        if (!instance)
        {
            instance = this;
            maxAliens = GameObject.FindGameObjectsWithTag("Alien").Length;
            aliveAliens = GameObject.FindGameObjectsWithTag("Alien").Length;
        }
        else
        {
            Destroy(this);
        }
	}

    private void Update() {
        if(rescuedAliens >= aliveAliens)
        {
            GameManager.instance.SetAlienCount(rescuedAliens, SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("MainMenu");
        }
    }
}
