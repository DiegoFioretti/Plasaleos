using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ResourceListener : MonoBehaviour {
    [SerializeField] Resource m_resource;
    Text m_text;
    Button m_button;
    EventTrigger m_eventTrigger;

    private void Awake() {
        m_text = GetComponentInChildren<Text>();
        m_button = GetComponentInParent<Button>();
        m_eventTrigger = GetComponentInParent<EventTrigger>();
    }

    private void Start() {
        m_resource.Change.AddListener(UpdateText);
        UpdateText();
    }

    void UpdateText() {
        m_text.text = m_resource.GetCount().ToString();
        if (m_text.text == "0") {
            m_button.interactable = false;
            if (m_eventTrigger) {
                m_eventTrigger.enabled = false;
            }
        } else {
            m_button.interactable = true;
            if (m_eventTrigger) {
                m_eventTrigger.enabled = true;
            }
        }
    }
}