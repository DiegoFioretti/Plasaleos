using UnityEngine;

public class Movement : MonoBehaviour, IState {
    Entity m_entity;

    private void Awake() {
        m_entity = GetComponent<Entity>();
    }

    private void OnEnable() {
        GetComponent<Animator>().SetBool("Moving", true);
    }

    private void OnDisable() {
        GetComponent<Animator>().SetBool("Moving", false);
    }

    public void StateUpdate(out IState nextState) {
        m_entity.CheckForLanding();
        m_entity.CheckForFlip();
        m_entity.TakeGravityEffect();

        GetComponent<Animator>().SetFloat("Speed",
            m_entity.m_rb.velocity.magnitude / m_entity.Speed);
        nextState = this;
    }

    public void StateFixedUpdate() {
        float angle = Vector2.Angle(m_entity.EntityRight, -Physics2D.gravity);
        if (m_entity.Grounded && angle <= 90f) {
            m_entity.m_rb.AddForce(m_entity.EntityRight * m_entity.Speed * 2f);
        }
    }

}