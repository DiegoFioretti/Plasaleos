using UnityEngine;
using UnityEngine.EventSystems;

public class Liana : MonoBehaviour {
	[SerializeField] LayerMask m_terrain;
	[SerializeField] Color m_invalidColor;
	ResourceManager m_resourceManager;
	Camera m_screenCamera;
	Vector3 m_newPos;
	SpriteRenderer m_sprite;
	BoxCollider2D m_collider;
	Vector2 m_newSize;
	bool m_adjusting;
	float m_width;
	Touch m_touch;

	private void Awake() {
		m_screenCamera = Camera.main;
		m_sprite = GetComponent<SpriteRenderer>();
		m_collider = GetComponent<BoxCollider2D>();
		m_resourceManager = FindObjectOfType<ResourceManager>();
		m_width = m_sprite.size.x;
	}

	private void OnEnable() {
		m_adjusting = false;
		m_collider.enabled = false;
		m_sprite.enabled = false;
	}

	private void Update() {
		if (Input.touchCount > 0) {
			m_touch = Input.GetTouch(Input.touchCount - 1);

			if (m_touch.phase == TouchPhase.Began) {
				// if (!EventSystem.current.IsPointerOverGameObject(m_touch.fingerId)) {
				OnTouchDown();
				// } else {
				// 	Time.timeScale = 1f;
				// 	Destroy(gameObject);
				// }
			} else if (m_touch.phase == TouchPhase.Ended) {
				OnTouchUp();
			}
		}
	}

	void OnTouchDown() {
		Vector3 startPos = m_screenCamera.ScreenToWorldPoint(m_touch.position);
		if (!Physics2D.Raycast(startPos, Vector3.forward, 1000f, m_terrain)) {
			Time.timeScale = 1f;
			FindObjectOfType<FinishEdit>().Finish();
			m_resourceManager.Lianas.Add();
			Destroy(gameObject);
		} else {
			startPos.z = 0;
			transform.position = startPos;
			m_sprite.enabled = true;
			m_adjusting = true;
			ColorPermitted();
		}
	}

	void OnTouchUp() {
		CancelInvoke();
		if (m_adjusting) {
			m_adjusting = false;
			FindObjectOfType<FinishEdit>().Finish();
			Time.timeScale = 1f;
			if (m_sprite.color == Color.white) {
				m_collider.size = m_newSize;
				m_collider.offset = new Vector2(m_newSize.x * 0.5f, 0f);
				m_collider.enabled = true;
				enabled = false;
			} else {
				m_resourceManager.Lianas.Add();
				Destroy(gameObject);
			}
		}
	}

	private void LateUpdate() {
		if (m_adjusting) {
			m_newPos = m_screenCamera.ScreenToWorldPoint(m_touch.position);
			m_newPos.z = 0f;
			if (m_newPos.x - transform.position.x < 0f) {
				m_sprite.flipY = true;
			} else {
				m_sprite.flipY = false;
			}
			m_newSize = m_sprite.size;
			m_newSize.x = Vector2.Distance(transform.position, m_newPos);
			Vector3 orientation = transform.eulerAngles;
			orientation.z = Vector2.SignedAngle(
				Vector3.right, m_newPos - transform.position);
			transform.eulerAngles = orientation;
			if (m_newSize.x > m_width / 2) {
				m_sprite.size = m_newSize;
			}
			ColorPermitted();
		}
	}

	void ColorPermitted() {
		m_sprite.color = m_invalidColor;
		bool once = false;
		bool twice = false;
		if (Physics2D.Raycast(m_newPos, Vector3.forward, 1000f, m_terrain)) {
			Vector3 start = transform.position;
			int i;
			int count = 50;
			float rate = m_newSize.x / count;
			for (i = 0; i < count; i++) {
				Vector3 diff = m_newPos - start;
				if (!Physics2D.Raycast(start, diff, rate, m_terrain)) {
					if (!once) {
						once = true;
					} else if (twice) {
						m_sprite.color = m_invalidColor;
						break;
					}
				} else {
					if (once) {
						twice = true;
					}
				}
				start += diff * 0.1f;
			}
			if (i == count && once) {
				m_sprite.color = Color.white;
			}
		}
		Invoke("ColorPermitted", 0.33f);
	}

	void Cancel() {
		m_resourceManager.Lianas.Add();
		Destroy(gameObject);
	}

}