using UnityEngine;
using UnityEngine.UI;

public class FinishEdit : MonoBehaviour {
    [SerializeField] RectMask2D m_elementsButtons;

    public void Finish() {
        m_elementsButtons.enabled = false;
        transform.parent.gameObject.SetActive(false);
    }

}