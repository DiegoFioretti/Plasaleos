using UnityEngine;
using UnityEngine.Tilemaps;

public class GravityTilemapChanger : MonoBehaviour {
    [SerializeField] TilemapRenderer m_unrestrictedTilemap;
    [SerializeField] TilemapRenderer m_restrictedTilemap;

    private void Start() {    
        GravityController.Instance.RestrictionChange.AddListener(ChangeRestriction);
    }

    void ChangeRestriction() {
        bool restriction = GravityController.Instance.Restricted;
        m_restrictedTilemap.enabled = restriction;
        m_unrestrictedTilemap.enabled = !restriction;
    }

}
