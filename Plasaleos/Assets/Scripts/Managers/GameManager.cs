using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    static public GameManager instance;

    private int alienCount = 0;
    private int pieceCount = 0;

    private int[] aliensSaved;
    private int[] piecesSaved;

    public int levelAmount = 3;

    public bool isDragGravity = false;

    // Use this for initialization
    void Awake() {
        if (!instance) {
            instance = this;
            aliensSaved = new int[levelAmount];
            piecesSaved = new int[levelAmount];
            for (int i = 0; i < levelAmount; i++) {
                aliensSaved[i] = 0;
                piecesSaved[i] = 0;
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

    public int PieceCount
    {
        get
        {
            return pieceCount;
        }
    }

    public void SetAlienCount(int alienValue, int pieceValue, string level) {
        int a;
        System.Int32.TryParse(level[level.Length - 1].ToString(), out a);
        int b;
        System.Int32.TryParse(level[level.Length - 2].ToString(), out b);
        b *= 10;
        a += b;
        if (aliensSaved[a] < alienValue) {
            alienCount += alienValue - aliensSaved[a];
            aliensSaved[a] = alienValue;
        }
        if (piecesSaved[a] < pieceValue)
        {
            pieceCount += pieceValue - piecesSaved[a];
            piecesSaved[a] = pieceValue;
        }
        if (alienValue > 0)
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

    public int GetPiecesSavedInLevel(int level)
    {
        return piecesSaved[level];
    }
}