using UnityEngine;
using UnityEngine.EventSystems;

public class Mushroom : MonoBehaviour {
    [SerializeField] float m_jumpForce;
    Animator m_animator;

    private void Awake() {
        m_animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb.velocity.y < -1f) {
            rb.angularVelocity = 0f;
            rb.velocity = new Vector2(0f, m_jumpForce);
            m_animator.SetTrigger("Jumped");
        }
    }
}