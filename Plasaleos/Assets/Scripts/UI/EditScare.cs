using UnityEngine;
using UnityEngine.UI;

public class EditScare : MonoBehaviour, IResourceEdition {
    [SerializeField] Image m_scareCircle;
    [SerializeField] Image m_adjustmentController;
    [SerializeField] Image m_line;
    [SerializeField] LayerMask m_affectedLayer;
    Camera m_camera;
    float m_initDistance;

    private void Awake() {
        m_camera = Camera.main;
        m_initDistance = Vector3.Distance(m_adjustmentController.transform.position,
            transform.position);
        print("Init Dist:" + m_initDistance);
		FinishEdit[] fes = FindObjectsOfType<FinishEdit>();
		foreach (FinishEdit fe in fes) {
			fe.SetEditingObject((this));
		}
    }

    public void SetPosition() {
        Touch touch = Input.GetTouch(Input.touchCount - 1);
        transform.position = touch.position;
    }

    public void SetSize() {
        Touch touch = Input.GetTouch(Input.touchCount - 1);
        m_adjustmentController.transform.position = new Vector2(touch.position.x,
            m_adjustmentController.transform.position.y);
        float distance = Vector3.Distance(m_adjustmentController.transform.position,
            transform.position);
        print("Dist:" + distance);
        m_scareCircle.rectTransform.sizeDelta = Vector2.one * distance;
        m_line.rectTransform.sizeDelta = new Vector2(
            distance, m_line.rectTransform.sizeDelta.y);
        m_line.rectTransform.anchoredPosition = new Vector2(
            m_line.rectTransform.sizeDelta.x * 0.5f, 0f);
    }

    public void Cancel() {
        ResourceManager.Instance.Scares.Add();
    }

    public void Confirm() {
        //wc: WorlCoordinates
        Vector2 wcPos = m_camera.ScreenToWorldPoint(transform.position);
        Vector2 circleBorder = transform.position +
            Vector3.right * m_scareCircle.rectTransform.sizeDelta.x * 0.5f;
        Vector2 wcBorder = m_camera.ScreenToWorldPoint(circleBorder);
        float wcDistante = Vector2.Distance(wcPos, wcBorder);
        Debug.DrawRay(transform.position,
            Vector3.right * wcDistante, Color.magenta, 5f);
        Collider2D[] victims = Physics2D.OverlapCircleAll(wcPos, wcDistante, m_affectedLayer);
        foreach (Collider2D victim in victims) {
            victim.GetComponent<Entity>().Scare(wcPos);
        }
		Destroy(gameObject);
    }
}