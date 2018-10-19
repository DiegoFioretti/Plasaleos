using UnityEngine;

public class Rest : MonoBehaviour, IState {
    [SerializeField] float m_speedToAwake;
    Entity m_entity;
    bool m_awaken;

    private void Awake() {
        m_entity = GetComponent<Entity>();
    }

    private void OnEnable() {
        m_awaken = false;
        GetComponent<Animator>().SetBool("Sleep", true);
    }

    private void OnDisable()
    {
        GetComponent<Animator>().SetBool("Sleep", false);
    }

    public void StateUpdate(out IState nextState) {
        if (m_entity.m_rb.velocity.magnitude > m_speedToAwake) {
            m_awaken = true;
        }
        if (m_awaken) {
            nextState = GetComponent<Movement>();
        } else {
            nextState = this;
        }
    }

    public void StateFixedUpdate() { }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.GetComponent<Alien>()) {
            m_awaken = true;
        }
    }
}