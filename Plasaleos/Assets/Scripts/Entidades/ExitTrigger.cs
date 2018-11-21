using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTrigger : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Alien") {
            if (!collision.GetComponent<Alien>().IsDead) {

                LevelManager.instance.RescuedAliens++;
                AkSoundEngine.PostEvent("Alien_Rescued", gameObject);
                collision.gameObject.SetActive(false);
            }
        }
        if (collision.tag == "ShipPiece") {
            LevelManager.instance.RescuedPieces++;
            AkSoundEngine.PostEvent("Alien_Rescued", gameObject);
            collision.gameObject.SetActive(false);
            Debug.LogError("WTF?");
        }
        Debug.LogError("WTF???????????");
    }
}