using UnityEngine;

public class Liana : MonoBehaviour {
	[SerializeField] LayerMask m_terrain;
	[SerializeField] LayerMask m_sky;
	[SerializeField] float m_length;
	Camera m_screenCamera;
	Vector3 m_originalPosition;
	Vector3 m_newPos;
	RaycastHit2D m_hit;
	SpriteRenderer m_sprite;
	bool m_setting = false;
	bool m_adjusting = false;
	float m_width;

	private void Awake() {
		m_screenCamera = Camera.main;
		m_newPos = m_screenCamera.ScreenToWorldPoint(Input.mousePosition);
		m_sprite = GetComponent<SpriteRenderer>();
		m_width = m_sprite.size.x;
	}

	private void OnMouseDown() {
		if (!m_setting && !m_adjusting) {
			m_originalPosition = transform.position;
			m_setting = true;
		} else if (m_setting) {
			m_newPos = m_screenCamera.ScreenToWorldPoint(Input.mousePosition);
			if (Physics2D.Raycast(m_newPos, Vector3.forward, 1000f, m_terrain)) {
				m_adjusting = true;
			} else {
				transform.position = m_originalPosition;
				m_sprite.color = Color.white;
			}
			m_setting = false;
		}
	}
	private void LateUpdate() {
		if (m_setting) {
			m_newPos = m_screenCamera.ScreenToWorldPoint(Input.mousePosition);
			m_newPos.z = 0f;
			ColorPermitted(m_newPos);
			transform.position = m_newPos;
		} else if (m_adjusting) {
			m_newPos = m_screenCamera.ScreenToWorldPoint(Input.mousePosition);
			m_newPos.z = 0f;
			ColorPermitted(m_newPos);
			if (m_newPos.x - transform.position.x < 0f) {
				m_sprite.flipY = true;
			} else {
				m_sprite.flipY = false;
			}
			Vector3 orientation = transform.eulerAngles;
			orientation.z = Vector2.SignedAngle(
				Vector3.right, m_newPos - transform.position);
			transform.eulerAngles = orientation;
			Vector2 newSize = m_sprite.size;
			newSize.x = Vector2.Distance(transform.position, m_newPos);
			if (newSize.x > m_width / 2) {
				m_sprite.size = newSize;
			}
			if (Input.GetMouseButtonUp(0)) {
				if (!Physics2D.Raycast(m_newPos, Vector3.forward, 1000f, m_terrain)) {
					transform.position = m_originalPosition;
					m_sprite.flipY = false;
					m_sprite.size = new Vector2(m_width, m_sprite.size.y);
					transform.eulerAngles = Vector3.zero;
				}
				m_sprite.color = Color.white;
				m_adjusting = false;
			}
		}
	}

	void ColorPermitted(Vector3 position) {
		m_hit = Physics2D.Raycast(position, Vector3.forward, 1000f, m_terrain);
		if (m_hit.collider != null) {
			m_sprite.color = Color.green;
		} else {
			m_sprite.color = Color.red;
		}
		if (m_adjusting) {
			if ((m_sprite.size.x > m_length) ||
				!Physics2D.Raycast(position, m_newPos - position, m_sprite.size.x, m_sky)) {

				m_sprite.color = Color.red;
			}
		}
	}
}