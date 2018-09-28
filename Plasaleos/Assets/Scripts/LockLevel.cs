using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockLevel : MonoBehaviour {

    private Button button;
    [SerializeField] private Text text;
    [SerializeField] private int unlock;

	// Use this for initialization
	void Start () {
        button = gameObject.GetComponent<Button>();
        if(unlock > GameManager.instance.AlienCount)
        {
            button.enabled = false;
            text.text = "Save " + unlock + " Aliens";
        }
	}
}
