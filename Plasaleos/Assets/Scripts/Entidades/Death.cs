using UnityEngine;

public class Death : MonoBehaviour, IState {
    Animator m_animator;
    Entity m_entity;

    private void Awake() {
        m_entity = GetComponent<Entity>();
        m_animator = GetComponent<Animator>();
    }

    private void OnEnable() {
        m_animator.SetBool("Dead", true);
        Invoke("Die", 3f /*animationDuration*/ );
        m_entity.m_rb.isKinematic = true;
    }

    public void StateUpdate(out IState nextState) {
        nextState = this;
    }

    public void StateFixedUpdate() { }

    void Die() {
        if (gameObject.activeSelf) {
            gameObject.SetActive(false);
            LevelManager.instance.AliveAliens--;
        }
    }
}