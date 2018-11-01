using UnityEngine;
using UnityEngine.EventSystems;

public class Mushroom : MonoBehaviour {
    [SerializeField] float m_jumpForce;
    [SerializeField] float m_horizontalSpeed;
    Animator m_animator;

    private void Awake() {
        m_animator = GetComponentInChildren<Animator>();
    }

    private void Start() {
        AkSoundEngine.PostEvent("ObjectSpawn", gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb.velocity.y < -1f) {
            Entity m_entity = other.GetComponent<Entity>();
            float direction = 1f;
            if (m_entity) {
                m_entity.Jump();
                if (!m_entity.FacingRight) {
                    direction = -1f;
                }
            } else {
                if (rb.velocity.x < 0f) {
                    direction = -1f;
                }
            }
            rb.angularVelocity = 0f;
            rb.velocity = new Vector2(m_horizontalSpeed * direction, m_jumpForce);
            m_animator.SetTrigger("Jumped");
            AkSoundEngine.PostEvent("ObjectBounce", gameObject);
        }
    }
}