using UnityEngine;

[RequireComponent(typeof(Animal))]
public class Hunt : MonoBehaviour, IState {
    [SerializeField] float m_chaseSpeed;
    [SerializeField] float m_reachDistance;
    Animal m_animal;
    Movement m_movement;
    Alien m_alienPrey;
    Vector2 m_transformRight;
    Vector2 m_groundNormal;
    bool m_grounded;

    private void Awake() {
        m_animal = GetComponent<Animal>();
        m_movement = GetComponent<Movement>();
    }

    public void StateUpdate(out IState nextState) {
        m_movement.StateUpdate(out nextState);
        if (transform.localScale.z != m_alienPrey.transform.localScale.z) {
            m_animal.Flip();
        }
        RaycastHit2D hit;
        if (m_animal.SearchPrey(out hit)) {
            if (hit.distance < m_reachDistance) {
                m_alienPrey.Damage();
                nextState = GetComponent<Devour>();
            } else {
                m_alienPrey = hit.transform.GetComponent<Alien>();
                m_alienPrey.Scare(m_animal.FacingRight);
                nextState = this;
            }
        } else {
            nextState = this;
        }
    }

    public void StateFixedUpdate() {
        if (m_animal.IsGrounded()) {
            m_animal.m_rb.AddForce(m_movement.GetTransformRight() * m_chaseSpeed * 1.5f);
        }
    }

    public void Chase(Transform prey) {
        m_alienPrey = prey.GetComponent<Alien>();
        m_alienPrey.Scare(m_animal.FacingRight);
    }
}