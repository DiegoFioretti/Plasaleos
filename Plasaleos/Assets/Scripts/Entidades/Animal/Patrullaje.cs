using UnityEngine;

public class Patrullaje : MonoBehaviour, IState {
    [SerializeField] float m_speed;

    public void StateUpdate(out IState nextState) {
        nextState = this;
    }

    public void StateFixedUpdate() { }

}