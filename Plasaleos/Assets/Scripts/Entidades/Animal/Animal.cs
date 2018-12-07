using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Scareness))]
[RequireComponent(typeof(Rest))]
[RequireComponent(typeof(Death))]
[RequireComponent(typeof(Hunt))]
[RequireComponent(typeof(Devour))]
public class Animal : Entity {
    [SerializeField] LayerMask m_prayLayer;
    [SerializeField] float m_detectionDistance;
    [SerializeField] bool m_sleeper;
    IState m_currState;
    IState m_nextState;
    Movement m_movementState; 
    Hunt m_huntState;
    bool m_scared;

    protected override void Awake() {
        base.Awake();
        m_scared = false;
        m_movementState = GetComponent<Movement>();
        m_huntState = GetComponent<Hunt>();
        SetStateActive(m_movementState, false);
        SetStateActive(GetComponent<Scareness>(), false);
        SetStateActive(GetComponent<Rest>(), false);
        SetStateActive(GetComponent<Death>(), false);
        SetStateActive(m_huntState, false);
        SetStateActive(GetComponent<Devour>(), false);
        if (m_sleeper) {
            m_currState = GetComponent<Rest>();
        } else {
            m_currState = m_movementState;
        }
        SetStateActive(m_currState, true);
    }

    protected override void Update() {
        base.Update();
        m_currState.StateUpdate(out m_nextState);
        if (IsDead) {
            m_nextState = GetComponent<Death>();
        } else if (m_scared) {
            m_nextState = GetComponent<Scareness>();
            m_scared = false;
        } else if (m_currState == (m_movementState as IState) ||
                    m_currState == (m_huntState as IState) ) {

            RaycastHit2D[] hits;
            hits = Physics2D.RaycastAll(transform.position, EntityRight,
                    m_detectionDistance, m_prayLayer);
            if (hits.Length > 0) {
                Transform closest = hits[0].transform;
                float closestDistance = Vector2.Distance(closest.position,
                                                         transform.position);
                foreach (RaycastHit2D hit in hits) {
                    Alien alien = hit.transform.GetComponent<Alien>();
                    if (alien) {
                        alien.Scare(FacingRight);
                        if (m_currState == (m_movementState as IState)) {
                            float distance = Vector2.Distance(alien.transform.position,
                                                            transform.position);
                            if (distance < closestDistance) {
                                closestDistance = distance;
                                closest = alien.transform;
                            }
                        }
                    }
                }
                if (m_currState == (m_movementState as IState)) {
                    m_huntState.Chase(closest.transform);
                    m_nextState = m_huntState;
                }
            }
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

    public bool SearchPrey(out RaycastHit2D hit) {
        hit = Physics2D.Raycast(transform.position, transform.right * transform.localScale.x,
            m_detectionDistance, m_prayLayer);
        return (hit != false);
    }

    private void FixedUpdate() {
        m_currState.StateFixedUpdate();
    }

    [ContextMenu("Scare")]
    public void Scared() {
        m_scared = true;
    }

}