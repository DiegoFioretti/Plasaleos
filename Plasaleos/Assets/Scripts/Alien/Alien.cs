using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Alien : MonoBehaviour {
    IState m_currState;
    IState m_nextState;

    private void Awake() {
        m_currState = GetComponent<Movement>();
    }

    private void Update() {
        m_currState.StateUpdate(ref m_nextState);
        m_currState = m_nextState;
    }

    private void FixedUpdate() {
        m_currState.StateFixedUpdate();
    }
}