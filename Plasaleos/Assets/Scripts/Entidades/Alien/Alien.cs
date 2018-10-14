using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Scareness))]
[RequireComponent(typeof(Alertness))]
[RequireComponent(typeof(Death))]
public sealed class Alien : Entity {
    IState m_currState;
    IState m_nextState;
    bool m_scared;
    bool m_alert;
    bool m_death;

    protected override void Awake() {
        base.Awake();
        m_scared = false;
        m_alert = false;
        m_death = false;
        SetStateActive(GetComponent<Movement>(), true);
        SetStateActive(GetComponent<Scareness>(), false);
        SetStateActive(GetComponent<Alertness>(), false);
        SetStateActive(GetComponent<Death>(), false);
        m_currState = GetComponent<Movement>();
    }

    protected override void Update() {
        base.Update();
        m_currState.StateUpdate(out m_nextState);
        if (m_death) {
            m_nextState = GetComponent<Death>();
        } else if (m_scared) {
            m_nextState = GetComponent<Scareness>();
            m_scared = false;
        } else if (m_alert) {
            m_nextState = GetComponent<Alertness>();
            m_alert = false;
        }
        if (m_nextState != m_currState) {
            SetStateActive(m_currState, false);
            SetStateActive(m_nextState, true);
            m_currState = m_nextState;
        }
    }

    protected override void LateUpdate() {
        base.LateUpdate();
    }

    private void FixedUpdate() {
        m_currState.StateFixedUpdate();
    }

    [ContextMenu("Scare")]
    public void Scare() {
        m_scared = true;
    }

    public void Scare(bool enemyFacingRight) {
        if (enemyFacingRight != FacingRight) {
            m_scared = true;
        }
    }

    [ContextMenu("Alert")]
    public void Alert() {
        if ((m_currState as MonoBehaviour) == GetComponent<Movement>()) {
            m_alert = true;
        } else if ((m_currState as MonoBehaviour) == GetComponent<Alertness>()) {
            GetComponent<Alertness>().ToggleAlert();
        }
    }

    public void Damage() {
        m_death = true;
    }
}