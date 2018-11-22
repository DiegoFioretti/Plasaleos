using UnityEngine;

public class RestrictGravity : MonoBehaviour {
    GravityController m_gravity;

    private void Awake() {
        m_gravity = FindObjectOfType<GravityController>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
    }
}
