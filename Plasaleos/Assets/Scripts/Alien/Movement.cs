using UnityEngine;

public class Movement : State {
    [SerializeField] LayerMask m_ground;
    [SerializeField] float m_speed;
    Rigidbody2D m_rb;
    float m_footOffset;

    private void Awake() {
        m_rb = GetComponent<Rigidbody2D>();
        m_footOffset = GetComponent<SpriteRenderer>().size.y / 2f;
    }

    public override State StateUpdate() {
        Walk();
        return this;
    }

    void Walk() {
        if (IsGrounded()) {
            m_rb.velocity = Vector2.right * m_speed;
        }
    }

    bool IsGrounded() {
        Vector2 footPosition = transform.position;
        footPosition.y -= m_footOffset;
        return (Physics2D.Raycast(footPosition, (-transform.up), 0.01f, m_ground));
    }
}