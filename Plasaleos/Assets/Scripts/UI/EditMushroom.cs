using UnityEngine;
using UnityEngine.UI;

public class EditMushroom : MonoBehaviour, IResourceEdition {
    [SerializeField] GameObject m_resourcePrefab;
    [SerializeField] LayerMask m_groundLayer;
    [SerializeField] Resource m_resource;
    Image m_sprite;
    Camera m_screenCamera;
    Touch m_touch;
    Vector3 posToWorld;
    float m_distToSide;
    float m_distToFeet;
    bool m_adjusting;
    float m_newY;
    float m_upOffset;

    private void Awake() {
        m_sprite = GetComponent<Image>();
        m_screenCamera = Camera.main;
        m_distToSide = m_sprite.sprite.bounds.size.x * 0.5f;
        m_distToFeet = m_sprite.sprite.bounds.size.y * 0.5f;
        m_upOffset = 1.5f;
        m_sprite.color = Color.red;
        transform.position = Vector2.zero;
        FinishEdit[] fes = FindObjectsOfType<FinishEdit>();
        foreach (FinishEdit fe in fes) {
            fe.SetEditingObject((this));
        }
    }

    private void Update() {
        if (m_adjusting) {
            if (Input.touchCount > 0) {
                m_touch = Input.GetTouch(Input.touchCount - 1);
                if (m_touch.phase == TouchPhase.Moved || m_touch.phase == TouchPhase.Began) {
                    WhileOnTouch();
                } else if (m_touch.phase == TouchPhase.Ended) {
                    OnTouchUp();
                }
            }
        }
    }

    public void StartToEdit() {
        m_adjusting = true;
    }

    void WhileOnTouch() {
        posToWorld = m_screenCamera.ScreenToWorldPoint(m_touch.position);
        posToWorld += transform.up * (m_distToFeet + m_upOffset);
        posToWorld.z = 0f;
        transform.position = m_screenCamera.WorldToScreenPoint(posToWorld);
        RaycastHit2D hitDown = Physics2D.Raycast(posToWorld, -transform.up,
            m_distToFeet * 3f, m_groundLayer);
        RaycastHit2D hitUp = Physics2D.Raycast(posToWorld,
            transform.up, m_distToFeet * 2f, m_groundLayer); //to avoid putting them under de ground
        if (hitDown.collider != null && hitUp.collider == null) {
            Vector2 up2D = transform.up;
            Vector2 overFloorSideChecker = hitDown.point + up2D * 0.1f;
            RaycastHit2D hitLeft = Physics2D.Raycast(overFloorSideChecker, -transform.right,
                m_distToSide, m_groundLayer);
            RaycastHit2D hitRight = Physics2D.Raycast(overFloorSideChecker, transform.right,
                m_distToSide, m_groundLayer);
            if (hitLeft.collider == null && hitRight.collider == null) {

                m_sprite.color = Color.white;
                m_newY = hitDown.point.y + m_distToFeet - 0.2f;
            } else {
                m_sprite.color = Color.red;
                m_newY = transform.position.y;
            }
        } else {
            m_sprite.color = Color.red;
            m_newY = transform.position.y;
        }
    }

    void OnTouchUp() {
        m_adjusting = false;
    }

    public void Cancel() {
        m_resource.Add();
        Destroy(gameObject);
    }

    public void Confirm() {
        if (m_sprite.color == Color.red) {
            Cancel();
        } else {
            posToWorld.y = m_newY;
            Instantiate(m_resourcePrefab, posToWorld, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}