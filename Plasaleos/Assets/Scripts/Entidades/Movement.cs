using UnityEngine;

public class Movement : MonoBehaviour, IState {
    Entity m_entity;
    bool m_wasGrounded;

    private void Awake() {
        m_entity = GetComponent<Entity>();
        m_wasGrounded = false;
    }

    public void StateUpdate(out IState nextState) {
        m_entity.TakeGravityEffect();
        m_entity.CheckForLanding();

        Walk();

        m_entity.CheckForFlip();

        GetComponent<Animator>().SetFloat("Speed", m_entity.m_rb.velocity.magnitude / m_entity.Speed);
        nextState = this;
    }

    public void StateFixedUpdate() {
        float angle = Vector2.Angle(m_entity.EntityRight, -Physics2D.gravity);
        if (m_entity.Grounded && angle <= 90f) {
            m_entity.m_rb.AddForce(m_entity.EntityRight * m_entity.Speed * 2f);
        }
    }

    void Walk() {
        GetComponent<Animator>().SetBool("Moving", true);
        if (m_entity.Grounded && !m_wasGrounded) {
            if (m_entity.m_rb.velocity.magnitude < m_entity.Speed) {
                m_entity.m_rb.velocity += m_entity.EntityRight * m_entity.Speed * 0.5f;
            }
        } else if (!m_entity.Grounded && m_wasGrounded) {
            if (m_entity.m_rb.velocity.magnitude > m_entity.Speed) {
                m_entity.m_rb.velocity -= m_entity.EntityRight * m_entity.Speed * 0.5f;
            }
        }
        m_wasGrounded = m_entity.Grounded;
    }
}