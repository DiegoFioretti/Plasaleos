using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Alien"){
            other.gameObject.SetActive(false);
            LevelManager.instance.AliveAliens--;
        }
        
    }
}
