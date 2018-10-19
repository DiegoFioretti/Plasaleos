using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    static public LevelManager instance;

    private int maxAliens = -1;

    private int aliveAliens = -1;

    private int rescuedAliens = 0;

    public int RescuedAliens {
        get {
            return rescuedAliens;
        }

        set {
            rescuedAliens = value;
        }
    }

    public int AliveAliens {
        get {
            return aliveAliens;
        }
        set {
            aliveAliens = value;
        }
    }

    // Use this for initialization
    void Start() {
        if (!instance) {
            AkSoundEngine.PostEvent("StopAll", gameObject);
            instance = this;
            maxAliens = GameObject.FindGameObjectsWithTag("Alien").Length;
            aliveAliens = GameObject.FindGameObjectsWithTag("Alien").Length;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Firebase.Analytics.FirebaseAnalytics.LogEvent("LevelPlayed");
            GameObject gyro = GameObject.FindGameObjectWithTag("Gyroscope");
            if (gyro != null)
            {
                if (!gyro.GetComponent<GyroController>().dragGravity)
                {
                    Firebase.Analytics.FirebaseAnalytics.LogEvent("LevelPlayedWithGyro");
                }
                else
                {
                    Firebase.Analytics.FirebaseAnalytics.LogEvent("LevelPlayedWithDrag");
                }
            }
        } else {
            Destroy(this);
        }
    }

    private void Update() {
        if (rescuedAliens >= aliveAliens) {
            GameManager.instance.SetAlienCount(rescuedAliens, SceneManager.GetActiveScene().name);
            //SceneManager.LoadScene("MainMenu");
            //GameObject.FindGameObjectWithTag("EndScreen").SetActive(true);
            var fooGroup = Resources.FindObjectsOfTypeAll<GameObject>();
            if (fooGroup.Length > 0) {
                for (int i = 0; i < fooGroup.Length; i++) {
                    if (fooGroup[i].tag == "EndScreen") {
                        fooGroup[i].SetActive(true);
                        i = fooGroup.Length;
                    }
                }
            }
        }
    }
}