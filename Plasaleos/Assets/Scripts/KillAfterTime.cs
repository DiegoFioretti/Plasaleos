using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAfterTime : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("Kill", 2);
	}

    private void Kill()
    {
        Destroy(gameObject);
    }
}
