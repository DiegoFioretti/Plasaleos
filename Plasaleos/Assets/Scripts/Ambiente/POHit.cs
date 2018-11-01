using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POHit : MonoBehaviour {

    [SerializeField] private LayerMask lm;
    [SerializeField] private float minSpeed = 1;
    private Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (rb.velocity.magnitude > minSpeed) {
            if (Vector2.Angle(rb.velocity, -collision.contacts[0].normal) < 90f) {
                Entity entity;
                if (entity = collision.gameObject.GetComponent<Entity>()) {
                    entity.Damage();
                    AkSoundEngine.PostEvent("Alien_Dead", gameObject);
                }
            }
        }
    }
}