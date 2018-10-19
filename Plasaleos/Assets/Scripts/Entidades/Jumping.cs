using UnityEngine;

public class Jumping : MonoBehaviour, IState {
    Entity m_entity;

    private void Awake() {
        m_entity = GetComponent<Entity>();
    }

    public void StateUpdate(out IState nextState) {
        if (m_entity.Jumping || !m_entity.Grounded) {
            print("yesh");
            nextState = this;
        } else {
            print("nop");
            nextState = GetComponent<Movement>();
        }
    }

    public void StateFixedUpdate() { }

}