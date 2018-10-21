using UnityEngine;
using UnityEngine.UI;

public class FinishEdit : MonoBehaviour {
    [SerializeField] GameObject m_elementsButtons;

    public void Finish() {
        m_elementsButtons.SetActive(true);
        transform.parent.gameObject.SetActive(false);
    }

}