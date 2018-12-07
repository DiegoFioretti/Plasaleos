using UnityEngine;

public class Alertness : MonoBehaviour, IState {
    [SerializeField] float m_duration;
    [SerializeField] GameObject alertSpawn;
    [SerializeField] float alertHigh = 3;
    Entity m_entity;
    bool m_alerted = false;

    private void Awake() {
        m_entity = GetComponent<Entity>();
    }

    private void OnEnable() {
        m_alerted = true;
        if (m_entity.Grounded) {
            m_entity.m_rb.velocity = Vector2.zero;
            GetComponent<Animator>().SetBool("Moving", false);
        }
        Invoke("ToggleAlert", m_duration);
        Instantiate(alertSpawn, transform.up * alertHigh + transform.position, transform.rotation);
    }

    public void StateUpdate(out IState nextState) {
        m_entity.CheckForLanding();
        m_entity.TakeGravityEffect();
        if (m_alerted) {
            nextState = this;
        } else {
            CancelInvoke();
            nextState = GetComponent<Movement>();
        }
    }

    public void StateFixedUpdate() { }

    public bool ToggleAlert() {
        m_alerted = !m_alerted;
        return m_alerted;
    }

    public float GetDuration() {
        return m_duration;
    }
}