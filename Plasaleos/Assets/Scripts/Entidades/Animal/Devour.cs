using UnityEngine;

[RequireComponent(typeof(Animal))]
public class Devour : MonoBehaviour, IState {
    [SerializeField] float m_duration;
    Animal m_animal;
    float m_timer;

    private void Awake() {
        m_animal = GetComponent<Animal>();
    }

    private void OnEnable() {
        m_timer = m_duration;
        print("helo");
        GetComponent<Animator>().SetBool("Eat", true);
    }

    public void StateUpdate(out IState nextState) {
        m_timer -= Time.deltaTime;
        if (m_timer < 0f) {
            RaycastHit2D hit;
            GetComponent<Animator>().SetBool("Eat", false);
            if (m_animal.SearchPrey(out hit)) {
                Hunt hunt = GetComponent<Hunt>();
                hunt.Chase(hit.transform);
                nextState = hunt;
            } else {
                nextState = GetComponent<Movement>();
            }
        } else {
            nextState = this;
        }
    }

    public void StateFixedUpdate() { }
}