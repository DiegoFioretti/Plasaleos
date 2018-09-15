using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Entity : MonoBehaviour {
    [SerializeField] bool m_facingRight = true;
    [SerializeField] LayerMask m_groundLayer;
    public Rigidbody2D m_rb;
    float m_footOffset;
    public LayerMask GroundLayer { get { return m_groundLayer; } }
    public bool FacingRight { get { return m_facingRight; } }

    protected virtual void Awake() {
        m_footOffset = GetComponent<SpriteRenderer>().size.y * 0.5f;
        m_rb = GetComponent<Rigidbody2D>();
    }

    protected void SetStateActive(IState state, bool active) {
        (state as MonoBehaviour).enabled = active;
    }

    public void Flip() {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
        m_facingRight = !m_facingRight;
    }

    public bool IsGrounded() {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, (-transform.up),
            m_footOffset + 0.2f, m_groundLayer);
        return (hit);
    }

    public bool IsGrounded(out Vector2 normal) {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, (-transform.up),
            m_footOffset + 0.2f, m_groundLayer);
        normal = hit.normal;
        return (hit);
    }
}