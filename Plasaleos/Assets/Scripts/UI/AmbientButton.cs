using UnityEngine;

public class AmbientButton : MonoBehaviour {
    [SerializeField] GameObject m_object;
    [SerializeField] Resource m_resource;
    [SerializeField] Transform m_editionCanvas;

    public void Instance() {
        if (m_resource.Request()) {
            GameObject go = Instantiate(m_object);
            go.transform.SetParent(m_editionCanvas);
            go.SetActive(true);
            Time.timeScale = 0f;
        }
    }

}