using UnityEngine;

public class Jumping : MonoBehaviour, IState {
    Entity m_entity;

    private void Awake() {
        m_entity = GetComponent<Entity>();
    }

    public void StateUpdate(out IState nextState) {
        if (m_entity.Jumping) {
            nextState = this;
        } else {
            nextState = GetComponent<Movement>();
        }
    }

    public void StateFixedUpdate() { }

}