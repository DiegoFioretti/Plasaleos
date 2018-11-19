using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    static public GameManager instance;

    private int alienCount = 0;
    public uint pieceCount = 0;

    private int[] aliensSaved;

    public int levelAmount = 3;

    public bool isDragGravity = false;

    // Use this for initialization
    void Awake() {
        if (!instance) {
            instance = this;
            aliensSaved = new int[levelAmount];
            for (int i = 0; i < levelAmount; i++) {
                aliensSaved[i] = 0;
            }
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update() {

    }

    public int AlienCount {
        get {
            return alienCount;
        }
    }

    public void SetAlienCount(int value, string level) {
        int a;
        System.Int32.TryParse(level[level.Length - 1].ToString(), out a);
        int b;
        System.Int32.TryParse(level[level.Length - 2].ToString(), out b);
        b *= 10;
        a += b;
        if (aliensSaved[a] < value) {
            alienCount += value - aliensSaved[a];
            aliensSaved[a] = value;
        }
        if(value > 0)
        {
            Firebase.Analytics.FirebaseAnalytics.LogEvent("LevelWon");
            GameObject gyro = GameObject.FindGameObjectWithTag("Gyroscope");
            if (gyro != null)
            {
                if (!gyro.GetComponent<GyroController>().dragGravity)
                {
                    Firebase.Analytics.FirebaseAnalytics.LogEvent("LevelWonWithGyro");
                }
                else
                {
                    Firebase.Analytics.FirebaseAnalytics.LogEvent("LevelWonWithDrag");
                }
            }
        }

    }

    public int GetAlienSavedInLevel(string level)
    {
        int a;
        System.Int32.TryParse(level[level.Length - 1].ToString(), out a);
        int b;
        System.Int32.TryParse(level[level.Length - 2].ToString(), out b);
        b *= 10;
        a += b;
        return aliensSaved[a];
    }

    public int GetAlienSavedInLevel(int level)
    {
        return aliensSaved[level];
    }
}