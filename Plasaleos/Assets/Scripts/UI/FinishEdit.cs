using UnityEngine;
using UnityEngine.UI;

public class FinishEdit : MonoBehaviour {
    [SerializeField] Canvas m_elementsButtons;
    IResourceEdition m_editingResource;

    void Finish() {
        Time.timeScale = 1f;
        m_elementsButtons.enabled = true;
        transform.parent.gameObject.SetActive(false);
    }

    public void Cancel() {
        m_editingResource.Cancel();
        Finish();
    }

    public void Confirm() {
        m_editingResource.Confirm();
        Finish();
    }

    public void SetEditingObject(IResourceEdition editingResource) {
        m_editingResource = editingResource;
    }
}