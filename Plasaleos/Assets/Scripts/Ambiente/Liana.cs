using UnityEngine;

public class Liana : MonoBehaviour {
	[SerializeField] LayerMask m_terrain;
	ResourceManager m_resourceManager;
	Camera m_screenCamera;
	Vector3 m_newPos;
	SpriteRenderer m_sprite;
	BoxCollider2D m_collider;
	Vector2 m_newSize;
	bool m_adjusting = false;
	float m_width;
	Touch touch;

	private void Awake() {
		m_screenCamera = Camera.main;
		m_sprite = GetComponent<SpriteRenderer>();
		m_collider = GetComponent<BoxCollider2D>();
		m_resourceManager = FindObjectOfType<ResourceManager>();
		m_width = m_sprite.size.x;
	}

	private void OnEnable() {
		m_collider.enabled = false;
		m_sprite.enabled = false;
		print(Input.touchCount);
	}

	private void Update() {
		if (Input.touchCount > 0) {
			touch = Input.GetTouch(Input.touchCount - 1);

			if (touch.phase == TouchPhase.Began) {
				OnTouchDown();
			} else if (touch.phase == TouchPhase.Ended) {
				OnTouchUp();
			}
		}
	}

	void OnTouchDown() {
		Vector3 startPos = m_screenCamera.ScreenToWorldPoint(touch.position);
		if (!Physics2D.Raycast(startPos, Vector3.forward, 1000f, m_terrain)) {
			Cancel();
			Time.timeScale = 1f;
		} else {
			startPos.z = 0;
			transform.position = startPos;
			m_sprite.enabled = true;
			m_adjusting = true;
		}
	}

	void OnTouchUp() {
		if (m_adjusting) {
			m_adjusting = false;
			if (m_sprite.color == Color.white) {
				m_collider.size = m_newSize;
				m_collider.offset = new Vector2(m_newSize.x * 0.5f, 0f);
				m_collider.enabled = true;
				enabled = false;
			} else {
				Cancel();
			}
			Time.timeScale = 1f;
		}
	}

	private void LateUpdate() {
		if (m_adjusting) {
			m_newPos = m_screenCamera.ScreenToWorldPoint(touch.position);
			m_newPos.z = 0f;
			if (m_newPos.x - transform.position.x < 0f) {
				m_sprite.flipY = true;
			} else {
				m_sprite.flipY = false;
			}
			Vector3 orientation = transform.eulerAngles;
			orientation.z = Vector2.SignedAngle(
				Vector3.right, m_newPos - transform.position);
			transform.eulerAngles = orientation;
			m_newSize = m_sprite.size;
			m_newSize.x = Vector2.Distance(transform.position, m_newPos);
			if (m_newSize.x > m_width / 2) {
				m_sprite.size = m_newSize;
			}
			ColorPermitted();
		}
	}

	void ColorPermitted() {
		m_sprite.color = Color.red;
		if (Physics2D.Raycast(m_newPos, Vector3.forward, 1000f, m_terrain)) {
			float rate = m_newSize.x * 0.1f;
			Vector3 start = transform.position;
			for (int i = 0; i < 10; i++) {
				Vector3 diff = m_newPos - start;
				if (!Physics2D.Raycast(start, diff, rate, m_terrain)) {
					m_sprite.color = Color.white;
					break;
				}
				start += diff * 0.1f;
			}
		}
	}

	void Cancel() {
		m_sprite.flipY = false;
		m_sprite.size = new Vector2(m_width, m_sprite.size.y);
		transform.eulerAngles = Vector3.zero;
		m_resourceManager.AddLiana();

		Destroy(gameObject);
	}

}