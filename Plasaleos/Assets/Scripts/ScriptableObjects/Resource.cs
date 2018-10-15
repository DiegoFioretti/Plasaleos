using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Resources/Resource")]
public class Resource : ScriptableObject {
    int m_count;
    [HideInInspector]
    public UnityEvent Change;

    public void Init(int count) {
        m_count = count;
    }

    public bool Request() {
        if (m_count > 0) {
            m_count--;
            Change.Invoke();
            return true;
        } else {
            return false;
        }
    }

    public void Add() {
        m_count++;
        Change.Invoke();
    }

    public int GetCount() {
        return m_count;
    }

}