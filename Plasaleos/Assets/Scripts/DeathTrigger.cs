using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    LevelManager manager;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Alien")
        {
            other.gameObject.SetActive(false);
            AkSoundEngine.PostEvent("Alien_Dead", gameObject);
            if(tag == "Trap")
            {
                AkSoundEngine.PostEvent("Trap_Hit", gameObject);
            }
            manager.AliveAliens--;
        }

    }
}