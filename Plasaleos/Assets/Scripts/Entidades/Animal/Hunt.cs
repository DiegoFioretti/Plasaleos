using UnityEngine;

[RequireComponent(typeof(Animal))]
public class Hunt : MonoBehaviour, IState {
    [SerializeField] float m_chaseSpeed;
    [SerializeField] float m_reachDistance;
    [SerializeField] float m_looseDistance;
    Animal m_animal;
    Alien m_alienPrey;
    Vector2 m_transformRight;
    Vector2 m_groundNormal;
    bool m_grounded;

    private void Awake() {
        m_animal = GetComponent<Animal>();
    }

    public void StateUpdate(out IState nextState) {
        m_animal.CheckForFlip();
        m_animal.CheckForLanding();
        m_animal.TakeGravityEffect();
        if (transform.localScale.z != m_alienPrey.transform.localScale.z) {
            m_animal.Flip();
        }
        RaycastHit2D hit;
        if (!m_alienPrey.isActiveAndEnabled || (Vector3.Distance(
                transform.position, m_alienPrey.transform.position) > m_looseDistance)) {

            nextState = GetComponent<Movement>();
        } else {
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
    }

    public void StateFixedUpdate() {
        if (m_animal.Grounded) {
            m_animal.m_rb.AddForce(m_animal.EntityRight * m_chaseSpeed * 1.5f);
        }
    }

    public void Chase(Transform prey) {
        m_alienPrey = prey.GetComponent<Alien>();
        m_alienPrey.Scare(m_animal.FacingRight);
    }
}