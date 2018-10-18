﻿using UnityEngine;

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
    bool m_scared;

    protected override void Awake() {
        base.Awake();
        m_scared = false;
        m_movementState = GetComponent<Movement>();
        SetStateActive(GetComponent<Movement>(), false);
        SetStateActive(GetComponent<Scareness>(), false);
        SetStateActive(GetComponent<Rest>(), false);
        SetStateActive(GetComponent<Death>(), false);
        SetStateActive(GetComponent<Hunt>(), false);
        SetStateActive(GetComponent<Devour>(), false);
        if (m_sleeper) {
            m_currState = GetComponent<Rest>();
        } else {
            m_currState = GetComponent<Movement>();
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
        } else if (m_currState == (m_movementState as IState)) {
            RaycastHit2D hit;
            if (hit = Physics2D.Raycast(transform.position, transform.right * transform.localScale.x,
                    m_detectionDistance, m_prayLayer)) {

                Hunt hunt = GetComponent<Hunt>();
                hunt.Chase(hit.transform);
                m_nextState = hunt;
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