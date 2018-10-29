using UnityEngine;

public class AlertAlien : MonoBehaviour {
    [SerializeField] GameObject m_alertButton;
    [SerializeField] GameObject[] m_continueButtons;
    ResourceManager m_resourceManager;
    float m_alertDuration;
    bool m_alerted;

    private void Awake() {
        m_alerted = false;
        m_alertButton.SetActive(true);
        foreach (GameObject continueButton in m_continueButtons) {
            continueButton.SetActive(false);
        }
        m_resourceManager = FindObjectOfType<ResourceManager>();
        m_alertDuration = FindObjectOfType<Alien>().GetAlertDuration();
    }

    public void ToggleAlert() {
        if (m_alerted) {
            Alert();
        } else if (m_resourceManager.Alerts.Request()) {
            Alert();
        }
    }

    void Alert() {
        GameObject[] go = GameObject.FindGameObjectsWithTag("Alien");
        for (int i = 0; i < go.Length; i++) {
            go[i].GetComponent<Alien>().Alert();
        }
        if (!m_alerted) {
            Invoke("ExpireAlert", m_alertDuration);
        } else {
            CancelInvoke();
        }
        m_alerted = !m_alerted;
        m_alertButton.SetActive(!m_alerted);
        foreach (GameObject continueButton in m_continueButtons) {
            continueButton.SetActive(m_alerted);
        }
    }

    void ExpireAlert() {
        m_alerted = !m_alerted;
        m_alertButton.SetActive(!m_alerted);
        foreach (GameObject continueButton in m_continueButtons) {
            continueButton.SetActive(m_alerted);
        }
    }
}