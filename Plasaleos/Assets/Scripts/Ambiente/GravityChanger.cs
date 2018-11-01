using UnityEngine;

public class GravityChanger : MonoBehaviour {
    [SerializeField] bool m_restrict;
    GravityController m_gravityController;

    private void Awake() {
        m_gravityController = FindObjectOfType<GravityController>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        m_gravityController.Restricted = m_restrict;
    }
}