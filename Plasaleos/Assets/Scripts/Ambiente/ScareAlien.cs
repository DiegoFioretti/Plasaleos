using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareAlien : MonoBehaviour {

	public void ChangeToScare()
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("Alien");
        for(int i = 0; i < go.Length; i++)
        {
            go[i].GetComponent<Alien>().Scare();
        }
        /*
        go = GameObject.FindGameObjectsWithTag("Animal");
        for (int i = 0; i < go.Length; i++)
        {
            go[i].GetComponent<Animal>().Scared();
        }*/
    }
}
