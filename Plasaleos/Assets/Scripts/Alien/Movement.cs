using UnityEngine;

public class Movement : MonoBehaviour, IState {
    [SerializeField] LayerMask m_groundLayer;
    [SerializeField] float m_speed;
    [SerializeField] float m_detachAngle;
    [SerializeField] bool m_facingRight = true;
    Rigidbody2D m_rb;
    Vector2 m_transformRight;
    Vector2 m_groundNormal;
    float m_footOffset;
    bool m_grounded;
    bool m_wasGrounded;

    private void Awake() {
        m_rb = GetComponent<Rigidbody2D>();
        m_footOffset = GetComponent<SpriteRenderer>().size.y * 0.5f;
        m_wasGrounded = false;
    }

    public void StateUpdate(ref IState nextState) {
        m_grounded = IsGrounded();
        if (m_grounded) {
            if (Vector2.Angle(-transform.up, Physics2D.gravity) > m_detachAngle) {
                m_grounded = false;
            }
        }

        Walk();

        if (m_grounded) {
            transform.eulerAngles = new Vector3(
                0f, 0f, -Vector2.SignedAngle(m_groundNormal, Vector2.up)
            );
            m_transformRight = new Vector2(m_groundNormal.y, -m_groundNormal.x) * (m_facingRight? 1f: -1f);
        } else {
            transform.eulerAngles = new Vector3(
                0f, 0f, -Vector2.SignedAngle(Physics2D.gravity, -Vector2.up)
            );
            m_transformRight = new Vector2(transform.right.x, transform.right.y) * (m_facingRight? 1f: -1f);
        }

        Debug.DrawRay(transform.position, m_transformRight, Color.blue, 1.5f);
        if (m_rb.velocity == Vector2.zero) {
            if (Physics2D.Raycast(transform.position, m_transformRight, 1.5f, m_groundLayer)) {
                Flip();
            }
        }

        if (m_rb.velocity.magnitude > m_speed && m_grounded) {
            m_rb.velocity = Vector3.Normalize(m_rb.velocity) * m_speed * 0.707f;
            //multiply by sqr(2) so that velocity.magnitude ~= m_speed
        }
        nextState = this;
    }

    public void StateFixedUpdate() {
        if (m_grounded) {
            m_rb.AddForce(m_transformRight * m_speed * 1.5f);
        }
    }

    void Walk() {
        if (m_grounded && !m_wasGrounded) {
            if (m_rb.velocity.magnitude < m_speed) {
                m_rb.velocity += m_transformRight * m_speed * 0.5f;
            }
        } else if (!m_grounded && m_wasGrounded) {
            if (m_rb.velocity.magnitude > m_speed) {
                m_rb.velocity -= m_transformRight * m_speed * 0.5f;
            }
        }
        m_wasGrounded = m_grounded;
    }

    bool IsGrounded() {
        RaycastHit2D hit;
        Debug.DrawRay(transform.position, (-transform.up), Color.red, m_footOffset + 0.2f);
        hit = Physics2D.Raycast(transform.position, (-transform.up),
            m_footOffset + 0.2f, m_groundLayer);
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