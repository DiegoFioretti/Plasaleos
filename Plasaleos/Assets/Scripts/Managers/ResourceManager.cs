using UnityEngine;
using UnityEngine.Events;

public class ResourceManager : MonoBehaviour {
    [SerializeField] LevelResources m_levelResources;
    [Header("Ambient")]
    [SerializeField] Resource m_lianas;
    [SerializeField] Resource m_mushrooms;
    [Header("Sounds")]
    [SerializeField] Resource m_alerts;
    [SerializeField] Resource m_scares;
    public Resource Lianas { get { return m_lianas; } }
    public Resource Mushroom { get { return m_mushrooms; } }
    public Resource Alert { get { return m_alerts; } }
    public Resource Scare { get { return m_scares; } }

    private void Awake() {
        m_lianas.Init(m_levelResources.lianas);
        m_mushrooms.Init(m_levelResources.mushrooms);
        m_alerts.Init(m_levelResources.alerts);
        m_scares.Init(m_levelResources.scares);
        m_lianas.Change.Invoke();
        m_mushrooms.Change.Invoke();
        m_alerts.Change.Invoke();
        m_scares.Change.Invoke();
    }
}