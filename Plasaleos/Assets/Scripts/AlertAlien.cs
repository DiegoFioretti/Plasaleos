using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertAlien : MonoBehaviour {

    public void ChangeToAlert()
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("Alien");
        for (int i = 0; i < go.Length; i++)
        {
            go[i].GetComponent<Alien>().Alert();
        }
    }
}
