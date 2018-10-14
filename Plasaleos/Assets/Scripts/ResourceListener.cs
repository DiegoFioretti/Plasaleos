using UnityEngine;
using UnityEngine.UI;

enum Resource { LIANA, MUSHROOM, ALERT, SCARE }

public class ResourceListener : MonoBehaviour {
    [SerializeField] Resource m_resource;
    ResourceManager m_resourceManager;
    Text m_text;
    Button m_button;

    private void Awake() {
        m_text = GetComponentInChildren<Text>();
        m_button = GetComponentInParent<Button>();
        m_resourceManager = FindObjectOfType<ResourceManager>();
        if (m_resource == Resource.LIANA) {
            m_resourceManager.LianaChange.AddListener(UpdateText);
        } else if (m_resource == Resource.MUSHROOM) {
            m_resourceManager.MushroomChange.AddListener(UpdateText);
        } else if (m_resource == Resource.ALERT) {
            m_resourceManager.AlertChange.AddListener(UpdateText);
        } else if (m_resource == Resource.SCARE) {
            m_resourceManager.ScareChange.AddListener(UpdateText);
        }
    }

    void UpdateText() {
        if (m_resource == Resource.LIANA) {
            m_text.text = m_resourceManager.GetLianaAmount().ToString();
        } else if (m_resource == Resource.MUSHROOM) {
            // m_text.text = m_resourceManager.GetLianaAmount().ToString();
        } else if (m_resource == Resource.ALERT) {
            // m_text.text = m_resourceManager.GetLianaAmount().ToString();
        } else if (m_resource == Resource.SCARE) {
            // m_text.text = m_resourceManager.GetLianaAmount().ToString();
        }
        if (m_text.text == "0") {
            m_button.interactable = false;
        }
    }
}