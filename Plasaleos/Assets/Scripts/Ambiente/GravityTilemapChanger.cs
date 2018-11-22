using UnityEngine;
using UnityEngine.Tilemaps;

public class GravityTilemapChanger : MonoBehaviour {
    [SerializeField] TilemapRenderer m_unrestrictedTilemap;
    [SerializeField] TilemapRenderer m_restrictedTilemap;

    private void Start() {    
        GravityController.Instance.ToRestricted.AddListener(ChangeToRestric);
        GravityController.Instance.ToUnrestricted.AddListener(ChangeToUnrestric);
    }

    void ChangeToRestric() {
        m_restrictedTilemap.enabled = true;
        m_unrestrictedTilemap.enabled = false;
    }

    void ChangeToUnrestric() {
        m_restrictedTilemap.enabled = false;
        m_unrestrictedTilemap.enabled = true;
    }
}
