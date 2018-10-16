﻿using UnityEngine;

public class LianaButton : MonoBehaviour {
    [SerializeField] GameObject m_liana;
    ResourceManager m_resourceManager;

    private void Awake() {
        m_resourceManager = FindObjectOfType<ResourceManager>();
    }

    public void Instance() {
        if (m_resourceManager.Lianas.Request()) {
            GameObject go = Instantiate(m_liana);
            go.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}