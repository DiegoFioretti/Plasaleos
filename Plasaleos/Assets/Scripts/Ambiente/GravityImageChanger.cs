using UnityEngine;
using UnityEngine.UI;

public class GravityImageChanger : MonoBehaviour {
    [SerializeField] Sprite m_unrestrictedSprite;
    [SerializeField] Sprite m_restrictedSprite;
    Image m_image;

    private void Awake() {
        m_image = GetComponent<Image>();
    }

    private void Start() {    
        GravityController.Instance.RestrictionChange.AddListener(ChangeRestriction);
    }

    void ChangeRestriction() {
        m_image.sprite = (GravityController.Instance.Restricted?
            m_restrictedSprite : m_unrestrictedSprite);
    }


}
