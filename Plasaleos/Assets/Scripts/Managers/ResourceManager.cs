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
    int m_mushrooms;
    int m_alerts;
    int m_scares;

    private void Awake() {
        m_lianas = m_levelResources.lianas;
        m_mushrooms = m_levelResources.mushrooms;
        m_alerts = m_levelResources.alerts;
        m_scares = m_levelResources.scares;
        LianaChange.Invoke();
        MushroomChange.Invoke();
        AlertChange.Invoke();
        ScareChange.Invoke();
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

    public bool RequestMushroom() {
        if (m_mushrooms > 0) {
            m_mushrooms--;
            MushroomChange.Invoke();
            return true;
        } else {
            return false;
        }
    }

    public void AddMushroom() {
        m_mushrooms++;
        MushroomChange.Invoke();
    }

    public int GetMushroomAmount() {
        return m_mushrooms;
    }

    public bool RequestAlert() {
        if (m_alerts > 0) {
            m_alerts--;
            AlertChange.Invoke();
            return true;
        } else {
            return false;
        }
    }

    public void AddAlert() {
        m_alerts++;
        AlertChange.Invoke();
    }

    public int GetAlertAmount() {
        return m_alerts;
    }

    public bool RequestScare() {
        if (m_scares > 0) {
            m_scares--;
            ScareChange.Invoke();
            return true;
        } else {
            return false;
        }
    }

    public void AddScare() {
        m_scares++;
        ScareChange.Invoke();
    }

    public int GetScareAmount() {
        return m_scares;
    }
}