using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Scareness))]
[RequireComponent(typeof(Alertness))]
[RequireComponent(typeof(Death))]
sealed class Alien : Entity {
    IState m_currState;
    IState m_nextState;
    bool m_scare;
    bool m_alert;
    bool m_death;

    protected override void Awake() {
        base.Awake();
        SetStateActive(GetComponent<Movement>(), true);
        SetStateActive(GetComponent<Scareness>(), false);
        SetStateActive(GetComponent<Alertness>(), false);
        SetStateActive(GetComponent<Death>(), false);
        m_currState = GetComponent<Movement>();
    }

    private void Update() {
        m_currState.StateUpdate(out m_nextState);
        if (m_death) {
            m_nextState = GetComponent<Death>();
        } else if (m_scare) {
            m_nextState = GetComponent<Scareness>();
            m_scare = false;
        } else if (m_alert) {
            m_nextState = GetComponent<Alertness>();
            m_alert = false;
        }
        if (m_nextState != m_currState) {
            SetStateActive(m_currState, false);
            SetStateActive(m_nextState, true);
            m_currState = m_nextState;
        }
    }

    private void FixedUpdate() {
        m_currState.StateFixedUpdate();
    }

    public void Scare() {
        m_scare = true;
    }

    public void Alert() {
        if ((m_currState as MonoBehaviour) == GetComponent<Movement>()) {
            m_alert = true;
        }
    }

    public void Damage() {
        m_death = true;
    }
}