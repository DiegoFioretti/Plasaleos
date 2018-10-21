using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Entity : MonoBehaviour {
    [SerializeField] LayerMask m_groundLayer;
    [SerializeField] bool m_facingRight = true;
    [SerializeField] float m_speed;
    [SerializeField] float m_detachAngle;
    [HideInInspector]
    public Rigidbody2D m_rb;
    Vector2 m_entityRight;
    Vector2 m_prevGravity;
    Vector2 m_groundNormal;
    float m_footOffset;
    float m_speedMultiplier;
    bool m_grounded;
    bool m_jumping;
    bool m_death;

    public LayerMask GroundLayer { get { return m_groundLayer; } }
    public Vector2 EntityRight { get { return m_entityRight; } }
    public float Speed { get { return m_speed; } }
    public bool FacingRight { get { return m_facingRight; } }
    public bool Grounded { get { return m_grounded; } }
    public bool Jumping { get { return m_jumping; } }
    public bool IsDead { get { return m_death; } }

    protected virtual void Awake() {
        m_death = false;
        m_footOffset = GetComponent<SpriteRenderer>().size.y * 0.5f;
        m_rb = GetComponent<Rigidbody2D>();
        m_speedMultiplier = 1f;
        m_prevGravity = Physics.gravity;
    }

    protected void SetStateActive(IState state, bool active) {
        (state as MonoBehaviour).enabled = active;
    }

    protected virtual void Update() {
        m_grounded = IsGrounded(out m_groundNormal);
    }

    protected virtual void LateUpdate() {
        m_prevGravity = Physics2D.gravity;
    }

    private void OnValidate() {
        Vector3 scale = transform.localScale;
        if (!m_facingRight) {
            scale.x = -1;
        } else {
            scale.x = 1;
        }
        transform.localScale = scale;
    }

    public void TakeGravityEffect() {
        float angle = Vector2.Angle(m_entityRight, Physics2D.gravity);
        if (!m_grounded) {
            m_speedMultiplier = 2.5f;
        } else if (m_grounded && angle < 90f) {
            m_speedMultiplier = 1.5f;
        } else if (m_grounded && angle > 90f) {
            m_speedMultiplier = 0.5f;
        } else {
            m_speedMultiplier = 1f;
        }

        if ((!m_grounded || (m_grounded && angle == 90f)) &&
            (m_prevGravity != Physics2D.gravity)) {

            float newSpeed = m_rb.velocity.magnitude * 0.7f;
            m_rb.velocity = Physics2D.gravity.normalized * newSpeed;
        }

        if (m_rb.velocity.magnitude > m_speed * m_speedMultiplier) {
            m_rb.velocity = m_rb.velocity.normalized *
                m_speed * m_speedMultiplier * 0.707f;
            //multiply by sqr(2) so that velocity.magnitude ~= m_speed
        }
    }

    public void CheckForLanding() {
        if (m_grounded) {
            if (Vector2.Angle(-transform.up, Physics2D.gravity) > m_detachAngle) {
                m_grounded = false;
            }
        }
        if (m_grounded) {
            transform.eulerAngles = new Vector3(
                0f, 0f, -Vector2.SignedAngle(m_groundNormal, Vector2.up)
            );
            m_entityRight = new Vector2(
                m_groundNormal.y, -m_groundNormal.x) * (m_facingRight? 1f: -1f);
        } else {
            transform.eulerAngles = new Vector3(
                0f, 0f, -Vector2.SignedAngle(Physics2D.gravity, -Vector2.up)
            );
            m_entityRight = new Vector2(
                transform.right.x, transform.right.y) * (m_facingRight? 1f: -1f);
        }
    }

    public void CheckForFlip() {
        RaycastHit2D frontHit = Physics2D.Raycast(transform.position - 0.5f * transform.up,
            m_entityRight, 1.3f, m_groundLayer);
        if (frontHit && m_grounded && !((Vector3.Angle(frontHit.normal, Vector3.down) != 90f) &&
                (Vector3.Angle(Physics2D.gravity, m_entityRight) < 90f))) {

            if (Physics2D.Raycast(transform.position, -m_entityRight, //check if it's stuck in a corner
                    1.5f, m_groundLayer)) {

                transform.up = frontHit.normal;
            }
            Flip();
        }
    }

    public void Flip() {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
        m_facingRight = !m_facingRight;
    }

    bool IsGrounded() {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, (-transform.up),
            m_footOffset + 0.2f, m_groundLayer);
        return (hit);
    }

    bool IsGrounded(out Vector2 normal) {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, (-transform.up),
            m_footOffset + 0.2f, m_groundLayer);
        normal = hit.normal;
        return (hit);
    }

    public void Damage() {
        m_death = true;
    }

    public void Jump() {
        Invoke("EndJump", 0.5f);
        m_jumping = true;
    }

    void EndJump() {
        m_jumping = false;
    }

}