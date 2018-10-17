using UnityEngine;

public class AmbientButton : MonoBehaviour {
    [SerializeField] GameObject m_object;
    [SerializeField] Resource m_resource;

    public void Instance() {
        if (m_resource.Request()) {
            GameObject go = Instantiate(m_object);
            go.SetActive(true);
            Time.timeScale = 0f;
        }
    }

}