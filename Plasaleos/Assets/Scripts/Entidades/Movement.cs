using UnityEngine;

public class Movement : MonoBehaviour, IState {
    [SerializeField] float m_speed;
    [SerializeField] float m_detachAngle;
    Entity m_entity;
    Vector2 m_transformRight;
    Vector2 m_groundNormal;
    Vector2 m_prevGravity;
    float m_speedMultiplier;
    bool m_grounded;
    bool m_wasGrounded;

    private void Awake() {
        m_entity = GetComponent<Entity>();
        m_wasGrounded = false;
        m_prevGravity = Physics.gravity;
        m_speedMultiplier = 1f;
    }

    public void StateUpdate(out IState nextState) {
        m_grounded = m_entity.IsGrounded(out m_groundNormal);
        if (m_grounded) {
            if (Vector2.Angle(-transform.up, Physics2D.gravity) > m_detachAngle) {
                m_grounded = false;
            }
        }

        float angle = Vector2.Angle(m_transformRight, Physics2D.gravity);
        if (!m_grounded) {
            m_speedMultiplier = 2.5f;
        } else if (m_grounded && angle < 90f) {
            m_speedMultiplier = 1.5f;
        } else if (m_grounded && angle > 90) {
            m_speedMultiplier = 0.5f;
        } else {
            m_speedMultiplier = 1f;
        }

        if ((!m_grounded || (m_grounded && angle == 90f)) &&
            (m_prevGravity != Physics2D.gravity)) {

            float newSpeed = m_entity.m_rb.velocity.magnitude * 0.7f;
            m_entity.m_rb.velocity = Physics2D.gravity.normalized * newSpeed;
        }

        Walk();

        if (m_grounded) {
            transform.eulerAngles = new Vector3(
                0f, 0f, -Vector2.SignedAngle(m_groundNormal, Vector2.up)
            );
            m_transformRight = new Vector2(
                m_groundNormal.y, -m_groundNormal.x) * (m_entity.FacingRight? 1f: -1f);
        } else {
            transform.eulerAngles = new Vector3(
                0f, 0f, -Vector2.SignedAngle(Physics2D.gravity, -Vector2.up)
            );
            m_transformRight = new Vector2(
                transform.right.x, transform.right.y) * (m_entity.FacingRight? 1f: -1f);
        }

        // Debug.DrawRay(transform.position, m_transformRight, Color.blue, 1.5f);
        RaycastHit2D hit = Physics2D.Raycast(transform.position,
            m_transformRight, 1.25f, m_entity.GroundLayer);
        if (hit) {

            if (Physics2D.Raycast(transform.position, -m_transformRight, //check if it's stuck in a corner
                    1.5f, m_entity.GroundLayer)) {

                transform.up = hit.normal;
            }
            m_entity.Flip();
        }

        if (m_entity.m_rb.velocity.magnitude > m_speed * m_speedMultiplier) {
            m_entity.m_rb.velocity = m_entity.m_rb.velocity.normalized *
                m_speed * m_speedMultiplier * 0.707f;
            //multiply by sqr(2) so that velocity.magnitude ~= m_speed
        }

        GetComponent<Animator>().SetFloat("Speed", m_entity.m_rb.velocity.magnitude / m_speed);
        m_prevGravity = Physics2D.gravity;
        nextState = this;
    }

    public void StateFixedUpdate() {
        float angle = Vector2.Angle(m_transformRight, -Physics2D.gravity);
        if (m_grounded && angle <= 90f) {
            m_entity.m_rb.AddForce(m_transformRight * m_speed * 2f);
        }
    }

    void Walk() {
        GetComponent<Animator>().SetBool("Moving", true);
        if (m_grounded && !m_wasGrounded) {
            if (m_entity.m_rb.velocity.magnitude < m_speed) {
                m_entity.m_rb.velocity += m_transformRight * m_speed * 0.5f;
            }
        } else if (!m_grounded && m_wasGrounded) {
            if (m_entity.m_rb.velocity.magnitude > m_speed) {
                m_entity.m_rb.velocity -= m_transformRight * m_speed * 0.5f;
            }
        }
        m_wasGrounded = m_grounded;
    }

    public Vector2 GetTransformRight() {
        return m_transformRight;
    }

    public bool IsGrounded() {
        return m_grounded;
    }
}