using UnityEngine;

public class Movement : MonoBehaviour, IState {
    [SerializeField] float m_speed;
    [SerializeField] float m_detachAngle;
    Entity m_entity;
    Vector2 m_transformRight;
    Vector2 m_groundNormal;
    Vector2 m_prevGravity;
    bool m_grounded;
    bool m_wasGrounded;

    private void Awake() {
        m_entity = GetComponent<Entity>();
        m_wasGrounded = false;
        m_prevGravity = Physics.gravity;
    }

    public void StateUpdate(out IState nextState) {
        m_grounded = m_entity.IsGrounded(out m_groundNormal);
        if (m_grounded) {
            if (Vector2.Angle(-transform.up, Physics2D.gravity) > m_detachAngle) {
                m_grounded = false;
            }
        }

        if (m_prevGravity != Physics2D.gravity) {
            float currSpeed = m_entity.m_rb.velocity.magnitude;
            m_entity.m_rb.velocity = Physics2D.gravity * currSpeed;
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
        if (m_entity.m_rb.velocity == Vector2.zero) {
            RaycastHit2D hit = Physics2D.Raycast(transform.position,
                m_transformRight, 1.5f, m_entity.GroundLayer);
            if (hit) {

                if (Physics2D.Raycast(transform.position, -m_transformRight, //check if it's stuck in a corner
                        1.5f, m_entity.GroundLayer)) {

                    transform.up = hit.normal;
                }
                m_entity.Flip();
            } else if (m_grounded) {
                m_entity.m_rb.velocity += m_transformRight * m_speed * 0.5f;
            }
        }

        if (m_entity.m_rb.velocity.magnitude > m_speed) {
            m_entity.m_rb.velocity = Vector3.Normalize(
                m_entity.m_rb.velocity) * m_speed * 0.707f * (m_grounded? 1f : 3f);
            //multiply by sqr(2) so that velocity.magnitude ~= m_speed
        }

        GetComponent<Animator>().SetFloat("Speed", m_entity.m_rb.velocity.magnitude / m_speed);
        m_prevGravity = Physics2D.gravity;
        nextState = this;
    }

    public void StateFixedUpdate() {
        if (m_grounded) {
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