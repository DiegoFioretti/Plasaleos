using UnityEngine;

public class Movement : MonoBehaviour, IState {
    [SerializeField] LayerMask m_groundLayer;
    [SerializeField] float m_speed;
    [SerializeField] bool m_facingRight = true;
    Rigidbody2D m_rb;
    Vector2 transformRight;
    Vector2 m_groundNormal;
    float m_footOffset;
    bool m_grounded;
    bool m_wasGrounded;

    private void Awake() {
        m_rb = GetComponent<Rigidbody2D>();
        m_footOffset = GetComponent<SpriteRenderer>().size.y / 2f;
        m_wasGrounded = false;
    }

    public IState StateUpdate() {
        m_grounded = IsGrounded();
        if (m_grounded) {
            transform.eulerAngles = new Vector3(
                0f, 0f, Vector2.SignedAngle(m_groundNormal, Vector2.up)
            );
        } else {
            transform.eulerAngles = new Vector3(
                0f, 0f, -Vector2.SignedAngle(Physics2D.gravity, -Vector2.up)
            );
        }
        if (m_grounded) {
            transformRight = new Vector2(m_groundNormal.y, m_groundNormal.x);
        } else {
            transformRight = new Vector2(transform.right.x, transform.right.y);
        }
        if (m_rb.velocity == Vector2.zero) {
            if (Physics2D.Raycast(transform.position, transform.right, 0.5f, m_groundLayer)) {
                Flip();
            }
        }
        Walk();
        return this;
    }

    public void StateFixedUpdate() {
        if (m_grounded) {
            m_rb.AddForce(transformRight * m_speed * 1.5f);
        }
    }

    void Walk() {
        if (m_grounded && !m_wasGrounded) {
            if (m_rb.velocity.magnitude < m_speed) {
                m_rb.velocity += transformRight * m_speed;
            }
        } else if (!m_grounded && m_wasGrounded) {
            if (m_rb.velocity.magnitude > m_speed) {
                m_rb.velocity -= transformRight * m_speed;
            }
        }
        m_wasGrounded = m_grounded;
    }

    bool IsGrounded() {
        RaycastHit2D hit;
        Debug.DrawRay(transform.position, (-transform.up), Color.red, m_footOffset + 0.25f);
        hit = Physics2D.Raycast(transform.position, (-transform.up),
            m_footOffset + 0.25f, m_groundLayer);
        m_groundNormal = hit.normal;
        return (hit);
    }

    void Flip() {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
        m_facingRight = !m_facingRight;
    }
}