using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class ShipPiece : MonoBehaviour {
    [SerializeField] Sprite m_outOfGroundImage;
    SpriteRenderer m_sprite;
    Rigidbody2D m_rb;

    private void Awake() {
        m_sprite = GetComponent<SpriteRenderer>();
        m_rb = GetComponent<Rigidbody2D>();
        m_rb.isKinematic = true;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        gameObject.layer = LayerMask.NameToLayer("Environment");
        m_sprite.sprite = m_outOfGroundImage;
        m_rb.isKinematic = false;
        Destroy(this);
    }

}
