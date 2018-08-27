using UnityEngine;

public class Alien : MonoBehaviour {
    State m_currState;

    private void Awake() {
        m_currState = GetComponent<Movement>();
    }

    private void Update() {
        m_currState = m_currState.StateUpdate();
    }
}