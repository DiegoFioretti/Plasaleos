using UnityEngine;

[RequireComponent(typeof(Entity))]
public class Alertness : MonoBehaviour, IState {
    [SerializeField] float m_duration;
    Entity m_entity;
    float m_timeLeft;

    private void Awake() {
        m_entity = GetComponent<Entity>();
    }

    private void OnEnable() {
        m_timeLeft = m_duration;
        if (m_entity.m_rb) {
            m_entity.m_rb.velocity = Vector2.zero;
        }
    }

    public void StateUpdate(out IState nextState) {
        if (m_timeLeft <= 0f) {
            nextState = GetComponent<Movement>();
        } else {
            m_timeLeft -= Time.deltaTime;
            nextState = this;
        }
    }

    public void StateFixedUpdate() { }

}