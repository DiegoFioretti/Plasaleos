using UnityEngine;

public class EndController : MonoBehaviour {
    [SerializeField] GameObject m_endCanvas;
    [SerializeField] GameObject[] m_editionCanvas;

    private void Start() {
        LevelManager.instance.LevelWon.AddListener(ToggleUI);
    }

    void ToggleUI() {
        if (!m_endCanvas.activeInHierarchy) {
            foreach (GameObject go in m_editionCanvas) {
                go.SetActive(false);
            }
            m_endCanvas.SetActive(true);
        }
    }
}