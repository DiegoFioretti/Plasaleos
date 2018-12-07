using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareAlien : MonoBehaviour {
    ResourceManager m_resourceManager;

    private void Awake() {
        m_resourceManager = FindObjectOfType<ResourceManager>();
    }
    public void ChangeToScare() {
        if (m_resourceManager.Scares.Request()) {
            GameObject[] go = GameObject.FindGameObjectsWithTag("Alien");
            for (int i = 0; i < go.Length; i++) {
                go[i].GetComponent<Alien>().Scare();
            }
            AkSoundEngine.PostEvent("ScareSound", gameObject);
        }
    }
}