using UnityEngine;

public class Tutorial : MonoBehaviour {

    private void OnEnable() {
        Time.timeScale = 0f;
    }

    public void Continue() {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

}