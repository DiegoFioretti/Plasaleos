using UnityEngine;
using UnityEngine.UI;

public class Liana : MonoBehaviour, IResourceEdition {
	[SerializeField] GameObject m_resourcePrefab;
	[SerializeField] LayerMask m_terrain;
	[SerializeField] Color m_invalidColor;
	Camera m_screenCamera;
	Vector3 m_worldPos;
	Vector3 m_worldEndPoint;
	Vector3 m_endPoint;
	Image m_sprite;
	Vector2 m_newSize;
	Vector2 m_halfScreen;
	Touch m_touch;
	RectTransform m_rect;
	CanvasScaler m_scaler;
	Vector2 m_toReferenceResolution;

	private void OnEnable() {
		m_scaler = GetComponentInParent<CanvasScaler>();
		m_screenCamera = Camera.main;
		m_sprite = GetComponent<Image>();
		m_rect = GetComponent<RectTransform>();
		m_halfScreen = new Vector2 (Screen.width, Screen.height) / 2f;
		m_toReferenceResolution = m_scaler.referenceResolution;
		m_toReferenceResolution.x /= Screen.width;
		m_toReferenceResolution.y /= Screen.height;
		transform.position = m_halfScreen + new Vector2(-m_rect.sizeDelta.x * 0.5f, 0f);
		m_endPoint = transform.position;
		m_endPoint.x += m_rect.sizeDelta.x;
		m_sprite.color = m_invalidColor;
		m_worldPos =  m_screenCamera.ScreenToWorldPoint(transform.position);
		m_worldEndPoint = m_screenCamera.ScreenToWorldPoint(m_endPoint);
		FinishEdit[] fes = FindObjectsOfType<FinishEdit>();
		foreach (FinishEdit fe in fes) {
			fe.SetEditingObject((this));
		}
	}

	public void ColorPermitted() {
		m_sprite.color = m_invalidColor;
		bool once = false;
		bool twice = false;
		Vector3 pos = m_screenCamera.ScreenToWorldPoint(m_endPoint);
		Vector3 start = m_screenCamera.ScreenToWorldPoint(transform.position);
		Vector3 diff = pos - start;
		if (Physics2D.Raycast(pos, Vector3.forward, 1000f, m_terrain) &&
			Physics2D.Raycast(start, Vector3.forward, 1000f, m_terrain)) {

			int i;
			int count = 50;
			float rate = diff.magnitude / count;
			pos.z = 0f;
			start.z = 0f;
			for (i = 0; i < count; i++) {
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
				start += diff.normalized * rate;
			}
			if (i == count && once) {
				m_sprite.color = Color.white;
			}
		}
	}

	public void SetOrigin() {
		if (Input.touchCount > 0) {
			m_touch = Input.GetTouch(Input.touchCount - 1);
		}
		m_worldPos =  m_screenCamera.ScreenToWorldPoint(m_touch.position);
		print(m_touch.position * m_toReferenceResolution);
		transform.position = m_touch.position;
		RefreshSize();
	}

	public void SetEndPoint() {
		if (Input.touchCount > 0) {
			m_touch = Input.GetTouch(Input.touchCount - 1);
		}
		m_worldEndPoint = m_screenCamera.ScreenToWorldPoint(m_touch.position);
		m_endPoint = m_touch.position ;
		RefreshSize();
	}

	void RefreshSize() {
		Vector3 diff = m_endPoint - transform.position;
		diff *= m_toReferenceResolution;
		Vector2 size = new Vector2(diff.magnitude, m_rect.sizeDelta.y) ;
		m_rect.sizeDelta = size;
		m_rect.eulerAngles = new Vector3(0f, 0f, Vector2.SignedAngle(Vector3.right, diff));
		ColorPermitted();
	}

	public void Cancel() {
		ResourceManager.Instance.Lianas.Add();
		Destroy(gameObject);
	}

	public void Confirm() {
		if (m_sprite.color == m_invalidColor) {
			Cancel();
		} else {
			m_worldPos.z = 0f;
			m_worldEndPoint.z = 0f;
			GameObject liana = Instantiate(m_resourcePrefab, m_worldPos, transform.rotation);
			BoxCollider2D collider = liana.GetComponent<BoxCollider2D>();
			SpriteRenderer sprite = liana.GetComponent<SpriteRenderer>();
			m_newSize = sprite.size;
			Vector3 worldDiff = m_worldEndPoint - m_worldPos;
			m_newSize.x = worldDiff.magnitude / liana.transform.localScale.x;
			sprite.size = m_newSize;
			collider.size = new Vector2(m_newSize.x, collider.size.y);
			collider.offset = new Vector2(m_newSize.x * 0.5f, 0f);
			collider.enabled = true;
			Destroy(gameObject);
		}
	}

	private void OnDrawGizmos() {
		Gizmos.DrawWireSphere(m_worldEndPoint, 1f);
		Gizmos.DrawWireSphere(m_worldPos, 1f);
	}
}