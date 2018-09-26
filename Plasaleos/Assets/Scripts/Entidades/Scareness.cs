using UnityEngine;

public class Scareness : MonoBehaviour, IState {
    [SerializeField] float m_duration;
    [SerializeField] float m_speed;
    Entity m_entity;
    float m_timeLeft;

    private void Awake() {
        m_entity = GetComponent<Entity>();
    }

    private void OnEnable() {
        m_timeLeft = m_duration;
        m_entity.m_rb.velocity = Vector2.zero;
        m_entity.Flip();
    }

    public void StateUpdate(out IState nextState) {
        if (m_timeLeft <= 0f || !m_entity.IsGrounded()) {
            m_entity.Flip();
            nextState = GetComponent<Movement>();
        } else {
            m_timeLeft -= Time.deltaTime;
            nextState = this;
        }
    }

    public void StateFixedUpdate() {
        m_entity.m_rb.AddForce(transform.right * m_speed *
            (m_entity.FacingRight? 1f: -1f));
    }

}