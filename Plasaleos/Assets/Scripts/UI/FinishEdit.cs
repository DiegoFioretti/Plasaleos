using UnityEngine;
using UnityEngine.UI;

public class FinishEdit : MonoBehaviour {
    [SerializeField] Canvas m_elementsButtons;

    public void Finish() {
        m_elementsButtons.enabled = true;
        transform.parent.gameObject.SetActive(false);
    }

}