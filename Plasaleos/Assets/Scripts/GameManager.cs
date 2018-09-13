using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    static public GameManager instance;

    private int alienCount = 0;

    // Use this for initialization
    void Awake () {
		if(!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int AlienCount
    {
        get
        {
            return alienCount;
        }

        set
        {
            alienCount = value;
        }
    }
}
