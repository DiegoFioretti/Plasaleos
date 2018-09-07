using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTrigger : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Alien")
        {
            LevelManager.instance.RescuedAliens++;
            Destroy(collision.gameObject);
        }
    }
}
