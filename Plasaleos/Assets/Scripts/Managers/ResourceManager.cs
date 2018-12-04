using UnityEngine;
using UnityEngine.Events;

public class ResourceManager : MonoBehaviour {
    static public ResourceManager Instance;
    [SerializeField] LevelResources m_levelResources;
    [Header("Ambient")]
    [SerializeField] Resource m_lianas;
    [SerializeField] Resource m_mushrooms;
    [Header("Sounds")]
    [SerializeField] Resource m_alerts;
    [SerializeField] Resource m_scares;
    Resource m_lastResourceAffected;
    public Resource Lianas { get { return m_lianas; } }
    public Resource Mushrooms { get { return m_mushrooms; } }
    public Resource Alerts { get { return m_alerts; } }
    public Resource Scares { get { return m_scares; } }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else{
            Destroy(gameObject);
        }
        m_lianas.Init(m_levelResources.lianas);
        m_mushrooms.Init(m_levelResources.mushrooms);
        m_alerts.Init(m_levelResources.alerts);
        m_scares.Init(m_levelResources.scares);
        m_lianas.Change.Invoke();
        m_mushrooms.Change.Invoke();
        m_alerts.Change.Invoke();
        m_scares.Change.Invoke();
    }

    public void UpdateLastResourceAffected(Resource lastUsed) {
        m_lastResourceAffected = lastUsed;
    }

    public void RecoverLastResource() {
        m_lastResourceAffected.Add();
    }

    public bool IsGravityRestricted() {
        return m_levelResources.restricted;
    }

    public void SetGravityRestriction(bool restriction){
        m_levelResources.restricted = restriction;
    }

    public Vector2 GetGravityDirection() {
        return m_levelResources.direction.normalized;
    }
}