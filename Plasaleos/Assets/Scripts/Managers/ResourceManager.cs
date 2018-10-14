using UnityEngine;
using UnityEngine.Events;

public class ResourceManager : MonoBehaviour {
    [SerializeField] LevelResources m_levelResources;
    [HideInInspector]
    public UnityEvent LianaChange;
    [HideInInspector]
    public UnityEvent MushroomChange;
    [HideInInspector]
    public UnityEvent AlertChange;
    [HideInInspector]
    public UnityEvent ScareChange;
    int m_lianas;

    private void Awake() {
        m_lianas = m_levelResources.lianas;
        LianaChange.Invoke();
    }

    public bool RequestLiana() {
        if (m_lianas > 0) {
            m_lianas--;
            LianaChange.Invoke();
            return true;
        } else {
            return false;
        }
    }

    public void AddLiana() {
        m_lianas++;
        LianaChange.Invoke();
    }

    public int GetLianaAmount() {
        return m_lianas;
    }
}