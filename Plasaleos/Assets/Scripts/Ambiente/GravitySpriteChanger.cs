using UnityEngine;

public class GravitySpriteChanger : MonoBehaviour {
    [SerializeField] Sprite m_unrestrictedSprite;
    [SerializeField] Sprite m_restrictedSprite;
    SpriteRenderer m_sprite;

    private void Awake() {
        m_sprite = GetComponent<SpriteRenderer>();
    }

    private void Start() {    
        GravityController.Instance.RestrictionChange.AddListener(ChangeRestriction);
    }

    void ChangeRestriction() {
        m_sprite.sprite = (GravityController.Instance.Restricted?
            m_restrictedSprite : m_unrestrictedSprite);
    }

}
