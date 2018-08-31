using UnityEngine;

public class Alien : MonoBehaviour {
    IState m_currState;

    private void Awake() {
        m_currState = GetComponent<Movement>();
    }

    private void Update() {
        m_currState = m_currState.StateUpdate();
    }

    private void FixedUpdate() {
        m_currState.StateFixedUpdate();
    }
}