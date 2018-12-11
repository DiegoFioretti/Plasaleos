using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Scareness))]
[RequireComponent(typeof(Alertness))]
[RequireComponent(typeof(Death))]
[RequireComponent(typeof(Jumping))]
public sealed class Alien : Entity {
    IState m_currState;
    IState m_nextState;
    bool m_alerted;

    protected override void Awake() {
        base.Awake();
        m_alerted = false;
        SetStateActive(GetComponent<Movement>(), true);
        SetStateActive(GetComponent<Scareness>(), false);
        SetStateActive(GetComponent<Alertness>(), false);
        SetStateActive(GetComponent<Death>(), false);
        SetStateActive(GetComponent<Jumping>(), false);
        m_currState = GetComponent<Movement>();
    }

    protected override void Update() {
        base.Update();
        if (Jumping && m_currState == (GetComponent<Movement>()as IState)) {
            m_currState = GetComponent<Jumping>();
        }
        m_currState.StateUpdate(out m_nextState);
        if (IsDead) {
            m_nextState = GetComponent<Death>();
        } else if (Scared) {
            m_nextState = GetComponent<Scareness>();
            Scared = false;
        } else if (m_alerted) {
            m_nextState = GetComponent<Alertness>();
            m_alerted = false;
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

    public void Scare(bool enemyFacingRight) {
        if (enemyFacingRight != FacingRight) {
            Scared = true;
        }
    }

    [ContextMenu("Alert")]
    public void Alert() {
        if ((m_currState as MonoBehaviour) == GetComponent<Movement>()) {
            m_alerted = true;
        } else if ((m_currState as MonoBehaviour) == GetComponent<Alertness>()) {
            GetComponent<Alertness>().ToggleAlert();
        }
    }

    public float GetAlertDuration() {
        return GetComponent<Alertness>().GetDuration();
    }
}