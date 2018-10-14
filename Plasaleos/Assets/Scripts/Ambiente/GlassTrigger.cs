using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassTrigger : MonoBehaviour {

    [SerializeField] private float minForce = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if (go.GetComponent<Rigidbody2D>().velocity.magnitude > minForce)
        {
            AkSoundEngine.PostEvent("Hazard_BrokenIce", gameObject);
            Destroy(gameObject);
        }
    }
}
