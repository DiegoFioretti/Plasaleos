using UnityEngine;
using UnityEngine.UI;

public class ResourceListener : MonoBehaviour {
    [SerializeField] Resource m_resource;
    ResourceManager m_resourceManager;
    Text m_text;
    Button m_button;

    private void Awake() {
        m_text = GetComponentInChildren<Text>();
        m_button = GetComponentInParent<Button>();
        m_resourceManager = FindObjectOfType<ResourceManager>();
    }

    private void Start() {
        m_resource.Change.AddListener(UpdateText);
        UpdateText();
    }

    void UpdateText() {
        m_text.text = m_resource.GetCount().ToString();
        if (m_text.text == "0") {
            m_button.interactable = false;
        }
    }
}