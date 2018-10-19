using UnityEngine;

public class Movement : MonoBehaviour, IState {
    Entity m_entity;
    float m_stuckCounter;

    private void Awake() {
        m_entity = GetComponent<Entity>();
    }

    private void OnEnable() {
        GetComponent<Animator>().SetBool("Moving", true);
        m_stuckCounter = 0f;
    }

    public void StateUpdate(out IState nextState) {
        if (m_entity.m_rb.velocity.magnitude <= 0.2f) {
            m_stuckCounter += Time.deltaTime;
            if (m_stuckCounter >= 0.15f) {
                m_entity.Flip();
                m_stuckCounter = 0f;
            }
        } else {
            m_stuckCounter = 0f;
        }

        m_entity.CheckForLanding();
        m_entity.TakeGravityEffect();
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

}