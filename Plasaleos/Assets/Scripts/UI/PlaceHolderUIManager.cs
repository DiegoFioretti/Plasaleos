using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaceHolderUIManager : MonoBehaviour {
    private void Awake() {
        Screen.sleepTimeout = 15;
        Time.timeScale = 1f;
    }

    public void LoadLevel1() {
        SceneManager.LoadScene("testLevel_00");
    }

    public void LoadLevel2() {
        SceneManager.LoadScene("testLevel_01");
    }

    public void LoadLevel3() {
        SceneManager.LoadScene("testLevel_02");
    }

    public void LoadLevel4() {
        SceneManager.LoadScene("testLevel_03");
    }

    public void LoadLevel5() {
        SceneManager.LoadScene("testLevel_04");
    }

    public void LoadLevel6() {
        SceneManager.LoadScene("testLevel_05");
    }

    public void LoadLevel7() {
        SceneManager.LoadScene("testLevel_06");
    }

    public void LoadLevel8() {
        SceneManager.LoadScene("testLevel_07");
    }

    public void LoadLevel9() {
        SceneManager.LoadScene("testLevel_08");
    }

    public void LoadLevel10() {
        SceneManager.LoadScene("testLevel_09");
    }

    public void LoadLevel11() {
        SceneManager.LoadScene("testLevel_10");
    }

    public void LoadLevel(string level) {
        SceneManager.LoadScene(level);
    }

    public void ResetLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame() {
        Application.Quit();
    }
}