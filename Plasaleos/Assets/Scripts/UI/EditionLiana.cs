using UnityEngine;
using UnityEngine.UI;

public class EditionLiana : MonoBehaviour, IResourceEdition {
	[SerializeField] GameObject m_resourcePrefab;
	[SerializeField] LayerMask m_terrain;
	[SerializeField] Color m_invalidColor;
	ResourceManager m_resourceManager;
	Camera m_screenCamera;
	Vector3 m_endPoint;
	SpriteRenderer m_sprite;
	Vector2 m_newSize;
	Touch m_touch;

	private void Awake() {
		m_screenCamera = Camera.main;
		m_sprite = GetComponent<SpriteRenderer>();
		m_resourceManager = FindObjectOfType<ResourceManager>();
		// transform.position = (new Vector2(Screen.width, Screen.height)) / 2f;
		Vector2 pos2D = transform.position;
		m_endPoint = pos2D + m_sprite.size;
		m_sprite.color = m_invalidColor;
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
		m_newSize = new Vector2(diff.magnitude / 1.85f, 0f);
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
		print("hello");
		if (Input.touchCount > 0) {
			m_touch = Input.GetTouch(Input.touchCount - 1);
			transform.position = m_screenCamera.ScreenToWorldPoint(m_touch.position);
			RefreshSize();
		}
	}

	public void SetEndPoint() {
		if (Input.touchCount > 0) {
			m_touch = Input.GetTouch(Input.touchCount - 1);
		}
		m_endPoint = m_touch.position;
		RefreshSize();
	}

	void RefreshSize() {
		Vector3 diff = m_endPoint - transform.position;
		Vector2 size = new Vector2(diff.magnitude, m_sprite.size.y);
		m_sprite.size = size;
		transform.eulerAngles = new Vector3(0f, 0f, Vector2.SignedAngle(Vector3.right, diff));
		ColorPermitted();
	}

	public void Cancel() {
		m_resourceManager.Lianas.Add();
		Destroy(gameObject);
	}

	public void Confirm() {
		if (m_sprite.color == m_invalidColor) {
			Cancel();
		} else {
			Vector3 pos = m_screenCamera.ScreenToWorldPoint(transform.position);
			pos.z = 0f;
			GameObject liana = Instantiate(m_resourcePrefab, pos, transform.rotation);
			BoxCollider2D collider = liana.GetComponent<BoxCollider2D>();
			SpriteRenderer sprite = liana.GetComponent<SpriteRenderer>();
			m_newSize.y = sprite.size.y;
			sprite.size = m_newSize;
			collider.size = m_newSize;
			collider.offset = new Vector2(m_newSize.x * 0.5f, 0f);
			collider.enabled = true;
			Destroy(gameObject);
		}
	}
}