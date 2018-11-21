using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    static public LevelManager instance;
    public UnityEvent LevelWon;

    private int aliveAliens = -1;
    private int rescuedAliens = 0;
    private int rescuedPieces = 0;
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

    public int RescuedPieces
    {
        get
        {
            return rescuedPieces;
        }

        set
        {
            rescuedPieces = value;
        }
    }

    private void Awake() {
        if (!instance) {
            instance = this;
            aliveAliens = GameObject.FindGameObjectsWithTag("Alien").Length;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Firebase.Analytics.FirebaseAnalytics.LogEvent("LevelPlayed");
            GameObject gyro = GameObject.FindGameObjectWithTag("Gyroscope");
            if (gyro != null) {
                if (!gyro.GetComponent<GyroController>().dragGravity) {
                    Firebase.Analytics.FirebaseAnalytics.LogEvent("LevelPlayedWithGyro");
                } else {
                    Firebase.Analytics.FirebaseAnalytics.LogEvent("LevelPlayedWithDrag");
                }
            }
        } else {
            Destroy(this);
        }

    }

    // Use this for initialization
    void Start() {
        AkSoundEngine.PostEvent("StopAll", gameObject);
    }

    private void Update() {
        if (rescuedAliens >= aliveAliens) {
            GameManager.instance.SetAlienCount(rescuedAliens, RescuedPieces, SceneManager.GetActiveScene().name);
            var fooGroup = Resources.FindObjectsOfTypeAll<GameObject>();
            if(fooGroup.Length > 0)
            {
                for(int i = 0; i > fooGroup.Length; i++)
                {
                    if(fooGroup[i].tag == "EndScreen")
                    {
                        fooGroup[i].SetActive(true);
                        i = fooGroup.Length;
                    }
                }
            }
            LevelWon.Invoke();
        }
    }
}