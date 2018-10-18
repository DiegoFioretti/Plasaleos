using UnityEngine;

public class JumpToMushroom : MonoBehaviour {
    [SerializeField] float m_jumpForce;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Alien") {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            Alien m_entity = other.GetComponent<Alien>();
            if (m_entity) {
                m_entity.Jump();
            }
            rb.angularVelocity = 0f;
            rb.velocity = transform.up * m_jumpForce;
        }
    }
}