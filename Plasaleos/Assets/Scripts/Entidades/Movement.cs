using UnityEngine;

public class Movement : MonoBehaviour, IState {
    Entity m_entity;

    private void Awake() {
        m_entity = GetComponent<Entity>();
    }

    private void OnEnable() {
        GetComponent<Animator>().SetBool("Moving", true);
    }

    public void StateUpdate(out IState nextState) {
        m_entity.TakeGravityEffect();
        m_entity.CheckForLanding();

        m_entity.CheckForFlip();

        GetComponent<Animator>().SetFloat("Speed", m_entity.m_rb.velocity.magnitude / m_entity.Speed);
        nextState = this;
    }

    public void StateFixedUpdate() {
        float angle = Vector2.Angle(m_entity.EntityRight, -Physics2D.gravity);
        if (m_entity.Grounded && angle <= 90f) {
            Vector2 newSpeed = m_entity.m_rb.velocity;
            newSpeed.x = m_entity.Speed * (m_entity.FacingRight? 1f: -1f);
            m_entity.m_rb.velocity = newSpeed;
        }
    }

}