using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowTutorial : MonoBehaviour {

	// Use this for initialization
	void Start() {
		if (GameManager.instance.GetAlienSavedInLevel(SceneManager.GetActiveScene().name) > 0) {
			gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>().enabled = true;
		}
	}
}