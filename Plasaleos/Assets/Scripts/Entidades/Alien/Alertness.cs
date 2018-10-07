using UnityEngine;

public class Alertness : MonoBehaviour, IState {
    Entity m_entity;
    bool m_alerted = false;

    private void Awake() {
        m_entity = GetComponent<Entity>();
    }

    private void OnEnable() {
        m_alerted = true;
        if (m_entity.m_rb) {
            m_entity.m_rb.velocity = Vector2.zero;
            GetComponent<Animator>().SetBool("Moving", false);
        }
    }

    public void StateUpdate(out IState nextState) {
        if (m_alerted) {
            nextState = this;
        } else {
            nextState = GetComponent<Movement>();
        }
    }

    public void StateFixedUpdate() { }

    public bool ToggleAlert() {
        m_alerted = !m_alerted;
        return m_alerted;
    }
}