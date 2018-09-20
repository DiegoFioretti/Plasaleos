using UnityEngine;

public class Reposo : MonoBehaviour, IState {
    bool m_awaken;

    private void OnEnable() {
        m_awaken = false;
    }

    public void StateUpdate(out IState nextState) {
        // if death
        // if scare
        if (m_awaken) {
            nextState = GetComponent<Patrullaje>();
        } else {
            nextState = this;
        }
    }

    public void StateFixedUpdate() { }

    private void OnCollisionEnter2D(Collision2D other) {
        m_awaken = true;
    }

    public void Alerted() {
        m_awaken = true;
    }
}