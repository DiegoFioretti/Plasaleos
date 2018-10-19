using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManagerFB : MonoBehaviour {

    static public UserManagerFB instance;

    private int startingValue;
    private int value;

    private void Awake()
    {
        startingValue = Random.Range(0, 10000);
        if(instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        value = startingValue;
	}
	
	public int GetValue()
    {
        value++;
        return value;
    }
}
