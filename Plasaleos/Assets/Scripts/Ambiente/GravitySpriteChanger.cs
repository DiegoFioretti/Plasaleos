using UnityEngine;

public class GravitySpriteChanger : MonoBehaviour {
    [SerializeField] Sprite m_unrestrictedSprite;
    [SerializeField] Sprite m_restrictedSprite;
    SpriteRenderer m_sprite;

    private void Awake() {
        m_sprite = GetComponent<SpriteRenderer>();
    }

    private void Start() {    
        GravityController.Instance.ToRestricted.AddListener(ChangeToRestric);
        GravityController.Instance.ToUnrestricted.AddListener(ChangeToUnrestric);
    }

    void ChangeToRestric() {
        m_sprite.sprite = m_restrictedSprite;
    }

    void ChangeToUnrestric() {
       m_sprite.sprite = m_unrestrictedSprite;
    }
}
