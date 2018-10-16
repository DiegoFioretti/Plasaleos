using UnityEngine;

public class MushroomButton : MonoBehaviour {
    [SerializeField] GameObject m_mushroom;
    ResourceManager m_resourceManager;

    private void Awake() {
        m_resourceManager = FindObjectOfType<ResourceManager>();
    }

    public void Instance() {
        if (m_resourceManager.Mushrooms.Request()) {
            GameObject go = Instantiate(m_mushroom);
            go.SetActive(true);
            Time.timeScale = 0f;
        }
    }

}