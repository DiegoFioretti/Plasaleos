using UnityEngine;

public class Animal : Entity {
    [SerializeField] bool m_sleeper;
    IState m_currState;
    IState m_nextState;

    public bool Sleeper { get { return m_sleeper; } }

    protected override void Awake() {
        if (m_sleeper) {
            m_currState = GetComponent<Reposo>();
        } else {
            m_currState = GetComponent<Patrullaje>();
        }
        SetStateActive(m_currState, true);
    }

    private void Update() {
        m_currState.StateUpdate(out m_nextState);
        if (m_nextState != m_currState) {
            SetStateActive(m_currState, false);
            SetStateActive(m_nextState, true);
        }
    }

    private void FixedUpdate() {
        m_currState.StateFixedUpdate();
    }
}