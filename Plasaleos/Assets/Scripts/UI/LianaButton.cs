using UnityEngine;

public class LianaButton : MonoBehaviour {
    [SerializeField] GameObject m_liana;
    ResourceManager m_resourceManager;

    private void Awake() {
        m_resourceManager = FindObjectOfType<ResourceManager>();
    }

    public void InstanceLiana() {
        if (m_resourceManager.RequestLiana()) {
            GameObject go = Instantiate(m_liana);
            go.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}