using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class GameManager : MonoBehaviour {

    static public GameManager instance;
    
    GameManagerData data;

    public int levelAmount = 3;

    public bool isDragGravity = false;

    public bool resetLevel = false;

    public Vector3 lastPosMainMenu = Vector3.zero;
    string dataPath;
    string json;

    // Use this for initialization
    void Awake() {
        if (!instance) {
            instance = this;
            dataPath = Application.persistentDataPath + "/plasaleos.json";
            if (File.Exists(dataPath)) {
                json = File.ReadAllText(dataPath);
                data = JsonUtility.FromJson<GameManagerData>(json);
                if (data.aliensSaved.Length != levelAmount) {
                    int[] aux = new int[levelAmount];
                    for (int i = 0; i < data.aliensSaved.Length; i++) {
                        aux[i] = data.aliensSaved[i];
                    }
                    data.aliensSaved = aux;
                    for (int i = 0; i < data.piecesSaved.Length; i++) {
                        aux[i] = data.piecesSaved[i];
                    }
                    data.piecesSaved = aux;
                }
            } else {
                CreateFile();
            }
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    void CreateFile() {
        data = new GameManagerData();
        data.aliensSaved = new int[levelAmount];
        data.piecesSaved = new int[levelAmount];
        json = JsonUtility.ToJson(data);
        File.WriteAllText(dataPath, json);
    }

    public int AlienCount {
        get {
            return data.alienCount;
        }
    }

    public int PieceCount {
        get {
            return data.pieceCount;
        }
    }

    public void SetAlienCount(int alienValue, int pieceValue, string level) {
        int a;
        System.Int32.TryParse(level[level.Length - 1].ToString(), out a);
        int b;
        System.Int32.TryParse(level[level.Length - 2].ToString(), out b);
        b *= 10;
        a += b;
        if (data.aliensSaved[a] < alienValue) {
            data.alienCount += alienValue - data.aliensSaved[a];
            data.aliensSaved[a] = alienValue;
        }
        if (data.piecesSaved[a] < pieceValue) {
            data.pieceCount += pieceValue - data.piecesSaved[a];
            data.piecesSaved[a] = pieceValue;
        }
        if (alienValue > 0) {
            Firebase.Analytics.FirebaseAnalytics.LogEvent("LevelWon");
            GameObject gyro = GameObject.FindGameObjectWithTag("Gyroscope");
            if (gyro != null) {
                if (!gyro.GetComponent<GyroController>().dragGravity) {
                    Firebase.Analytics.FirebaseAnalytics.LogEvent("LevelWonWithGyro");
                } else {
                    Firebase.Analytics.FirebaseAnalytics.LogEvent("LevelWonWithDrag");
                }
            }
        }
        json = JsonUtility.ToJson(data);
        File.WriteAllText(dataPath, json);
    }

    public int GetAlienSavedInLevel(string level) {
        int a;
        System.Int32.TryParse(level[level.Length - 1].ToString(), out a);
        int b;
        System.Int32.TryParse(level[level.Length - 2].ToString(), out b);
        b *= 10;
        a += b;
        return data.aliensSaved[a];
    }

    public int GetAlienSavedInLevel(int level) {
        return data.aliensSaved[level];
    }

    public int GetPiecesSavedInLevel(int level) {
        return data.piecesSaved[level];
    }

    public void ResetGame() {
        data.alienCount = 0;
        data.pieceCount = 0;
        for (int i = 0; i < data.aliensSaved.Length; i++) {
            data.aliensSaved[i] = 0;
        }
        for (int i = 0; i < data.piecesSaved.Length; i++) {
            data.piecesSaved[i] = 0;
        }
        json = JsonUtility.ToJson(data);
        File.WriteAllText(dataPath, json);
    }
}