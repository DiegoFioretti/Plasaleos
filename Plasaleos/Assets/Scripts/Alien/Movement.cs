using UnityEngine;

public class Movement : MonoBehaviour, IState {
    [SerializeField] LayerMask m_ground;
    [SerializeField] float m_speed;
    Rigidbody2D m_rb;
    float m_footOffset;
    bool m_wasGrounded;

    private void Awake() {
        m_rb = GetComponent<Rigidbody2D>();
        m_footOffset = GetComponent<SpriteRenderer>().size.y / 2f;
        m_wasGrounded = false;
    }

    public IState StateUpdate() {
        Walk();
        return this;
    }

    public void StateFixedUpdate() { }

    void Walk() {
        bool grounded = IsGrounded();
        if (grounded && !m_wasGrounded) {
            // m_rb.AddForce(Vector2.right * m_speed);
            m_rb.velocity += Vector2.right * m_speed;
        } else if (!grounded && m_wasGrounded) {
            m_rb.velocity -= Vector2.right * m_speed;
        }
        m_wasGrounded = grounded;
    }

    bool IsGrounded() {
        Vector2 footPosition = transform.position;
        footPosition.y -= m_footOffset;
        return (Physics2D.Raycast(footPosition, (-transform.up), 0.1f, m_ground));
    }
}