using UnityEngine;

public class GravitySwitch : MonoBehaviour {
    [SerializeField] Sprite m_spriteUnrestricted;
    [SerializeField] Sprite m_spriteRestricted;
    [SerializeField] float m_disableDuration;
    SpriteRenderer m_sprite;
    float m_counter;
    bool m_restricting;

    private void Awake() {
        m_restricting = false;
        m_counter = 0f;
        m_sprite = GetComponent<SpriteRenderer>();
        m_sprite.sprite = m_spriteUnrestricted;
    }

    private void Start() {    
        GravityController.Instance.ToRestricted.AddListener(ChangeToRestric);
        GravityController.Instance.ToUnrestricted.AddListener(ChangeToUnrestric);
    }

    private void Update() {
        if (m_counter > 0f){
            m_counter -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D() {
        if (m_counter <= 0f) {
            if (!m_restricting) {
                GravityController.Instance.Restrict(-transform.up);
                m_sprite.sprite = m_spriteRestricted;
                m_restricting = true;
            } else {
                GravityController.Instance.Unrestric();
                m_sprite.sprite = m_spriteUnrestricted;
                m_restricting = false;
            }
            m_counter = m_disableDuration;
        }
    }

    void ChangeToRestric() {
        m_sprite.sprite = m_spriteRestricted;
        m_counter = m_disableDuration;
    }

    void ChangeToUnrestric() {
        m_sprite.sprite = m_spriteUnrestricted;
        m_counter = m_disableDuration;
    }
}
