using UnityEngine;
using UnityEngine.EventSystems;

public class Mushroom : MonoBehaviour {
    [SerializeField] LayerMask m_groundLayer;
    [SerializeField] float m_jumpForce;
    [SerializeField] float m_horizontalSpeed;
    ResourceManager m_resourceManager;
    Animator m_animator;
    Camera m_screenCamera;
    SpriteRenderer m_sprite;
    Touch m_touch;
    float m_newY;
    float m_distToFeet;
    bool m_adjusting;

    private void Awake() {
        m_resourceManager = FindObjectOfType<ResourceManager>();
        m_animator = GetComponentInChildren<Animator>();
        m_sprite = GetComponentInChildren<SpriteRenderer>();
        m_screenCamera = Camera.main;
        m_distToFeet = m_sprite.size.y * 0.5f;
    }

    private void OnEnable() {
        m_adjusting = true;
        m_sprite.enabled = false;
    }

    private void Update() {
        if (m_adjusting) {
            if (Input.touchCount > 0) {
                m_touch = Input.GetTouch(Input.touchCount - 1);

                if (m_touch.phase == TouchPhase.Began) {
                    if (!EventSystem.current.IsPointerOverGameObject(m_touch.fingerId)) {
                        OnTouchDown();
                    } else {
                        Time.timeScale = 1f;
                        Destroy(gameObject);
                    }
                } else if (m_touch.phase == TouchPhase.Moved) {
                    WhileOnTouch();
                } else if (m_touch.phase == TouchPhase.Ended) {
                    OnTouchUp();
                }
            }
        }
    }

    void OnTouchDown() {
        m_sprite.enabled = true;
        Vector3 newPos = m_screenCamera.ScreenToWorldPoint(m_touch.position);
        newPos.y += m_distToFeet * 3f;
        newPos.z = 0f;
        transform.position = newPos;
        WhileOnTouch();
    }

    void WhileOnTouch() {
        Vector3 newPos = m_screenCamera.ScreenToWorldPoint(m_touch.position);
        newPos += transform.up * m_distToFeet * 3f;
        newPos.z = 0f;
        transform.position = newPos;
        RaycastHit2D hit = Physics2D.Raycast(transform.position + transform.up * m_distToFeet, -transform.up, m_distToFeet * 4f, m_groundLayer);
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position + transform.up * m_distToFeet,
            transform.up, m_distToFeet * 2f, m_groundLayer); //to avoid putting them under de ground
        if (hit.collider != null && hitUp.collider == null) {
            m_sprite.color = Color.white;
            m_newY = hit.point.y + m_distToFeet + 0.1f;
        } else {
            m_sprite.color = Color.red;
            m_newY = transform.position.y;
        }
    }

    void OnTouchUp() {
        if (m_adjusting) {
            Vector3 pos = transform.position;
            pos.y = m_newY;
            transform.position = pos;
            m_adjusting = false;
            Time.timeScale = 1f;
            FindObjectOfType<FinishEdit>().Finish();
            if (m_sprite.color == Color.red) {
                m_resourceManager.Mushrooms.Add();
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb.velocity.y < -1f) {
            Entity m_entity = other.GetComponent<Entity>();
            float direction = 1f;
            if (m_entity) {
                m_entity.Jump();
                if (!m_entity.FacingRight) {
                    direction = -1f;
                }
            } else {
                if (rb.velocity.x < 0f) {
                    direction = -1f;
                }
            }
            rb.angularVelocity = 0f;
            rb.velocity = new Vector2(m_horizontalSpeed * direction, m_jumpForce);
            m_animator.SetTrigger("Jumped");
        }
    }
}